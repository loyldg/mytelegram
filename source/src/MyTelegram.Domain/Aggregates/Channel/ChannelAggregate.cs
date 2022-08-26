namespace MyTelegram.Domain.Aggregates.Channel;

public class ChannelAggregate : MyInMemorySnapshotAggregateRoot<ChannelAggregate, ChannelId, ChannelSnapshot>
{
    private readonly ChannelState _state = new();

    public ChannelAggregate(ChannelId id) : base(id,
        SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void CheckChannelState(long senderPeerId,
        int messageId,
        int date,
        MessageSubType messageSubType,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (_state.Broadcast && _state.CreatorId != senderPeerId)
        {
            var admin = _state.GetAdmin(senderPeerId);
            if (admin == null || !admin.AdminRights.PostMessages)
            {
                if (messageSubType != MessageSubType.InviteToChannel)
                {
                    ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.ChatWriteForbidden);
                }
            }
        }

        if (messageSubType != MessageSubType.InviteToChannel)
        {
            CheckBannedRights(senderPeerId,
                _state.GetDefaultBannedRights().SendMessages,
                false,
                RpcErrorMessages.ChatWriteForbidden);
        }

        if (_state.SlowModeSeconds > 0)
        {
            if (senderPeerId == _state.LatestNoneBotSenderPeerId && senderPeerId != _state.CreatorId)
            {
                var nextSendDate = _state.SlowModeSeconds + _state.LastSendDate;
                var now = DateTime.UtcNow.ToTimestamp();
                var waitForX = nextSendDate - now;
                if (waitForX > 0)
                {
                    ThrowHelper.ThrowUserFriendlyException(string.Format(RpcErrorMessages.SlowModeWait, waitForX));
                }
            }
        }

        Emit(new CheckChannelStateCompletedEvent(senderPeerId,
            messageId,
            date,
            _state.Broadcast,
            _state.Broadcast ? 1 : 0,
            _state.BotUidList,
            _state.LinkedChannelId,
            correlationId));
    }

    public void Create(RequestInfo request,
        long channelId,
        long creatorId,
        bool broadcast,
        bool megaGroup,
        string title,
        string? about,
        string? address,
        long accessHash,
        int date,
        long randomId,
        string messageActionData,
        Guid correlationId)
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ChannelCreatedEvent(request,
            channelId,
            creatorId,
            title,
            broadcast,
            megaGroup,
            about,
            address,
            accessHash,
            date,
            randomId,
            messageActionData,
            correlationId));
    }

    public void EditAbout(long reqMsgId,
        long selfUserId,
        string? about)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var admin = _state.GetAdmin(selfUserId);
        CheckBannedRights(selfUserId,
            _state.GetDefaultBannedRights().ChangeInfo,
            admin?.AdminRights.ChangeInfo,
            RpcErrorMessages.ChatAdminRequired);

        if (about?.Length > MyTelegramServerDomainConsts.ChatAboutMaxLength)
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.ChatAboutTooLong);
        }

        Emit(new ChannelAboutEditedEvent(reqMsgId, about));
    }

    public void EditAdmin(long reqMsgId,
        long selfUserId,
        long promotedBy,
        bool canEdit,
        long userId,
        ChatAdminRights adminRights,
        string rank)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (_state.AdminList.Count > MyTelegramServerDomainConsts.ChannelAdminMaxCount)
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.AdminsTooMuch);
        }

        var admin = _state.GetAdmin(selfUserId);
        CheckBannedRights(selfUserId, true, admin?.AdminRights.AddAdmins, RpcErrorMessages.ChatAdminRequired);
        Emit(new ChannelAdminRightsEditedEvent(reqMsgId,
            _state.ChannelId,
            promotedBy,
            canEdit,
            userId,
            adminRights,
            rank));
    }

    public void EditChatDefaultBannedRights(long reqMsgId,
        ChatBannedRights bannedRights,
        long selfUserId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        CheckBannedRights(selfUserId, true, false, RpcErrorMessages.ChatAdminRequired);

        var value = bannedRights.ToIntValue();
        var lastValue = _state.GetDefaultBannedRights().ToIntValue();
        if (value == lastValue)
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.ChatNotModified);
        }

        Emit(new ChannelDefaultBannedRightsEditedEvent(reqMsgId, _state.ChannelId, bannedRights));
    }

    public void EditPhoto(RequestInfo request,
        //long fileId,
        byte[] photo,
        string messageActionData,
        long randomId,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ChannelPhotoEditedEvent(request,
            _state.ChannelId,
            photo,
            messageActionData,
            randomId,
            correlationId));
    }

    public void EditTitle(RequestInfo request,
        string title,
        string messageActionData,
        long randomId,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var admin = _state.GetAdmin(request.UserId);
        CheckBannedRights(request.UserId,
            _state.GetDefaultBannedRights().ChangeInfo,
            admin?.AdminRights.ChangeInfo,
            RpcErrorMessages.ChatAdminRequired);

        Emit(new ChannelTitleEditedEvent(request,
            _state.ChannelId,
            title,
            messageActionData,
            randomId,
            correlationId));
    }

    public void ExportChatInvite(long reqMsgId,
        long adminId,
        int? expireDate,
        int? usageLimit,
        //bool legacyRevokePermanent,
        string randomLink,
        int date
    )
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (_state.CreatorId != adminId)
        {
            ThrowHelper.ThrowUserFriendlyException("Only admin can export chat invite");
        }

        var link = $"AAAAA{_state.ChannelId}/{randomLink}";
        Emit(new ExportChatInviteEvent(reqMsgId,
            _state.ChannelId,
            adminId,
            expireDate,
            usageLimit,
            false,
            link,
            true,
            date,
            date));
    }

    public void IncrementParticipantCount()
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new IncrementParticipantCountEvent());
    }

    public void ReadChannelLatestNoneBotOutboxMessage(string sourceCommandId,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ReadChannelLatestNoneBotOutboxMessageEvent(_state.LatestNoneBotSenderPeerId,
            _state.LatestNoneBotSenderMessageId,
            sourceCommandId,
            correlationId));
    }

    public void SetDiscussionGroup(long reqMsgId,
        long selfUserId,
        long broadcastChannelId,
        long? groupChannelId)
    {
        // TODO:Use saga to set discussion group id,check whether the groupChannelId is valid
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (selfUserId != _state.CreatorId)
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.BroadcastIdInvalid);
        }

        Emit(new SetDiscussionGroupEvent(reqMsgId, broadcastChannelId, groupChannelId));
    }

    public void SetPinnedMsgId(int pinnedMsgId,
        bool pinned)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new NewMsgIdPinnedEvent(pinnedMsgId, pinned));
    }

    public void SetPts(long senderPeerId,
        int pts,
        int messageId,
        int date)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new SetChannelPtsEvent(senderPeerId, pts, messageId, date));
    }

    public void StartInviteToChannel(RequestInfo request,
        long inviterId,
        IReadOnlyList<long> memberUidList,
        IReadOnlyList<long> botUidList,
        int date,
        long randomId,
        string messageActionData,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);

        // self join channel
        if (memberUidList.Count == 1 && memberUidList.ElementAt(0) == inviterId)
        {
        }
        else
        {
            var admin = _state.GetAdmin(inviterId);
            CheckBannedRights(inviterId,
                _state.GetDefaultBannedRights().InviteUsers,
                admin?.AdminRights.InviteUsers,
                RpcErrorMessages.ChatAdminRequired);
        }

        Emit(new StartInviteToChannelEvent(request,
            _state.ChannelId,
            inviterId,
            memberUidList,
            botUidList,
            date,
            _state.MaxMessageId,
            _state.PreHistoryHidden ? _state.MaxMessageId : 0,
            randomId,
            messageActionData,
            _state.Broadcast,
            correlationId));
    }

    public void StartSendChannelMessage( //long reqMsgId,
        long senderPeerId,
        bool senderIsBot,
        int messageId,
        MessageSubType subType,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (_state.Broadcast && _state.CreatorId != senderPeerId)
        {
            var admin = _state.GetAdmin(senderPeerId);
            if (admin == null || !admin.AdminRights.PostMessages)
            {
                if (subType != MessageSubType.InviteToChannel)
                {
                    ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.ChatWriteForbidden);
                }
            }
        }

        if (subType != MessageSubType.InviteToChannel)
        {
            CheckBannedRights(senderPeerId,
                _state.GetDefaultBannedRights().SendMessages,
                false,
                RpcErrorMessages.ChatWriteForbidden);
        }

        if (_state.SlowModeSeconds > 0)
        {
            if (senderPeerId == _state.LatestNoneBotSenderPeerId && senderPeerId != _state.CreatorId)
            {
                var nextSendDate = _state.SlowModeSeconds + _state.LastSendDate;
                var now = DateTime.UtcNow.ToTimestamp();
                var waitForX = nextSendDate - now;
                if (waitForX > 0)
                {
                    ThrowHelper.ThrowUserFriendlyException(string.Format(RpcErrorMessages.SlowModeWait, waitForX));
                }
            }
        }

        Emit(new StartSendChannelMessageEvent( //reqMsgId,
            senderPeerId,
            senderIsBot,
            _state.Broadcast,
            _state.Broadcast ? 1 : 0,
            messageId,
            _state.BotUidList,
            _state.LinkedChannelId,
            DateTime.UtcNow.ToTimestamp(),
            correlationId));
    }

    public void TogglePreHistoryHidden(long reqMsgId,
        bool hidden,
        long selfUserId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        // only channel creator can change this setting
        CheckBannedRights(selfUserId, true, false, RpcErrorMessages.ChatAdminRequired);
        Emit(new PreHistoryHiddenChangedEvent(reqMsgId, _state.ChannelId, hidden));
    }

    public void ToggleSlowMode(long reqMsgId,
        int seconds,
        long selfUserId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        CheckBannedRights(selfUserId,
            _state.GetDefaultBannedRights().ChangeInfo,
            false,
            RpcErrorMessages.ChatAdminRequired);

        Emit(new SlowModeChangedEvent(reqMsgId, _state.ChannelId, seconds));
    }

    public void UpdateUserName(long reqMsgId,
        string userName,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ChannelUserNameChangedEvent(reqMsgId,
            _state.ChannelId,
            userName,
            _state.UserName,
            correlationId));
    }

    protected override Task<ChannelSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new ChannelSnapshot(_state.Broadcast,
            _state.ChannelId,
            _state.CreatorId,
            _state.PreHistoryHidden,
            _state.MaxMessageId,
            _state.BotUidList,
            _state.LatestNoneBotSenderPeerId,
            _state.LatestNoneBotSenderMessageId,
            _state.DefaultBannedRights,
            _state.SlowModeSeconds,
            _state.LastSendDate,
            //_state.LastSenderPeerId,
            _state.AdminList,
            _state.PinnedMsgId,
            _state.Photo,
            _state.LinkedChannelId,
            _state.UserName));
    }

    protected override Task LoadSnapshotAsync(ChannelSnapshot snapshot,
        ISnapshotMetadata metadata,
        CancellationToken cancellationToken)
    {
        _state.LoadFromSnapshot(snapshot);
        return Task.CompletedTask;
    }

    private void CheckBannedRights(long selfUserId, bool bannedRights,
        bool? adminRights,
        string error)
    {
        if (_state.CreatorId != selfUserId)
        {
            if (adminRights.HasValue && adminRights.Value)
            {
                return;
            }

            if (bannedRights)
            {
                ThrowHelper.ThrowUserFriendlyException(error);
            }
        }
    }
}
