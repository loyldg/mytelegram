namespace MyTelegram.Domain.Aggregates.Channel;

[EnableAutoGeneration]
public class ChannelAggregate : MyInMemorySnapshotAggregateRoot<ChannelAggregate, ChannelId, ChannelSnapshot>
{
    private readonly ChannelState _state = new();

    public ChannelAggregate(ChannelId id) : base(id,
        SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void RemoveChannelAdmin(long adminUserId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var channelId = _state.ChannelId;
        var chatAdmins = _state.ChatAdmins;
        chatAdmins.Remove(adminUserId, out _);
        var adminList = chatAdmins.Values.ToList();

        Emit(new ChannelAdminRemovedEvent(channelId, adminUserId, adminList));
    }

    public void UpdateChannelTopMessageId(int topMessageId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ChannelTopMessageIdUpdatedEvent(_state.ChannelId, topMessageId));
    }

    public void ToggleJoinRequest(RequestInfo requestInfo, bool enabled)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        CheckAdminRights(requestInfo, r => r.ChangeInfo);
        Emit(new ChannelJoinRequestUpdatedEvent(requestInfo, _state.ChannelId, enabled));
    }
    public void ToggleParticipantsHidden(RequestInfo requestInfo, bool enabled)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        CheckAdminRights(requestInfo, r => r.ChangeInfo);
        Emit(new ChannelParticipantsHiddenUpdatedEvent(requestInfo, _state.ChannelId, enabled));
    }

    public void UpdateParticipantCount(int updatedCount)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var newParticipantCount = _state.ParticipantCount + updatedCount;
        Emit(new ChannelParticipantCountChangedEvent(_state.ChannelId, newParticipantCount));
    }

    public void DeleteChannel(RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);

        if (_state.CreatorId != requestInfo.UserId)
        {
            RpcErrors.RpcErrors400.ChatAdminRequired.ThrowRpcError();
        }

        var megagroup = !_state.Broadcast;
        Emit(new ChannelDeletedEvent(requestInfo, _state.ChannelId, _state.Broadcast, megagroup, _state.AccessHash, _state.Title));
    }

    public void CheckChannelState(
        RequestInfo requestInfo,
        long senderPeerId,
        int messageId,
        int date,
        MessageSubType messageSubType)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);

        var admin = _state.GetAdmin(senderPeerId);
        if (_state.Broadcast && _state.CreatorId != senderPeerId)
        {
            if (admin == null || !admin.AdminRights.PostMessages)
            {
                if (messageSubType != MessageSubType.InviteToChannel)
                {
                    //ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.ChatWriteForbidden);
                    RpcErrors.RpcErrors403.ChatWriteForbidden.ThrowRpcError();
                }
            }
        }

        if (messageSubType != MessageSubType.InviteToChannel)
        {
            if (_state.Broadcast)
            {
                CheckAdminRights(senderPeerId, r => r.PostMessages, RpcErrors.RpcErrors403.ChatWriteForbidden);
            }

            CheckBannedRights(senderPeerId, _state.GetDefaultBannedRights().SendMessages,
                RpcErrors.RpcErrors403.ChatWriteForbidden);
        }

        if (_state.SlowModeSeconds > 0)
        {
            if (senderPeerId == _state.LatestNonBotSenderPeerId && senderPeerId != _state.CreatorId)
            {
                var nextSendDate = _state.SlowModeSeconds + _state.LastSendDate;
                var now = DateTime.UtcNow.ToTimestamp();
                var waitForX = nextSendDate - now;
                if (waitForX > 0)
                {
                    //ThrowHelper.ThrowUserFriendlyException(string.Format(RpcErrorMessages.SlowModeWait, waitForX));
                    RpcErrors.RpcErrors420.SlowModeWaitX.ThrowRpcError(waitForX);
                }
            }
        }

        Emit(new CheckChannelStateCompletedEvent(
            requestInfo,
            senderPeerId,
            messageId,
            date,
            _state.Broadcast,
            _state.Broadcast ? 1 : 0,
            _state.BotUserIdList,
            _state.LinkedChannelId));
    }

    public void CreateChannel(RequestInfo requestInfo,
        long channelId,
        long creatorId,
        bool broadcast,
        bool megaGroup,
        string title,
        string? about,
        GeoPoint? geoPoint,
        string? address,
        long accessHash,
        int date,
        long randomId,
        IMessageAction messageAction,
        int? ttlPeriod,
        bool migratedFromChat,
        long? migratedFromChatId,
        int? migratedMaxId,
        long? photoId,
        bool autoCreateFromChat = false,
        bool ttlFromDefaultSetting = false,
        List<long>? memberUserIds = null,
        List<long>? botUserIds = null
        )
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ChannelCreatedEvent(requestInfo,
            channelId,
            creatorId,
            broadcast,
            megaGroup,
            title,
            about,
            geoPoint,
            address,
            accessHash,
            date,
            randomId,
            messageAction,
            ttlPeriod,
            migratedFromChat,
            migratedFromChatId,
            migratedMaxId,
            photoId,
            autoCreateFromChat,
            ttlFromDefaultSetting,
            memberUserIds ?? [],
            botUserIds ?? []
        ));
    }

    public void EditChannelAbout(RequestInfo requestInfo,
        long selfUserId,
        string? about)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        CheckAdminRights(requestInfo, r => r.ChangeInfo);
        CheckBannedRights(requestInfo, _state.GetDefaultBannedRights().ChangeInfo);

        if (about?.Length > MyTelegramConsts.ChatAboutMaxLength)
        {
            RpcErrors.RpcErrors400.ChatAboutTooLong.ThrowRpcError();
        }

        Emit(new ChannelAboutEditedEvent(requestInfo, _state.ChannelId, about));
    }

    public void EditChannelAdmin(RequestInfo requestInfo,
        long promotedBy,
        bool canEdit,
        long userId,
        bool isBot,
        bool isChannelMember,
        ChatAdminRights adminRights,
        string rank,
        int date,
        bool shouldCreatePermanentChatInvite
    )
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (_state.ChatAdmins.Count > MyTelegramConsts.ChannelAdminMaxCount)
        {
            RpcErrors.RpcErrors400.AdminsTooMuch.ThrowRpcError();
        }

        CheckAdminRights(requestInfo, r => r.AddAdmins);

        // flags value==0 means no rights(should remove member from admin list)
        var removeAdminFromList = adminRights.GetFlags().ToInt32() == 0;
        var isNewAdmin = !_state.ChatAdmins.ContainsKey(userId);
        var isBroadcast = _state.Broadcast;

        Emit(new ChannelAdminRightsEditedEvent(requestInfo,
            _state.ChannelId,
            promotedBy,
            canEdit,
            userId,
            isBot,
            isChannelMember,
            isNewAdmin,
            adminRights,
            rank,
            removeAdminFromList,
            date,
            isBroadcast,
            shouldCreatePermanentChatInvite
        ));
    }

    public void EditChannelDefaultBannedRights(RequestInfo requestInfo,
        ChatBannedRights defaultBannedRights,
        long selfUserId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        CheckAdminRights(requestInfo, r => r.Other);
        Emit(new ChannelDefaultBannedRightsEditedEvent(requestInfo, defaultBannedRights, _state.ChannelId));
    }

    public void EditChannelPhoto(RequestInfo requestInfo,
        long? photoId,
        IMessageAction messageAction,
        long randomId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        CheckAdminRights(requestInfo, r => r.ChangeInfo);

        Emit(new ChannelPhotoEditedEvent(requestInfo,
            _state.ChannelId,
            _state.Broadcast,
            photoId,
            messageAction,
            _state.LinkedChannelId,
            randomId
            ));
    }

    public void EditChannelTitle(RequestInfo requestInfo,
        string title,
        IMessageAction messageAction,
        long randomId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        CheckAdminRights(requestInfo, r => r.ChangeInfo);

        Emit(new ChannelTitleEditedEvent(requestInfo,
            _state.ChannelId,
            _state.Broadcast,
            title,
            _state.LinkedChannelId,
            messageAction,
            randomId
            ));
    }

    public void IncrementParticipantCount()
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var newParticipantCount = _state.ParticipantCount + 1;
        Emit(new IncrementParticipantCountEvent(_state.ChannelId, newParticipantCount));
    }

    public void ReadChannelLatestNonBotOutboxMessage(
        RequestInfo requestInfo,
        string sourceCommandId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ReadChannelLatestNoneBotOutboxMessageEvent(
            requestInfo,
            _state.LatestNonBotSenderPeerId,
            _state.LatestNonBotSenderMessageId,
            sourceCommandId));
    }

    public void SetDiscussionGroup(RequestInfo requestInfo,
        long broadcastChannelId,
        long? groupChannelId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (requestInfo.UserId != _state.CreatorId)
        {
            RpcErrors.RpcErrors400.BroadcastIdInvalid.ThrowRpcError();
        }
        var oldGroupChannelId = _state.LinkedChannelId;
        Emit(new DiscussionGroupUpdatedEvent(requestInfo, broadcastChannelId, groupChannelId, oldGroupChannelId));
    }

    [DoNotInheritRequestCommand]
    public void SetLinkedChannelId(RequestInfo requestInfo, long? linkedChannelId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var oldLinkedChannelId = _state.LinkedChannelId;
        Emit(new LinkedChannelChangedEvent(requestInfo, _state.ChannelId, linkedChannelId, oldLinkedChannelId));
    }

    public void SetPinnedMsgId(int pinnedMsgId,
        bool pinned)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new NewMsgIdPinnedEvent(pinnedMsgId, pinned));
    }

    public void SetChannelPts(long senderPeerId,
        int pts,
        int messageId,
        int date)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new SetChannelPtsEvent(senderPeerId, pts, messageId, date));
    }

    public void ToggleChannelNoForwards(RequestInfo requestInfo, bool enabled)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        CheckAdminRights(requestInfo, rights => rights.ChangeInfo);
        Emit(new ChannelNoForwardsChangedEvent(requestInfo, _state.ChannelId, enabled));
    }

    public void TogglePreHistoryHidden(RequestInfo requestInfo,
        bool hidden,
        long selfUserId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        // Only channel creator can change this setting
        CheckAdminRights(selfUserId, r => false, RpcErrors.RpcErrors400.ChatAdminRequired);
        Emit(new PreHistoryHiddenChangedEvent(requestInfo, _state.ChannelId, hidden));
    }

    public void ToggleSignature(RequestInfo requestInfo, bool signatureEnabled, bool profilesEnabled)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        CheckAdminRights(requestInfo, rights => rights.ChangeInfo);
        Emit(new ChannelSignatureChangedEvent(requestInfo, _state.ChannelId, signatureEnabled, profilesEnabled));
    }

    public void ToggleSlowMode(RequestInfo requestInfo,
        int seconds,
        long selfUserId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        CheckBannedRights(selfUserId, _state.GetDefaultBannedRights().ChangeInfo,
            RpcErrors.RpcErrors400.ChatAdminRequired);

        Emit(new SlowModeChangedEvent(requestInfo, _state.ChannelId, seconds));
    }

    public void UpdateChannelColor(RequestInfo requestInfo, PeerColor color, long? backgroundEmojiId, bool forProfile)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        CheckAdminRights(requestInfo, rights => rights.ChangeInfo);
        Emit(new ChannelColorUpdatedEvent(requestInfo, _state.ChannelId, color, backgroundEmojiId, forProfile));
    }

    public void UpdateChannelUserName(RequestInfo requestInfo,
            string userName)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var oldUserName = _state.UserName;
        Emit(new ChannelUserNameChangedEvent(requestInfo,
            _state.ChannelId,
            userName,
            oldUserName));
    }

    protected override Task<ChannelSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new ChannelSnapshot(_state.Broadcast,
            _state.ChannelId,
            _state.AccessHash,
            _state.Title,
            _state.CreatorId,
            _state.PhotoId,
            _state.PreHistoryHidden,
            _state.MaxMessageId,
            _state.BotUserIdList,
            _state.LatestNonBotSenderPeerId,
            _state.LatestNonBotSenderMessageId,
            _state.DefaultBannedRights,
            _state.SlowModeSeconds,
            _state.LastSendDate,
            _state.ChatAdmins.Select(p => p.Value).ToList(),
            _state.PinnedMsgId,
            _state.Photo,
            _state.LinkedChannelId,
            _state.UserName,
            _state.Forum,
            _state.MaxTopicId,
            _state.TtlPeriod,
            _state.MigratedFromChatId,
            _state.MigratedMaxId,
            _state.NoForwards,
            _state.IsFirstChatInviteCreated,
            _state.RequestsPending,
            _state.RecentRequesters?.ToList() ?? [],
            _state.SignatureEnabled,
            _state.ParticipantCount,
            _state.Color,
            _state.HasLink,
            _state.IsDeleted,
            _state.WallPaperId,
            _state.ThemeEmoticon,
            _state.WallPaperSettings,
            _state.IsGeoGroup,
            _state.TopMessageId,
            _state.StickerSetId,
            _state.EmojiStickerSetId,
            _state.EmojiStatus,
            _state.ParticipantsHidden,
            _state.JoinRequest
        ));
    }
    protected override Task LoadSnapshotAsync(ChannelSnapshot snapshot,
        ISnapshotMetadata metadata,
        CancellationToken cancellationToken)
    {
        _state.LoadFromSnapshot(snapshot);
        return Task.CompletedTask;
    }
    private void CheckAdminRights(RequestInfo requestInfo, Func<ChatAdminRights, bool> rightsToCheck,
        RpcError? rpcError = null /*string errorMessage = RpcErrorMessages.ChatAdminRequired*/)
    {
        CheckAdminRights(requestInfo.UserId, rightsToCheck, rpcError);
    }

    private void CheckAdminRights(long userId, Func<ChatAdminRights, bool> rightsToCheck,
        RpcError? rpcError = null /*string errorMessage = RpcErrorMessages.ChatAdminRequired*/)
    {
        if (_state.IsDeleted)
        {
            RpcErrors.RpcErrors400.ChannelPrivate.ThrowRpcError();
        }

        if (_state.CreatorId != userId)
        {
            var admin = _state.GetAdmin(userId);

            if (admin == null)
            {
                (rpcError ?? RpcErrors.RpcErrors400.ChatAdminRequired).ThrowRpcError();
            }

            if (admin!.UserId != _state.CreatorId)
            {
                var rights = rightsToCheck(admin.AdminRights);
                if (!rights)
                {
                    (rpcError ?? RpcErrors.RpcErrors400.ChatAdminRequired).ThrowRpcError();
                }
            }
        }
    }

    private void CheckBannedRights(RequestInfo requestInfo, bool bannedRights, RpcError? rpcError = null)
    {
        if (_state.IsDeleted)
        {
            RpcErrors.RpcErrors400.ChannelPrivate.ThrowRpcError();
        }

        CheckBannedRights(requestInfo.UserId, bannedRights, rpcError);
    }

    private void CheckBannedRights(long userId, bool bannedRights,
        RpcError? rpcError = null /*string errorMessage = RpcErrorMessages.ChatAdminRequired*/)
    {
        if (_state.CreatorId != userId)
        {
            if (bannedRights)
            {
                (rpcError ?? RpcErrors.RpcErrors400.ChatAdminRequired).ThrowRpcError();
            }
        }
    }
}