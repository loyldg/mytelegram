using MyTelegram.Messenger.Services.Interfaces;

namespace MyTelegram.Messenger.Services.Impl;

public class MessageAppService(
    IQueryProcessor queryProcessor,
    ICommandBus commandBus,
    IObjectMapper objectMapper,
    IPeerHelper peerHelper,
    IPhotoAppService photoAppService,
    IChannelAppService channelAppService,
    IUserAppService userAppService,
    IPrivacyAppService privacyAppService,
    IContactAppService contactAppService,
    IUsernameHelper usernameHelper,
    IOffsetHelper offsetHelper,
    IIdGenerator idGenerator)
    : BaseAppService, IMessageAppService, ITransientDependency
{
    private const string HashtagPattern = @"#[A-Za-z][A-Za-z0-9_]{0,255}";

    // Max length clamp for URL entities
    private const int MaxUrlLength = 2048;

    // Use a short timeout to prevent runaway backtracking
    private static readonly TimeSpan RxTimeout = TimeSpan.FromMilliseconds(150);

    // Cap for normal letter TLDs: e.g., "com", "technology", "international" (<=15)
    private const int TldMaxLetters = 15;

    // Strong URL regex: optional scheme, IPv4 or domain, optional port, path allowing balanced parens;
    // no trailing punctuation inside the entity; avoids picking up emails/usernames.
    private static readonly Regex UrlRegex = new(
        """
(?xi)
(?<![@\w./-])                         # left boundary (avoid emails / word-internals)
(?:https?://)?                        # optional scheme
(?:www\.)?                            # optional www.

(                                      # --- host ---
  (?:                                   # IPv4
    (?:25[0-5]|2[0-4]\d|1?\d?\d)\.
    (?:25[0-5]|2[0-4]\d|1?\d?\d)\.
    (?:25[0-5]|2[0-4]\d|1?\d?\d)\.
    (?:25[0-5]|2[0-4]\d|1?\d?\d)
  )
  |
  (?:                                   # domain with final capped TLD
    (?:[a-z0-9](?:[a-z0-9-]{0,61}[a-z0-9])?\.)+  # one or more labels+
    (?:                                  # final TLD:
        [a-z]{2,15}                      #   letters-only TLD, 2..15
      | xn--[a-z0-9-]{1,20}              #   punycode TLD (total len up to ~24)
    )
  )
)

(?: : (?<port>\d{2,5}) )?              # optional port

(?:                                     # --- optional path/query/frag ---
    /                                    # path starts
    (?:
      [^\s<>()\[\]{}"'`]+
      | \([^\s<>()\[\]{}"'`]*\)
    )*
)?
(?=
   \s | $ | [)\]\}.,!?;:]              # stop before trailing punctuation/space/end
)
""",
        RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant,
        RxTimeout);

    // Email ranges we want to exclude from mentions
    private static readonly Regex EmailRegex = new(
        @"(?xi)\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,24}\b",
        RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase,
        RxTimeout);

    public void CheckBotPermission(long requestUserId, Peer toPeer)
    {
        if (peerHelper.IsBotUser(requestUserId) && peerHelper.IsBotUser(toPeer.PeerId))
        {
            RpcErrors.RpcErrors400.UserIsBot.ThrowRpcError();
        }
    }

    public async Task<bool> CanSendAsPeerAsync(long channelId, long userId)
    {
        var channelReadModel = await channelAppService.GetAsync(channelId);
        var canSendAsPeer = false;

        // Channel: signature: true and hasAdminRights: true and canWriteToChat: true
        if (channelReadModel is { Broadcast: true, Signatures: true })
        {
            var channelAdmin = channelReadModel.AdminList.FirstOrDefault(p => p.UserId == userId);
            if (channelReadModel.CreatorId == userId || (channelAdmin?.AdminRights.PostMessages ?? false))
            {
                canSendAsPeer = true;
            }
        }

        if (!canSendAsPeer)
        {
            // Super group with linked channel/Public super group
            if (channelReadModel.MegaGroup && (!string.IsNullOrEmpty(channelReadModel.UserName) ||
                                               channelReadModel.LinkedChatId != null))
            {
                canSendAsPeer = true;
            }
        }

        return canSendAsPeer;
    }

    public async Task<bool> IsValidSendAsPeerAsync(long requestUserId, Peer toPeer, Peer? sendAsPeer)
    {
        if (sendAsPeer != null)
        {
            if (toPeer.PeerType != PeerType.Channel)
            {
                return false;
            }

            switch (sendAsPeer.PeerType)
            {
                case PeerType.User:
                case PeerType.Self:
                    if (sendAsPeer.PeerId != requestUserId)
                    {
                        return false;
                    }

                    break;

                case PeerType.Channel:
                    var canSendAsPeer = await CanSendAsPeerAsync(toPeer.PeerId, requestUserId);
                    if (!canSendAsPeer)
                    {
                        return false;
                    }

                    var sendAsChannelReadModel = await channelAppService.GetAsync(sendAsPeer.PeerId);

                    // We can only use the public channels created by the current user as SendAsPeer
                    if (sendAsChannelReadModel == null! ||
                        sendAsChannelReadModel.CreatorId != requestUserId ||
                        (string.IsNullOrEmpty(sendAsChannelReadModel.UserName) &&
                         sendAsChannelReadModel.LinkedChatId != toPeer.PeerId &&
                         sendAsChannelReadModel.ChannelId != toPeer.PeerId))
                    {
                        return false;
                    }

                    break;
            }
        }

        return true;
    }

    public async Task CheckSendAsAsync(long requestUserId, Peer toPeer, Peer? sendAsPeer)
    {
        var isValid = await IsValidSendAsPeerAsync(requestUserId, toPeer, sendAsPeer);
        if (!isValid)
        {
            RpcErrors.RpcErrors400.SendAsPeerInvalid.ThrowRpcError();
        }
    }

    public async Task<GetMessageOutput> GetChannelDifferenceAsync(GetDifferenceInput input)
    {
        return await GetMessagesInternalAsync(new GetMessagesQuery(input.OwnerPeerId,
            MessageType.Unknown,
            null,
            input.MessageIds,
            0,
            input.Limit,
            null,
            null,
            input.SelfUserId,
            input.Pts), input.Users, input.Chats);
    }

    public Task<GetMessageOutput> GetDifferenceAsync(GetDifferenceInput input)
    {
        return GetMessagesInternalAsync(new GetMessagesQuery(input.OwnerPeerId,
            MessageType.Unknown,
            null,
            null,
            0,
            input.Limit,
            null,
            null,
            input.SelfUserId,
            input.Pts), input.Users, input.Chats);
    }

    public Task<GetMessageOutput> GetHistoryAsync(GetHistoryInput input)
    {
        return GetMessagesCoreAsync(input);
    }

    public Task<GetMessageOutput> GetMessagesAsync(GetMessagesInput input)
    {
        return GetMessagesCoreAsync(input);
    }

    public Task<GetMessageOutput> GetRepliesAsync(GetRepliesInput input)
    {
        return GetMessagesCoreAsync(input);
    }

    public Task<GetMessageOutput> SearchAsync(SearchInput input)
    {
        return GetMessagesCoreAsync(input);
    }

    public Task<GetMessageOutput> SearchGlobalAsync(SearchGlobalInput input)
    {
        return GetMessagesCoreAsync(input);
    }
    public async Task SendMessageAsync(List<SendMessageInput> inputs)
    {
        if (inputs.Count == 0)
        {
            throw new ArgumentException();
        }

        List<SendMessageItem> sendMessageItems = [];
        var firstInput = inputs.First();
        var requestInfo = firstInput.RequestInfo;

        foreach (var input in inputs)
        {
            CheckBotPermission(input.RequestInfo.UserId, input.ToPeer);
            var item = await CreateSendMessageItemAsync(input);
            sendMessageItems.Add(item);
        }

        var command = new StartSendMessageCommand(TempId.New, requestInfo,
            sendMessageItems,
            firstInput.ClearDraft,
            firstInput.IsSendGroupedMessage,
            firstInput.IsSendQuickReplyMessage);

        await commandBus.PublishAsync(command);
    }

    public async Task<SearchPostsResult> SearchPostsAsync(long selfUserId, SearchPostsQuery searchPostsQuery)
    {
        var messageReadModels = await queryProcessor.ProcessAsync(searchPostsQuery);
        HashSet<long> userIds = [];
        HashSet<long> channelIds = [];
        AddExtraPeerIds(messageReadModels, userIds, channelIds);
        var userIdList = userIds.ToList();
        var userReadModels = await userAppService.GetListAsync(userIdList);
        var channelReadModels = channelIds.Count == 0
            ? []
            : await channelAppService.GetListAsync(channelIds);
        var channelMemberReadModels = channelReadModels.Count == 0
            ? []
            : await queryProcessor.ProcessAsync(
                new GetChannelMemberListByChannelIdListQuery(selfUserId, channelIds.ToList()));
        var photoReadModels = await photoAppService.GetPhotosAsync(channelReadModels);

        return new SearchPostsResult(messageReadModels, channelReadModels, channelMemberReadModels, photoReadModels, userReadModels);
    }

    private async Task<IChannelReadModel?> CheckChannelBannedRightsAsync(SendMessageInput input)
    {
        if (input.ToPeer.PeerType != PeerType.Channel)
        {
            return null;
        }

        var channelReadModel = await channelAppService.GetAsync(input.ToPeer.PeerId);
        if (channelReadModel!.Broadcast)
        {
            var admin = channelReadModel.AdminList.FirstOrDefault(p => p.UserId == input.SenderUserId);
            if (admin == null || !admin.AdminRights.PostMessages)
            {
                RpcErrors.RpcErrors403.ChatWriteForbidden.ThrowRpcError();
            }
        }

        var bannedDefaultRights = channelReadModel.DefaultBannedRights ?? ChatBannedRights.CreateDefaultBannedRights();
        if (bannedDefaultRights.SendMessages)
        {
            RpcErrors.RpcErrors403.ChatWriteForbidden.ThrowRpcError();
        }

        var channelMemberReadModel =
            await queryProcessor.ProcessAsync(new GetChannelMemberByUserIdQuery(channelReadModel.ChannelId,
                input.SenderUserId));


        if (channelMemberReadModel == null)
        {
            if (channelReadModel is { Broadcast: false, LinkedChatId: not null, JoinToSend: false })
            {

            }
            else
            {
                RpcErrors.RpcErrors403.ChatGuestSendForbidden.ThrowRpcError();
            }
        }

        if (channelMemberReadModel != null && channelMemberReadModel.BannedRights != 0)
        {
            var memberBannedRights =
                ChatBannedRights.FromValue(channelMemberReadModel.BannedRights, channelMemberReadModel.UntilDate);
            if (!string.IsNullOrEmpty(input.Message))
            {
                if (memberBannedRights.SendMessages)
                {
                    RpcErrors.RpcErrors400.UserBannedInChannel.ThrowRpcError();
                }
            }

            if (input.Media != null)
            {
                if (memberBannedRights.SendMedia)
                {
                    RpcErrors.RpcErrors400.UserBannedInChannel.ThrowRpcError();
                }
            }
        }

        //if (channelReadModel.SlowModeEnabled)
        //{

        //}

        return channelReadModel;
    }

    private async Task<Peer?> GetDefaultSendAsAsync(SendMessageInput input)
    {
        // Get the default SendAsPeer, follow the following rules
        // 1.If the client passes sendAs, verify the client's sendAs, if valid, use the value passed by the client
        // 2.If the client does not pass sendAs, query whether the user has set the default sendAsPeer, if set, use the set value
        // 3.If the client does not pass a value, the default SendAsPeer is not set, and in the discussion group, use discussion group as SendAsPeer
        if (input.SendAs != null)
        {
            if (await IsValidSendAsPeerAsync(input.RequestInfo.UserId, input.ToPeer, input.SendAs))
            {
                return input.SendAs;
            }
        }
        else if (input.ToPeer.PeerType == PeerType.Channel)
        {
            var channelReadModel = await channelAppService.GetAsync(input.ToPeer.PeerId);

            if (!await CanSendAsPeerAsync(input.ToPeer.PeerId, input.RequestInfo.UserId))
            {
                var admin = channelReadModel.AdminList.FirstOrDefault(p => p.UserId == input.SenderUserId);
                if (admin is { AdminRights.Anonymous: true })
                {
                    return channelReadModel.ChannelId.ToChannelPeer();
                }

                return null;
            }
            Peer? sendAsPeer;

            var userConfigReadModel = await queryProcessor.ProcessAsync(
                new GetUserConfigByKeyQuery(input.RequestInfo.UserId, ((int)UserConfigType.SendAsPeer).ToString()));
            if (userConfigReadModel != null)
            {
                if (long.TryParse(userConfigReadModel.Value, out var sendAsPeerId))
                {
                    sendAsPeer = peerHelper.GetPeer(sendAsPeerId);
                    if (await IsValidSendAsPeerAsync(input.RequestInfo.UserId, input.ToPeer, sendAsPeer))
                    {
                        return sendAsPeer;
                    }
                }
            }

            if (channelReadModel is { MegaGroup: true, LinkedChatId: not null })
            {
                sendAsPeer = channelReadModel.ChannelId.ToChannelPeer();
                if (await IsValidSendAsPeerAsync(input.RequestInfo.UserId, input.ToPeer, sendAsPeer))
                {
                    return sendAsPeer;
                }
            }
        }

        return null;
    }

    private async Task<SendMessageItem> CreateSendMessageItemAsync(SendMessageInput input)
    {
        //await CheckAccessHashAsync(input);
        //await CheckSendAsAsync(input);
        await CheckGlobalPrivacySettingsAsync(input);
        var channelReadModel = await CheckChannelBannedRightsAsync(input);

        var entities = input.Entities ?? [];
        var mentionedUserIds = await ProcessMessageEntitiesAsync(input.Message, entities, input.ToPeer);
        if (entities.Count == 0)
        {
            entities = null;
        }
        var ownerPeerId = input.ToPeer.PeerType == PeerType.Channel ? input.ToPeer.PeerId : input.SenderUserId;
        var replyToMsgId = input.InputReplyTo.ToReplyToMsgId();

        // Reply to group: ToPeerId=input.ToPeerId,SenderUserId=input.UserId
        // Reply to user:  ToPeerId=Input.UserId,OwnerPeerId=input.ToPeerId,MessageId=replyToMsgId

        var replyToMsgItems =
            await queryProcessor.ProcessAsync(new GetReplyToMsgIdListQuery(input.ToPeer, input.SenderUserId,
                replyToMsgId));
        var idType = IdType.MessageId;
        var subType = MessageSubType.Normal;
        var messageActionType = MessageActionType.None;
        var post = channelReadModel?.Broadcast ?? false;
        var linkedChannelId = channelReadModel?.Broadcast ?? false ? channelReadModel.LinkedChatId : null;
        var sendAs = await GetDefaultSendAsAsync(input);
        string? postAuthor = null;
        var isPublicPost = channelReadModel is { Broadcast: true, UserName: not null };
        int? views = null;
        if (channelReadModel?.Broadcast ?? false)
        {
            views = 0;
        }
        if (channelReadModel is { Signatures: true, Broadcast: true })
        {
            if (sendAs?.PeerType == PeerType.Channel)
            {
                var sendAsChannelReadModel = await channelAppService.GetAsync(sendAs.PeerId);
                postAuthor = sendAsChannelReadModel.Title;
            }
            else
            {
                var userReadModel = await userAppService.GetAsync(input.RequestInfo.UserId);
                postAuthor = $"{userReadModel.FirstName} {userReadModel.LastName}";
            }

            if (sendAs == null && channelReadModel.SignatureProfiles)
            {
                sendAs = input.RequestInfo.UserId.ToUserPeer();
            }
        }

        var scheduleDate = input.ScheduleDate;
        if (scheduleDate.HasValue)
        {
            // If the schedule_date is less than 20 seconds in the future, the message will be sent immediately,
            // generating a normal updateNewMessage/updateNewChannelMessage.
            if (scheduleDate.Value - CurrentDate < 20)
            {
                scheduleDate = null;
            }
            else
            {
                idType = IdType.ScheduleMessageId;
            }
        }

        var pts = 0;
        MessageReply? reply = null;
        if (post && linkedChannelId.HasValue)
        {
            reply = new MessageReply(linkedChannelId, 0, 0, 0, []);
        }

        var messageId = await idGenerator.NextIdAsync(idType, ownerPeerId);
        //var messageId = 0;
        int? scheduleMessageId = null;
        if (idType == IdType.ScheduleMessageId)
        {
            scheduleMessageId = await idGenerator.NextIdAsync(IdType.ScheduleMessageId, ownerPeerId);
        }

        var date = CurrentDate;
        var hashtags = GetHashtags(input.Message);
        var messageItem = new MessageItem(
            input.ToPeer with { PeerId = ownerPeerId /*, AccessHash = 0 */ },
            input.ToPeer,
            new Peer(PeerType.User, input.SenderUserId),
            input.SenderUserId,
            messageId,
            input.Message,
            date,
            input.RandomId,
            true,
            input.SendMessageType,
            //(MessageType)input.SendMessageType,
            input.MessageType,
            subType,
            input.InputReplyTo,
            //input.MessageActionData,
            input.MessageAction,
            messageActionType,
            entities,
            input.Media,
            input.GroupId,
            PollId: input.PollId,
            Post: post,
            ReplyMarkup: input.ReplyMarkup,
            TopMsgId: input.TopMsgId,
            PostAuthor: postAuthor,
            SendAs: sendAs,
            Effect: input.Effect,
            ReplyToMsgItems: replyToMsgItems?.ToList(),
            LinkedChannelId: linkedChannelId,
            Pts: pts,
            Silent: input.Silent,
            ScheduleDate: scheduleDate,
            ScheduleMessageId: scheduleMessageId,
            Reply: reply,
            InvertMedia: input.InvertMedia,
            PublicPosts: isPublicPost,
            Hashtags: hashtags,
            MentionedUserIds: mentionedUserIds,
            Views: views
        );

        var sendMessageItem = new SendMessageItem(messageItem, input.ClearDraft, mentionedUserIds, []);

        return sendMessageItem;
    }

    public List<string> GetHashtags(string? message)
    {
        if (string.IsNullOrEmpty(message))
        {
            return [];
        }

        var matches = Regex.Matches(message, HashtagPattern);
        var hashtags = new List<string>();
        const int maxHashtags = 10;
        foreach (Match match in matches)
        {
            if (hashtags.Count > maxHashtags)
            {
                break;
            }

            var hashtag = match.Groups[1].Value;
            if (!hashtags.Contains(hashtag))
            {
                hashtags.Add(hashtag);
            }
        }

        return hashtags;
    }

    private async Task CheckGlobalPrivacySettingsAsync(SendMessageInput input)
    {
        if (input.ToPeer.PeerType == PeerType.User && input.RequestInfo.UserId != input.ToPeer.PeerId)
        {
            var globalPrivacySettings = await privacyAppService.GetGlobalPrivacySettingsAsync(input.ToPeer.PeerId);
            if (globalPrivacySettings?.NewNoncontactPeersRequirePremium ?? false)
            {
                var userReadModel = await userAppService.GetAsync(input.RequestInfo.UserId);
                if (userReadModel.UserId != MyTelegramConsts.OfficialUserId && !userReadModel.Premium)
                {
                    var contactType =
                        await contactAppService.GetContactTypeAsync(input.RequestInfo.UserId, input.ToPeer.PeerId);
                    if (contactType != ContactType.Mutual && contactType != ContactType.ContactOfTargetUser)
                    {
                        RpcErrors.RpcErrors406.PrivacyPremiumRequired.ThrowRpcError();
                    }
                }
            }
        }
    }

    public Task<List<long>> ProcessMessageEntitiesAsync(string? message, IList<IMessageEntity>? entities, Peer toPeer)
    {
        if (string.IsNullOrWhiteSpace(message))
            return Task.FromResult<List<long>>([]);

        // 1) URLs first (also returns overlap guard map)
        var used = ProcessMessageEntityUrlListWithOverlap(message, ref entities);

        // 2) Hashtags (no overlap concerns in your spec, but we can keep as-is)
        ProcessMessageEntityHashtag(message, entities);

        // 3) Mentions (skip any overlap with URLs/emails)
        var result = ProcessMessageEntityMentionAsyncSafe(message, entities, toPeer, used);
        return result;
    }

    private void ProcessMessageEntityHashtag(string message, IList<IMessageEntity>? entities)
    {
        var hashtagMatches = Regex.Matches(message, HashtagPattern);
        foreach (Match match in hashtagMatches)
        {
            if (match.Success)
            {
                var entity = new TMessageEntityHashtag
                {
                    Offset = match.Index,
                    Length = match.Length
                };
                entities ??= [];
                entities.Add(entity);
            }
        }
    }

    private Task<GetMessageOutput> GetMessagesCoreAsync<TRequest>(TRequest input)
        where TRequest : GetPagedListInput
    {
        var offset = offsetHelper.GetOffsetInfo(input);
        var query = objectMapper.Map<TRequest, GetMessagesQuery>(input);
        query.Offset = offset;

        return GetMessagesInternalAsync(query);
    }

    private async Task<GetMessageOutput> GetMessagesInternalAsync(GetMessagesQuery query,
        IReadOnlyCollection<long>? users = null,
        IReadOnlyCollection<long>? chats = null)
    {
        var messageList = await queryProcessor.ProcessAsync(query);
        HashSet<long> userIds = users?.ToHashSet() ?? [];
        HashSet<long> channelIds = chats?.ToHashSet() ?? [];
        userIds.Add(query.SelfUserId);

        AddExtraPeerIds(messageList, userIds, channelIds);
        var userIdList = userIds.ToList();

        var userList = await userAppService.GetListAsync(userIdList);

        var channelList = channelIds.Count == 0
            ? []
            : await channelAppService.GetListAsync(channelIds);

        var contactList = await queryProcessor
            .ProcessAsync(new GetContactListQuery(query.SelfUserId, userIdList));

        var photoIds = new List<long>();
        photoIds.AddRange(channelList.Select(p => p.PhotoId ?? 0));
        photoIds.AddRange(userList.Select(p => p.PersonalPhotoId ?? 0));
        photoIds.AddRange(userList.Select(p => p.ProfilePhotoId ?? 0));
        photoIds.AddRange(userList.Select(p => p.FallbackPhotoId ?? 0));
        photoIds.AddRange(contactList.Select(p => p.PhotoId ?? 0));
        photoIds.RemoveAll(p => p == 0);

        var photoList = await photoAppService.GetListAsync(photoIds);

        IReadOnlyCollection<long> joinedChannelIdList = new List<long>();
        if (channelIds.Count > 0)
        {
            joinedChannelIdList = await queryProcessor
                .ProcessAsync(new GetJoinedChannelIdListQuery(query.SelfUserId, [.. channelIds]));
        }

        var privacyList = await privacyAppService.GetPrivacyListAsync(userIdList);
        IReadOnlyCollection<IChannelMemberReadModel> channelMemberList = new List<IChannelMemberReadModel>();
        if (joinedChannelIdList.Count > 0)
        {
            channelMemberList = await queryProcessor
                .ProcessAsync(
                    new GetChannelMemberListByChannelIdListQuery(query.SelfUserId, joinedChannelIdList.ToList()));
        }

        var pts = query.Pts;
        if (pts == 0 && messageList.Count > 0)
        {
            pts = messageList.Max(p => p.Pts);
        }

        var pollIdList = messageList.Where(p => p.PollId.HasValue).Select(p => p.PollId!.Value).ToList();
        IReadOnlyCollection<IPollReadModel>? pollReadModels = null;
        IReadOnlyCollection<IPollAnswerVoterReadModel>? chosenOptions = null;

        if (pollIdList.Count > 0)
        {
            pollReadModels = await queryProcessor.ProcessAsync(new GetPollsQuery(pollIdList));
            chosenOptions = await queryProcessor
                .ProcessAsync(new GetChosenVoteAnswersQuery(pollIdList, query.SelfUserId));
        }

        return new GetMessageOutput(channelList,
            channelMemberList,
            [],
            contactList,
            joinedChannelIdList,
            messageList,
            privacyList,
            userList,
            photoList,
            pollReadModels,
            chosenOptions,
            [],
            query.Limit == messageList.Count,
            query.IsSearchGlobal,
            pts,
            query.SelfUserId,
            query.Limit,
            query.Offset
        );
    }

    public (HashSet<long> userIds, HashSet<long> channelIds) GetExtraPeerIds(
        IReadOnlyCollection<IMessageReadModel> messageReadModels)
    {
        var userIds = new HashSet<long>();
        var channelIds = new HashSet<long>();
        AddExtraPeerIds(messageReadModels, userIds, channelIds);

        return (userIds, channelIds);
    }

    private void AddExtraPeerIds(IReadOnlyCollection<IMessageReadModel> messageReadModels, HashSet<long> userIds,
        HashSet<long> channelIds)
    {
        void AddPeerIdIfNeeded(Peer? peer)
        {
            switch (peer?.PeerType)
            {
                case PeerType.Channel:
                    channelIds.Add(peer.PeerId);
                    break;

                case PeerType.User:
                    userIds.Add(peer.PeerId);
                    break;
            }
        }

        foreach (var messageReadModel in messageReadModels)
        {
            AddPeerIdIfNeeded(messageReadModel.SendAs);
            AddPeerIdIfNeeded(messageReadModel.FwdHeader?.SavedFromPeer);

            var fwd = messageReadModel.FwdHeader;
            AddPeerIdIfNeeded(fwd?.FromId);
            AddPeerIdIfNeeded(fwd?.SavedFromId);
            AddPeerIdIfNeeded(fwd?.SavedFromPeer);
            AddPeerIdIfNeeded(messageReadModel.SendAs);
            var senderPeer = peerHelper.GetPeer(messageReadModel.SenderPeerId);
            AddPeerIdIfNeeded(senderPeer);

            switch (messageReadModel.ToPeerType)
            {
                case PeerType.Channel:
                    channelIds.Add(messageReadModel.ToPeerId);
                    break;

                case PeerType.User:
                    userIds.Add(messageReadModel.ToPeerId);
                    break;
            }

            switch (messageReadModel.MessageAction)
            {
                case TMessageActionChatAddUser messageActionChatAddUser:
                    foreach (var userId in messageActionChatAddUser.Users)
                    {
                        userIds.Add(userId);
                    }
                    break;

                case TMessageActionChatJoinedByLink messageActionChatJoinedByLink:
                    userIds.Add(messageActionChatJoinedByLink.InviterId);
                    break;

                case TMessageActionChatJoinedByRequest:

                    break;

                case TMessageActionChatDeleteUser messageActionChatDeleteUser:
                    userIds.Add(messageActionChatDeleteUser.UserId);
                    break;
            }
        }

    }

    // Creates URL entities, respecting max length and "one entity per character" rule.
    // Returns a boolean mask of used characters (to prevent overlaps with mentions).
    private static bool[] ProcessMessageEntityUrlListWithOverlap(string message, ref IList<IMessageEntity>? entities)
    {
        var used = new bool[message.Length];
        var matches = UrlRegex.Matches(message);
        if (matches.Count == 0) return used;

        entities ??= [];

        foreach (Match m in matches)
        {
            var (start, length) = TrimTrailingPunctuationAndBalance(message, m.Index, m.Length);
            if (length <= 0) continue;

            if (length > MaxUrlLength)
                length = MaxUrlLength;

            if (AnyUsed(used, start, length))
                continue;

            MarkUsed(used, start, length);

            entities.Add(new TMessageEntityUrl
            {
                Offset = start,
                Length = length
            });
        }

        return used;
    }

    private static (int start, int length) TrimTrailingPunctuationAndBalance(string s, int start, int length)
    {
        if (length <= 0) return (start, 0);

        while (length > 0)
        {
            char ch = s[start + length - 1];
            if (")]},.!?;:".IndexOf(ch) >= 0) length--;
            else break;
        }

        // Balance trailing ')' if they exceed '(' in the captured piece
        int open = 0, close = 0;
        for (int i = 0; i < length; i++)
        {
            char c = s[start + i];
            if (c == '(') open++;
            else if (c == ')') close++;
        }
        while (length > 0 && close > open)
        {
            if (s[start + length - 1] == ')') { length--; close--; }
            else break;
        }

        return (start, length);
    }

    private static bool AnyUsed(bool[] used, int start, int len)
    {
        int end = Math.Min(used.Length, start + len);
        for (int i = start; i < end; i++)
            if (used[i]) return true;
        return false;
    }

    private static void MarkUsed(bool[] used, int start, int len)
    {
        int end = Math.Min(used.Length, start + len);
        for (int i = start; i < end; i++)
            used[i] = true;
    }

    // New mention processor that ignores usernames inside URLs or emails
    private async Task<List<long>> ProcessMessageEntityMentionAsyncSafe(
        string message,
        IList<IMessageEntity>? entities,
        Peer toPeer,
        bool[] usedByUrls)
    {
        // Mark email ranges as used too (so @ inside email never becomes mention)
        var used = (bool[])usedByUrls.Clone();
        foreach (Match em in EmailRegex.Matches(message))
            MarkUsed(used, em.Index, em.Length);

        // Collect inline-provided entities first (existing logic preserved)
        var mentionedUserIds = new List<long>();
        var candidateUsernames = new List<string>();
        var mentionEntities = new List<TMessageEntityMention>();

        if (entities is { Count: > 0 })
        {
            foreach (var e in entities)
            {
                switch (e)
                {
                    case TInputMessageEntityMentionName named:
                        mentionedUserIds.Add(peerHelper.GetPeer(named.UserId).PeerId);
                        break;

                    case TMessageEntityMention m:
                        // Keep these, but we’ll add text-parsed mentions below (non-overlapping)
                        candidateUsernames.Add(message.Substring(m.Offset + 1, m.Length - 1));
                        mentionEntities.Add(m);
                        break;

                    case TMessageEntityMentionName mn:
                        mentionedUserIds.Add(mn.UserId);
                        break;
                }
            }
        }

        //// Text-based mentions using safe regex + overlap guard
        //foreach (Match mm in MentionRegex.Matches(message))
        //{
        //    var start = mm.Index;
        //    var length = mm.Length;

        //    if (AnyUsed(used, start, length))
        //        continue; // skip if inside URL/email (or already an entity)

        //    var uname = mm.Groups[1].Value; // without '@'
        //                                    // mark the range as used so nothing overlaps
        //    MarkUsed(used, start, length);

        //    candidateUsernames.Add(uname);
        //    mentionEntities.Add(new TMessageEntityMention { Offset = start, Length = length });
        //}

        foreach (var (start, length, uname) in usernameHelper.FindMentions(message))
        {
            if (AnyUsed(used, start, length))
                continue;

            MarkUsed(used, start, length);
            candidateUsernames.Add(uname);
            mentionEntities.Add(new TMessageEntityMention { Offset = start, Length = length });
        }

        if (mentionEntities.Count > 0)
        {
            entities ??= [];
            foreach (var m in mentionEntities)
                entities.Add(m);
        }

        // Resolve to user IDs (same as your original logic)
        if (toPeer.PeerType == PeerType.Channel && candidateUsernames.Count > 0)
        {
            var mentionedUsers = await queryProcessor.ProcessAsync(
                new GetUserNameListByNamesQuery(candidateUsernames, PeerType.User));

            mentionedUserIds.AddRange(mentionedUsers.Select(p => p.PeerId).Distinct());

            var memberUserIds = await queryProcessor.ProcessAsync(
                new GetChannelMemberIdListQuery(toPeer.PeerId, mentionedUserIds));

            return memberUserIds.ToList();
        }

        return [];
    }

    public async Task<TMessageMediaWebPage?> CreateInvitePreviewIfAnyAsync(
        string text,
        string joinChatDomain)
    {
        if (string.IsNullOrWhiteSpace(text))
            return null;

        var inviteRx = BuildInviteRegex(joinChatDomain); // add the builder below if you don't have it yet

        // Scan URLs once, trim punctuation, clamp length
        var matches = UrlRegex.Matches(text);
        if (matches.Count == 0)
            return null;

        foreach (Match m in matches)
        {
            var (start, length) = TrimTrailingPunctuationAndBalance(text, m.Index, m.Length);
            if (length <= 0) continue;
            if (length > MaxUrlLength) length = MaxUrlLength;

            var url = text.Substring(start, length);
            var im = inviteRx.Match(url);
            if (!im.Success) continue;

            var link = im.Groups["link"].Value;

            var chatInvite = await queryProcessor.ProcessAsync(new GetChatInviteByLinkQuery(link));
            if (chatInvite is null) continue;

            var channel = await channelAppService.GetAsync(chatInvite.PeerId);

            // Supergroup/public channel preview only
            if (!channel.Broadcast || (channel.Broadcast && !string.IsNullOrEmpty(channel.UserName)))
            {
                var baseJoin = joinChatDomain.TrimEnd('/');
                return new TMessageMediaWebPage
                {
                    Webpage = new Schema.TWebPage
                    {
                        Id = Random.Shared.NextInt64(),
                        Url = $"{baseJoin}/+{link}",
                        DisplayUrl = $"{baseJoin}/+{link}",
                        Type = channel.Broadcast ? "telegram_channel" : "telegram_megagroup",
                        SiteName = "MyTelegram",
                        Title = channel.Title,
                        Description = "Join this group on MyTelegram.",
                    }
                };
            }

            // Only one preview is allowed; if this one is not eligible, continue scanning.
        }

        return null;
    }

    private Regex BuildInviteRegex(string joinDomain)
    {
        var normalized = joinDomain.Trim().TrimEnd('/');
        string host, path = "";
        try
        {
            if (normalized.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                var u = new Uri(normalized);
                host = u.Host;
                path = u.AbsolutePath.Trim('/');
            }
            else
            {
                var slash = normalized.IndexOf('/');
                host = slash >= 0 ? normalized[..slash] : normalized;
                path = slash >= 0 ? normalized[(slash + 1)..] : "";
            }
        }
        catch { host = normalized; }

        var hostRx = Regex.Escape(host);
        var pathRx = string.IsNullOrEmpty(path) ? "" : $"{Regex.Escape(path)}/";

        var pat = $$"""
        (?xi)
        \b
        (?:https?://)? (?:www\.)? {{hostRx}} / {{pathRx}} \+ (?<link>[A-Za-z0-9_-]{16,64})
        (?= \s | $ | [)\]\}.,!?;:] )
        """;

        return new Regex(pat, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant, RxTimeout);
    }
}