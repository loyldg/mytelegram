namespace MyTelegram.Domain.Aggregates.Messaging;

public class MessageAggregate : SnapshotAggregateRoot<MessageAggregate, MessageId, MessageSnapshot>
{
    private readonly MessageState _state = new();

    public MessageAggregate(MessageId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    /// <summary>
    ///     Sender's message id and receiver's message id are independent,add receiver's message id to sender,delete messages
    ///     and pin messages need this
    /// </summary>
    /// <param name="inboxOwnerPeerId"></param>
    /// <param name="inboxMessageId"></param>
    public void AddInboxMessageIdToOutboxMessage(long inboxOwnerPeerId,
        int inboxMessageId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new InboxMessageIdAddedToOutboxMessageEvent(new InboxItem(inboxOwnerPeerId, inboxMessageId)));
    }

    public void CreateInboxMessage(
        RequestInfo requestInfo,
        MessageItem inboxMessageItem,
        int senderMessageId)
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new InboxMessageCreatedEvent(requestInfo, inboxMessageItem, senderMessageId));
    }

    public void AddInboxItemsToOutboxMessage(List<InboxItem> inboxItems)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new InboxItemsAddedToOutboxMessageEvent(inboxItems));
    }

    public void CreateOutboxMessage(RequestInfo requestInfo,
        MessageItem outboxMessageItem,
        List<long>? mentionedUserIds,
        List<ReplyToMsgItem>? replyToMsgItems,
        bool clearDraft,
        int groupItemCount,
        long? linkedChannelId,
        List<long>? chatMembers)
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new OutboxMessageCreatedEvent(requestInfo,
            outboxMessageItem,
            mentionedUserIds,
            replyToMsgItems,
            clearDraft,
            groupItemCount,
            linkedChannelId,
            chatMembers
        ));
    }

    protected override Task<MessageSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new MessageSnapshot(_state.MessageItem,
            _state.InboxItems,
            _state.SenderMessageId,
            _state.Pinned,
            _state.EditDate,
            _state.Edited,
            _state.Pts,
            _state.RecentRepliers.ToList()));
    }

    public void DeleteInboxMessage(Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new InboxMessageDeletedEvent(_state.MessageItem.OwnerPeer.PeerId,
            _state.MessageItem.MessageId,
            correlationId));
    }

    public void DeleteMessage(RequestInfo requestInfo, int messageId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new MessageDeletedEvent(
            requestInfo,
            _state.MessageItem.OwnerPeer.PeerId,
            messageId,
            _state.MessageItem.IsOut,
            _state.MessageItem.SenderPeer.PeerId,
            _state.SenderMessageId,
            _state.InboxItems
        ));
    }

    public void DeleteMessage(RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new MessageDeletedEvent(
            requestInfo,
            _state.MessageItem.OwnerPeer.PeerId,
            _state.MessageItem.MessageId,
            _state.MessageItem.IsOut,
            _state.MessageItem.SenderPeer.PeerId,
            _state.SenderMessageId,
            _state.InboxItems));
    }


    public void DeleteOtherPartyMessage(RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new OtherPartyMessageDeletedEvent(
            requestInfo,
            _state.MessageItem.OwnerPeer.PeerId,
            _state.MessageItem.MessageId));
    }

    public void DeleteOutboxMessage(Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new OutboxMessageDeletedEvent(_state.MessageItem.OwnerPeer.PeerId,
            _state.MessageItem.MessageId,
            _state.InboxItems,
            correlationId));
    }

    public void DeleteSelfMessage(RequestInfo requestInfo, int messageId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new SelfMessageDeletedEvent(
            requestInfo,
            _state.MessageItem.OwnerPeer.PeerId,
            messageId,
            _state.MessageItem.IsOut,
            _state.MessageItem.SenderPeer.PeerId,
            _state.SenderMessageId,
            _state.InboxItems
        ));
    }

    public void EditInboxMessage(
        RequestInfo requestInfo,
        int messageId,
        string newMessage,
        int editDate,
        byte[]? entities,
        byte[]? media)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new InboxMessageEditedEvent(
            requestInfo,
            _state.MessageItem.OwnerPeer.PeerId,
            messageId,
            newMessage,
            entities,
            editDate,
            _state.MessageItem.ToPeer,
            media,
            null,
            null));
    }

    public void EditOutboxMessage(RequestInfo requestInfo,
        int messageId,
        string newMessage,
        int editDate,
        byte[]? entities,
        byte[]? media)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (_state.MessageItem.Date + MyTelegramServerDomainConsts.EditTimeLimit < DateTime.UtcNow.ToTimestamp())
        {
            //ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.MessageEditTimeExpired);
            RpcErrors.RpcErrors400.MessageEditTimeExpired.ThrowRpcError();
        }

        if (!_state.MessageItem.IsOut)
        {
            //ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.MessageAuthorRequired);
            RpcErrors.RpcErrors403.MessageAuthorRequired.ThrowRpcError();
        }

        Emit(new OutboxMessageEditedEvent(requestInfo,
            _state.InboxItems,
            _state.MessageItem,
            _state.MessageItem.MessageId,
            newMessage,
            editDate,
            entities,
            media,
            new List<ReactionCount>(),
            new List<Reaction>()));
    }

    public void ForwardMessage(
        RequestInfo requestInfo,
        long randomId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new MessageForwardedEvent(requestInfo, randomId, _state.MessageItem));
    }


    public void IncrementViews()
    {
        //Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (!IsNew)
        {
            Emit(new MessageViewsIncrementedEvent(_state.MessageItem.MessageId, _state.MessageItem.Views ?? 0 + 1));
        }
    }

    protected override Task LoadSnapshotAsync(MessageSnapshot snapshot,
        ISnapshotMetadata metadata,
        CancellationToken cancellationToken)
    {
        _state.LoadSnapshot(snapshot);
        return Task.CompletedTask;
    }

    public void ReadInboxHistory(RequestInfo requestInfo,
        long readerUid)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new InboxMessageHasReadEvent(requestInfo,
            readerUid,
            _state.MessageItem.MessageId,
            _state.MessageItem.SenderPeer.PeerId,
            _state.SenderMessageId,
            _state.MessageItem.ToPeer,
            _state.MessageItem.SenderPeer.PeerId == readerUid
        ));
    }

    public void ReplyToMessage( /*int messageId*/ RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ReplyToMessageEvent(requestInfo, _state.SenderMessageId, _state.InboxItems));
    }

    public void StartDeleteMessages(RequestInfo requestInfo,
        bool revoke,
        IReadOnlyList<int> idList,
        long? chatCreatorId,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new DeleteMessagesStartedEvent(requestInfo,
            _state.MessageItem.OwnerPeer.PeerId,
            _state.MessageItem.IsOut,
            _state.MessageItem.SenderPeer.PeerId,
            _state.SenderMessageId,
            _state.MessageItem.ToPeer,
            idList,
            revoke,
            _state.InboxItems,
            chatCreatorId,
            correlationId));
    }

    public void StartForwardMessage(RequestInfo requestInfo,
        Peer fromPeer,
        Peer toPeer,
        IReadOnlyList<int> idList,
        IReadOnlyList<long> randomIdList,
        bool forwardFromLinkedChannel,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ForwardMessageStartedEvent(requestInfo,
            fromPeer,
            toPeer,
            idList,
            randomIdList,
            forwardFromLinkedChannel,
            correlationId));
    }

    public void StartReplyToMessage(RequestInfo requestInfo, Peer replierPeer,
        int replyToMsgId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var recentRepliers = _state.RecentRepliers.ToList();
        recentRepliers.RemoveAll(p => p.PeerId == replierPeer.PeerId);

        if (recentRepliers.Count >= MyTelegramServerDomainConsts.MaxRecentRepliersCount)
        {
            recentRepliers.RemoveAt(MyTelegramServerDomainConsts.MaxRecentRepliersCount - 1);
        }

        recentRepliers.Insert(0, replierPeer);
        Emit(new ReplyToMessageStartedEvent(
            requestInfo,
            replyToMsgId,
            _state.MessageItem.IsOut,
            _state.InboxItems,
            _state.MessageItem.OwnerPeer,
            _state.MessageItem.SenderPeer,
            _state.MessageItem.ToPeer,
            _state.SenderMessageId,
            _state.MessageItem.FwdHeader?.SavedFromPeer?.PeerId,
            _state.MessageItem.FwdHeader?.SavedFromMsgId,
            recentRepliers));
    }


    public void StartSendMessage(RequestInfo requestInfo,
        MessageItem outMessageItem,
        List<long>? mentionedUserIds,
        bool clearDraft,
        int groupItemCount,
        bool forwardFromLinkedChannel)
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new SendMessageStartedEvent(requestInfo,
            outMessageItem,
            mentionedUserIds,
            clearDraft,
            groupItemCount,
            forwardFromLinkedChannel));
    }

    public void StartUpdatePinnedMessage(RequestInfo requestInfo,
        bool pinned,
        bool pmOneSide,
        bool silent,
        int date,
        long randomId,
        string messageActionData,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var oldPmOneSide = pmOneSide;
        if (!pinned)
        {
            oldPmOneSide = _state.PmOneSide;
        }

        var item = _state.MessageItem;
        Emit(new UpdatePinnedMessageStartedEvent(requestInfo,
            item.OwnerPeer.PeerId,
            item.MessageId,
            pinned,
            oldPmOneSide,
            silent,
            date,
            item.IsOut,
            _state.InboxItems,
            item.SenderPeer.PeerId,
            _state.SenderMessageId,
            item.ToPeer,
            randomId,
            messageActionData,
            correlationId
        ));
    }

    public void UpdateInboxMessagePinned(
        RequestInfo requestInfo,
        bool pinned,
        bool pmOneSide,
        bool silent,
        int date)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var item = _state.MessageItem;
        Emit(new InboxMessagePinnedUpdatedEvent(
            requestInfo,
            item.OwnerPeer.PeerId,
            item.MessageId,
            pinned,
            pmOneSide,
            silent,
            date,
            item.ToPeer,
            _state.Pts));
    }

    public void UpdateOutboxMessagePinned(
        RequestInfo requestInfo,
        bool pinned,
        bool pmOneSide,
        bool silent,
        int date)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var item = _state.MessageItem;
        Emit(new OutboxMessagePinnedUpdatedEvent(
            requestInfo,
            item.OwnerPeer.PeerId,
            item.MessageId,
            pinned,
            pmOneSide,
            silent,
            date,
            _state.InboxItems,
            item.SenderPeer.PeerId,
            _state.SenderMessageId,
            item.ToPeer,
            _state.Pts
        ));
    }
}
