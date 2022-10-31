namespace MyTelegram.Domain.Aggregates.Chat;

public class ChatAggregate : MyInMemorySnapshotAggregateRoot<ChatAggregate, ChatId, ChatSnapshot>
{
    private readonly ChatState _state = new();

    public ChatAggregate(ChatId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void StartDeleteChatMessages(RequestInfo requestInfo,
        List<int> messageIds,
        bool revoke,
        bool isClearHistory,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new DeleteChatMessagesStartedEvent(requestInfo, messageIds, revoke, _state.CreatorUid, _state.ChatMembers.Count, isClearHistory, correlationId));
    }
    public void DeleteChat(RequestInfo request, Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (request.UserId != _state.CreatorUid)
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.ChatAdminRequired);
        }
        Emit(new ChatDeletedEvent(request, _state.ChatId, _state.Title, correlationId));
    }
    public void AddChatUser(
        RequestInfo request,
        long inviterUserId,
        long userId,
        int date,
        string messageActionData,
        long randomId,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        CheckBannedRights(_state.GetDefaultBannedRights().InviteUsers, RpcErrorMessages.ChatAdminRequired, inviterUserId);

        if (_state.ChatMembers.Count > MyTelegramServerDomainConsts.ChatMemberMaxCount)
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.UsersTooMuch);
        }

        if (_state.MemberUidList.Contains(userId))
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.UserAlreadyParticipant);
        }

        Emit(new ChatMemberAddedEvent(request,
            _state.ChatId,
            new ChatMember(userId, inviterUserId, date),
            messageActionData,
            randomId,
            correlationId));
    }

    private void CheckBannedRights(bool bannedRights,
        string error,
        long selfUserId)
    {
        if (_state.CreatorUid != selfUserId)
        {
            if (bannedRights)
            {
                ThrowHelper.ThrowUserFriendlyException(error);
            }
        }
    }

    public void CheckChatState(Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new CheckChatStateCompletedEvent(_state.Title, _state.MemberUidList, correlationId));
    }

    public void Create(RequestInfo request,
        long chatId,
        long creatorUid,
        string title,
        IReadOnlyList<long> memberUidList,
        int date,
        long randomId,
        string messageActionData,
        Guid correlationId)
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        if (memberUidList.Count > MyTelegramServerDomainConsts.ChatMemberMaxCount)
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.UsersTooMuch);
        }

        var memberList = memberUidList.Select(p => new ChatMember(p, creatorUid, date)).ToList();
        memberList.Insert(0, new ChatMember(creatorUid, creatorUid, date));
        Emit(new ChatCreatedEvent(request,
            chatId,
            creatorUid,
            title,
            memberList,
            date,
            randomId,
            messageActionData,
            correlationId));
    }

    protected override Task<ChatSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new ChatSnapshot(_state.ChatId,
            _state.Title,
            _state.CreatorUid,
            _state.ChatMembers,
            _state.LatestSenderPeerId,
            _state.LatestSenderMessageId,
            _state.LatestDeletedMemberUid,
            _state.DefaultBannedRights,
            _state.About
        ));
    }

    public void DeleteChatUser(
        RequestInfo request,
        long userId,
        string messageActionData,
        long randomId,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (_state.CreatorUid != request.UserId)
        {
            if (request.UserId != userId)
            {
                ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.ChatAdminRequired);
            }
        }

        Emit(new ChatMemberDeletedEvent(request,
            _state.ChatId,
            userId,
            messageActionData,
            randomId,
            correlationId));
    }

    public void EditAbout(long reqMsgId,
        long selfUserId,
        string? about)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (selfUserId != _state.CreatorUid)
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.ChatAdminRequired);
        }

        if (about?.Length > MyTelegramServerDomainConsts.ChatAboutMaxLength)
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.ChatAboutTooLong);
        }

        Emit(new ChatAboutEditedEvent(reqMsgId, about));
    }

    public void EditChatDefaultBannedRights(long reqMsgId,
        ChatBannedRights bannedRights,
        long selfUserId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        CheckBannedRights(_state.GetDefaultBannedRights().ChangeInfo, RpcErrorMessages.ChatAdminRequired, selfUserId);

        var value = bannedRights.ToIntValue();
        var lastValue = _state.GetDefaultBannedRights().ToIntValue();
        if (value == lastValue)
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.ChatNotModified);
        }

        Emit(new ChatDefaultBannedRightsEditedEvent(reqMsgId, _state.ChatId, bannedRights, Version));
    }

    public void EditPhoto(RequestInfo request,
        byte[] photo,
        string messageActionData,
        long randomId,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ChatPhotoEditedEvent(request,
            _state.ChatId,
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
        CheckBannedRights(_state.GetDefaultBannedRights().ChangeInfo, RpcErrorMessages.ChatAdminRequired, request.UserId);

        Emit(new ChatTitleEditedEvent(request,
            _state.ChatId,
            title,
            messageActionData,
            randomId,
            correlationId));
    }

    protected override Task LoadSnapshotAsync(ChatSnapshot snapshot,
        ISnapshotMetadata metadata,
        CancellationToken cancellationToken)
    {
        _state.LoadSnapshot(snapshot);
        return Task.CompletedTask;
    }

    public void ReadLatestNoneBotOutboxMessage(string sourceCommandId,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ReadLatestNoneBotOutboxMessageEvent(_state.ChatId,
            _state.LatestSenderPeerId,
            _state.LatestSenderMessageId,
            sourceCommandId,
            correlationId));
    }

    public void SetPinnedMsgId(int pinnedMsgId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new NewChatMsgIdPinnedEvent(pinnedMsgId));
    }

    public void StartClearGroupChatHistory(Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ClearGroupChatHistoryStartedEvent(_state.ChatId, _state.ChatMembers, correlationId));
    }

    public void StartSendChatMessage(
        long senderPeerId,
        int senderMessageId,
        bool senderIsBot,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new StartSendChatMessageEvent(_state.ChatId,
            _state.Title,
            _state.MemberUidList,
            senderPeerId,
            senderMessageId,
            senderIsBot,
            _state.LatestDeletedMemberUid,
            correlationId));
    }
}
