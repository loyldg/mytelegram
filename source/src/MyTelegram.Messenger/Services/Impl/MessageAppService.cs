using System.Text;

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
    IOffsetHelper offsetHelper,
	IDataEncryptionHelper dataEncryptionHelper,
    IOptionsMonitor<MyTelegramMessengerServerOptions> options,
    IIdGenerator idGenerator)
    : BaseAppService, IMessageAppService, ITransientDependency
{
    private const string HashtagPattern = "#(\\w+)";
    private const string UrlPattern = @"(?:^|\s)((?:https?:\/\/)?(?:www\.)?[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)+(?:\/[^\s]*)?)";

    private static byte[]? _encryptionKey;
    private static KeyConfig? _keyConfig;

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
            views = 1;
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
        byte[]? encryptedData = null;
        byte[]? inboxMessageEncryptedData = null;
        if (!string.IsNullOrEmpty(input.Message) &&
            options.CurrentValue.EncryptionConfig is { Enabled: true, MessageKeys.Count: > 0 })
        {
            _keyConfig ??= options.CurrentValue.EncryptionConfig.MessageKeys[0];
            _encryptionKey ??= Encoding.UTF8.GetBytes(_keyConfig.Key);
            encryptedData = dataEncryptionHelper.Encrypt(_keyConfig.Id, _encryptionKey, ownerPeerId, input.Message);

            if (input.ToPeer.PeerType == PeerType.User && input.ToPeer.PeerId != input.SenderUserId)
            {
                inboxMessageEncryptedData =
                    dataEncryptionHelper.Encrypt(_keyConfig.Id, _encryptionKey, input.ToPeer.PeerId, input.Message);
            }
        }
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
            Views: views,
			EncryptedData: encryptedData,
            InboxMessageEncryptedData: inboxMessageEncryptedData
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
                if (userReadModel.UserId != MyTelegramConsts.NotificationServiceUserId && !userReadModel.Premium)
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
        if (string.IsNullOrEmpty(message))
        {
            return Task.FromResult<List<long>>([]);
        }

        ProcessMessageEntityHashtag(message, entities);
        ProcessMessageEntityUrlList(message, entities);
        return ProcessMessageEntityMentionAsync(message, entities, toPeer);
    }

    private async Task<List<long>> ProcessMessageEntityMentionAsync(string message, IList<IMessageEntity>? entities, Peer toPeer)
    {
        var mentionsAndUserNames = GetMentions(message);
        var mentions = mentionsAndUserNames.mentions;
        var mentionedUserNames = mentionsAndUserNames.userNameList;
        var mentionedUserIds = new List<long>();

        if (entities?.Count > 0)
        {
            foreach (var messageEntity in entities)
            {
                switch (messageEntity)
                {
                    case TInputMessageEntityMentionName inputMessageEntityMentionName:
                        var userPeer = peerHelper.GetPeer(inputMessageEntityMentionName.UserId);
                        mentionedUserIds.Add(userPeer.PeerId);
                        break;
                    case TMessageEntityMention messageEntityMention:
                        mentionedUserNames.Add(message.Substring(messageEntityMention.Offset + 1,
                            messageEntityMention.Length - 1));
                        break;
                    case TMessageEntityMentionName messageEntityMentionName:
                        mentionedUserIds.Add(messageEntityMentionName.UserId);
                        break;
                }
            }
        }

        if (mentionedUserNames.Count > 0)
        {
            entities ??= [];
            foreach (var messageEntityMention in mentions)
            {
                entities.Add(messageEntityMention);
            }
        }

        if (toPeer.PeerType == PeerType.Channel)
        {
            var mentionedUsers =
                await queryProcessor.ProcessAsync(new GetUserNameListByNamesQuery(mentionedUserNames, PeerType.User));
            mentionedUserIds.AddRange(mentionedUsers.Select(p => p.PeerId).Distinct().ToList());

            var memberUserIds =
                await queryProcessor.ProcessAsync(new GetChannelMemberIdListQuery(toPeer.PeerId, mentionedUserIds));

            mentionedUserIds = memberUserIds.ToList();
        }
        else
        {
            mentionedUserIds = [];
        }

        return mentionedUserIds;
    }

    private (List<TMessageEntityMention> mentions, List<string> userNameList) GetMentions(string message)
    {
        var pattern = "@(\\w{4,40})";
        var mentions = new List<TMessageEntityMention>();
        var matches = Regex.Matches(message, pattern);
        var userNameList = new List<string>();
        foreach (Match match in matches)
        {
            if (match.Success)
            {
                mentions.Add(new TMessageEntityMention
                {
                    Offset = match.Index,
                    Length = match.Length
                });
                userNameList.Add(match.Value[1..]);
            }
        }

        return (mentions, userNameList);
    }

    private void ProcessMessageEntityUrlList(string message, IList<IMessageEntity>? entities)
    {
        var matches = Regex.Matches(message, UrlPattern);
        foreach (Match match in matches)
        {
            if (match.Success)
            {
                var entity = new TMessageEntityUrl
                {
                    Offset = match.Index,
                    Length = match.Length
                };
                entities ??= [];
                entities.Add(entity);
            }
        }
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
}