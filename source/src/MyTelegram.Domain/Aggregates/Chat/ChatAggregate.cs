namespace MyTelegram.Domain.Aggregates.Chat;

public class ChatAggregate : MyInMemorySnapshotAggregateRoot<ChatAggregate, ChatId, ChatSnapshot>
{
    private readonly ChatState _state = new();

    public ChatAggregate(ChatId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void EditAdmin(RequestInfo requestInfo,
        long promotedBy,
        bool canEdit,
        long userId,
        bool isBot,
        ChatAdminRights adminRights,
        string rank,
        int date
    )
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (_state.ChatAdmins.Count > MyTelegramServerDomainConsts.ChannelAdminMaxCount)
        {
            RpcErrors.RpcErrors400.AdminsTooMuch.ThrowRpcError();
        }

        CheckAdminRights(requestInfo, r => r.AddAdmins);
        //var admin = _state.GetAdmin(selfUserId);
        //CheckBannedRights(selfUserId, true, admin?.AdminRights.AddAdmins, RpcErrorMessages.ChatAdminRequired);

        // flags value==0 means no rights(should remove member from admin list)
        bool removeFromAdminList = adminRights.GetFlags().ToInt() == 0;
        bool isNewAdmin = !_state.ChatAdmins.ContainsKey(userId);

        Emit(new ChatAdminRightsEditedEvent(requestInfo,
            _state.ChatId,
            promotedBy,
            canEdit,
            userId,
            isBot,
            isNewAdmin,
            adminRights,
            rank,
            removeFromAdminList,
            date
        ));
    }

    public void AddChatUser(
        RequestInfo requestInfo,
        long inviterUserId,
        long userId,
        int date,
        string messageActionData,
        long randomId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);

        //CheckBannedRights(_state.GetDefaultBannedRights().InviteUsers,
        //    RpcErrorMessages.ChatAdminRequired,
        //    inviterUserId);

        CheckAdminRights(requestInfo, (rights => rights.InviteUsers));

        if (_state.ChatMembers.Count > MyTelegramServerDomainConsts.ChatMemberMaxCount)
        {
            RpcErrors.RpcErrors400.UsersTooMuch.ThrowRpcError();
        }

        if (_state.ChatMembers.ContainsKey(userId))
        {
            RpcErrors.RpcErrors400.UserAlreadyParticipant.ThrowRpcError();
        }

        var allMembers = _state.ChatMembers.Select(p => p.Key).ToList();
        allMembers.Add(userId);

        Emit(new ChatMemberAddedEvent(requestInfo,
            _state.ChatId,
            new ChatMember(userId, inviterUserId, date),
            messageActionData,
            randomId, allMembers));
    }

    private void CheckBannedRights(RequestInfo requestInfo, bool bannedRights, bool? adminRights, string error)
    {

    }

    private void CheckBannedRights(long selfUserId, bool bannedRights,
        bool? adminRights,
        RpcError? rpcError = null)
    {
        if (_state.CreatorId != selfUserId)
        {
            if (adminRights.HasValue && adminRights.Value)
            {
                return;
            }

            if (bannedRights)
            {
                //ThrowHelper.ThrowUserFriendlyException(error);
                (rpcError ?? RpcErrors.RpcErrors400.ChatAdminRequired).ThrowRpcError();
            }
        }
    }

    //private void CheckBannedRights(bool bannedRights,
    //    string error,
    //    long selfUserId)
    //{
    //    if (_state.CreatorUid != selfUserId)
    //    {
    //        if (bannedRights)
    //        {
    //            ThrowHelper.ThrowUserFriendlyException(error);
    //        }
    //    }
    //}

    public void CheckChatState(RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new CheckChatStateCompletedEvent(requestInfo, _state.Title, _state.ChatMembers.Keys.ToList()));
    }

    public void Create(RequestInfo requestInfo,
        long chatId,
        long creatorUid,
        string title,
        IReadOnlyList<long> memberUidList,
        int date,
        long randomId,
        string messageActionData,
        int? ttlPeriod
    )
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        if (memberUidList.Count > MyTelegramServerDomainConsts.ChatMemberMaxCount)
        {
            RpcErrors.RpcErrors400.UsersTooMuch.ThrowRpcError();
        }

        var memberList = memberUidList.Select(p => new ChatMember(p, creatorUid, date)).ToList();
        memberList.Insert(0, new ChatMember(creatorUid, creatorUid, date));
        Emit(new ChatCreatedEvent(requestInfo,
            chatId,
            creatorUid,
            title,
            memberList,
            date,
            randomId,
            messageActionData,
            ttlPeriod));
    }

    protected override Task<ChatSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new ChatSnapshot(_state.ChatId,
            _state.Title,
            _state.CreatorId,
            _state.PhotoId,
            _state.ChatMembers.Values.ToList(),
            _state.ChatAdmins.Values.ToList(),
            _state.BotUserIdList,
            _state.LatestSenderPeerId,
            _state.LatestSenderMessageId,
            _state.LatestDeletedMemberUid,
            _state.DefaultBannedRights,
            _state.About,
            _state.TtlPeriod,
            _state.MigrateToChannelId,
            _state.NoForwards
        ));
    }

    public void DeleteChat(RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (requestInfo.UserId != _state.CreatorId)
        {
            RpcErrors.RpcErrors400.ChatAdminRequired.ThrowRpcError();
        }

        Emit(new ChatDeletedEvent(requestInfo, _state.ChatId, _state.Title));
    }

    public void DeleteChatUser(
        RequestInfo requestInfo,
        long userId,
        string messageActionData,
        long randomId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (_state.CreatorId != requestInfo.UserId)
        {
            if (requestInfo.UserId != userId)
            {
                RpcErrors.RpcErrors400.ChatAdminRequired.ThrowRpcError();
            }
        }

        Emit(new ChatMemberDeletedEvent(requestInfo,
            _state.ChatId,
            userId,
            messageActionData,
            randomId
            ));
    }

    public void EditAbout(RequestInfo requestInfo,
        long selfUserId,
        string? about)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (selfUserId != _state.CreatorId)
        {
            RpcErrors.RpcErrors400.ChatAdminRequired.ThrowRpcError();
        }

        if (about?.Length > MyTelegramServerDomainConsts.ChatAboutMaxLength)
        {
            RpcErrors.RpcErrors400.ChatAboutTooLong.ThrowRpcError();
        }

        Emit(new ChatAboutEditedEvent(requestInfo, about));
    }

    public void EditChatDefaultBannedRights(RequestInfo requestInfo,
        ChatBannedRights bannedRights,
        long selfUserId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        CheckAdminRights(selfUserId, r => r.ChangeInfo);
        //CheckBannedRights(_state.GetDefaultBannedRights().ChangeInfo, RpcErrorMessages.ChatAdminRequired, selfUserId);


        //var value = bannedRights.ToIntValue();
        //var lastValue = _state.GetDefaultBannedRights().ToIntValue();
        //if (value == lastValue)
        //{
        //    ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.ChatNotModified);
        //}

        Emit(new ChatDefaultBannedRightsEditedEvent(requestInfo, _state.ChatId, bannedRights, Version));
    }

    public void EditPhoto(RequestInfo requestInfo,
        //byte[] photo,
        long photoId,
        string messageActionData,
        long randomId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        CheckAdminRights(requestInfo, r => r.ChangeInfo);
        Emit(new ChatPhotoEditedEvent(requestInfo,
            _state.ChatId,
            photoId,
            //photo,
            messageActionData,
            randomId
            ));
    }

    public void EditTitle(RequestInfo requestInfo,
        string title,
        string messageActionData,
        long randomId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        //CheckBannedRights(_state.GetDefaultBannedRights().ChangeInfo,
        //    RpcErrorMessages.ChatAdminRequired,
        //    requestInfo.UserId);
        CheckAdminRights(requestInfo, r => r.ChangeInfo);

        Emit(new ChatTitleEditedEvent(requestInfo,
            _state.ChatId,
            title,
            messageActionData,
            randomId));
    }

    protected override Task LoadSnapshotAsync(ChatSnapshot snapshot,
        ISnapshotMetadata metadata,
        CancellationToken cancellationToken)
    {
        _state.LoadSnapshot(snapshot);
        return Task.CompletedTask;
    }

    public void ReadLatestNoneBotOutboxMessage(RequestInfo requestInfo, string sourceCommandId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ReadLatestNoneBotOutboxMessageEvent(requestInfo, _state.ChatId,
            _state.LatestSenderPeerId,
            _state.LatestSenderMessageId,
            sourceCommandId));
    }

    public void SetPinnedMsgId(int pinnedMsgId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new NewChatMsgIdPinnedEvent(pinnedMsgId));
    }

    public void StartClearGroupChatHistory(RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ClearGroupChatHistoryStartedEvent(requestInfo, _state.ChatId, _state.ChatMembers.Values.ToList()));
    }

    public void StartDeleteChatMessages(RequestInfo requestInfo,
        List<int> messageIds,
        bool revoke,
        bool isClearHistory)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new DeleteChatMessagesStartedEvent(requestInfo,
            messageIds,
            revoke,
            _state.CreatorId,
            _state.ChatMembers.Count,
            isClearHistory));
    }

    public void StartSendChatMessage(
        RequestInfo requestInfo,
        long senderPeerId,
        int senderMessageId,
        bool senderIsBot)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new StartSendChatMessageEvent(
            requestInfo,
            _state.ChatId,
            _state.Title,
            _state.ChatMembers.Keys.ToList(),
            senderPeerId,
            senderMessageId,
            senderIsBot,
            _state.LatestDeletedMemberUid));
    }

    #region Check admin rights and banned rights



    private void CheckAdminRights(RequestInfo requestInfo, Func<ChatAdminRights, bool> rightsToCheck, RpcError? rpcError = null)
    {
        CheckAdminRights(requestInfo.UserId, rightsToCheck, rpcError);
    }

    private void CheckAdminRights(long userId, Func<ChatAdminRights, bool> rightsToCheck, RpcError? rpcError = null)
    {
        if (_state.CreatorId != userId)
        {
            var admin = _state.GetAdmin(userId);

            if (admin == null)
            {
                //ThrowHelper.ThrowUserFriendlyException(errorMessage);
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
        CheckBannedRights(requestInfo.UserId, bannedRights, rpcError);
    }

    private void CheckBannedRights(long userId, bool bannedRights, RpcError? rpcError = null)
    {
        if (_state.CreatorId != userId)
        {
            if (bannedRights)
            {
                (rpcError ?? RpcErrors.RpcErrors400.ChatAdminRequired).ThrowRpcError();
            }
        }
    }
    #endregion

}
