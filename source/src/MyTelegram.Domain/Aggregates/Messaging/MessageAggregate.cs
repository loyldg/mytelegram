namespace MyTelegram.Domain.Aggregates.Messaging;

public class MessageAggregate : AggregateRoot<MessageAggregate, MessageId>
{
    private readonly MessageState _state = new();

    public MessageAggregate(MessageId id) : base(id)
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

    public void CreateInboxMessage(MessageItem inboxMessageItem,
        int senderMessageId,
        Guid correlationId)
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new InboxMessageCreatedEvent(inboxMessageItem, senderMessageId, correlationId));
    }

    public void CreateOutboxMessage(long reqMsgId,
        MessageItem outboxMessageItem,
        bool clearDraft,
        int groupItemCount,
        long? linkedChannelId,
        Guid correlationId)
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new OutboxMessageCreatedEvent(reqMsgId,
            outboxMessageItem,
            clearDraft,
            groupItemCount,
            linkedChannelId,
            correlationId));
    }

    public void DeleteMessage(int messageId,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new MessageDeletedEvent(_state.MessageItem.OwnerPeer.PeerId,
            messageId,
            _state.MessageItem.IsOut,
            _state.MessageItem.SenderPeer.PeerId,
            _state.SenderMessageId,
            _state.InboxItems,
            correlationId
        ));
    }

    public void DeleteMessage(Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new MessageDeletedEvent(_state.MessageItem.OwnerPeer.PeerId,
            _state.MessageItem.MessageId,
            _state.MessageItem.IsOut,
            _state.MessageItem.SenderPeer.PeerId,
            _state.SenderMessageId,
            _state.InboxItems,
            correlationId));
    }

    public void DeleteOtherPartyMessage(Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new OtherPartyMessageDeletedEvent(_state.MessageItem.OwnerPeer.PeerId,
            _state.MessageItem.MessageId,
            correlationId));
    }

    public void EditInboxMessage(
        int messageId,
        string newMessage,
        int editDate,
        byte[]? entities,
        byte[]? media,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new InboxMessageEditedEvent(_state.MessageItem.OwnerPeer.PeerId,
            messageId,
            newMessage,
            entities,
            editDate,
            _state.MessageItem.ToPeer,
            media,
            correlationId));
    }

    public void EditOutboxMessage(RequestInfo request,
        int messageId,
        string newMessage,
        int editDate,
        byte[]? entities,
        byte[]? media,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (_state.MessageItem.Date + MyTelegramServerDomainConsts.EditTimeLimit < DateTime.UtcNow.ToTimestamp())
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.MessageEditTimeExpired);
        }

        if (!_state.MessageItem.IsOut)
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.MessageAuthorRequired);
        }

        Emit(new OutboxMessageEditedEvent(request,
            _state.InboxItems,
            _state.MessageItem,
            _state.MessageItem.MessageId,
            newMessage,
            editDate,
            entities,
            media,
            correlationId));
    }

    public void ForwardMessage(
        RequestInfo request,
        long randomId,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new MessageForwardedEvent(request, randomId, _state.MessageItem, correlationId));
    }

    public void IncrementViews()
    {
        //Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (!IsNew)
        {
            Emit(new MessageViewsIncrementedEvent(_state.MessageItem.MessageId, _state.MessageItem.Views ?? 0 + 1));
        }
    }

    public void ReadInboxHistory(long reqMsgId,
        long readerUid,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new InboxMessageHasReadEvent(reqMsgId,
            readerUid,
            _state.MessageItem.MessageId,
            _state.MessageItem.SenderPeer.PeerId,
            _state.SenderMessageId,
            _state.MessageItem.ToPeer,
            _state.MessageItem.SenderPeer.PeerId == readerUid,
            correlationId
        ));
    }

    public void ReplyToMessage( /*int messageId*/ Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ReplyToMessageEvent(_state.SenderMessageId, _state.InboxItems, correlationId));
    }

    public void StartDeleteMessages(RequestInfo request,
        bool revoke,
        IReadOnlyList<int> idList,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new DeleteMessagesStartedEvent(request,
            _state.MessageItem.OwnerPeer.PeerId,
            _state.MessageItem.IsOut,
            _state.MessageItem.SenderPeer.PeerId,
            _state.SenderMessageId,
            _state.MessageItem.ToPeer,
            idList,
            revoke,
            _state.InboxItems,
            correlationId));
    }

    public void StartForwardMessage(RequestInfo request,
        Peer fromPeer,
        Peer toPeer,
        IReadOnlyList<int> idList,
        IReadOnlyList<long> randomIdList,
        bool forwardFromLinkedChannel,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ForwardMessageStartedEvent(request,
            fromPeer,
            toPeer,
            idList,
            randomIdList,
            forwardFromLinkedChannel,
            correlationId));
    }

    public void StartReplyToMessage(Peer replierPeer,
        int replyToMsgId,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var recentRepliers = _state.RecentRepliers.ToList();
        recentRepliers.RemoveAll(p => p.PeerId == replierPeer.PeerId);
        if (recentRepliers.Count >= MyTelegramServerDomainConsts.MaxRecentRepliersCount)
        {
            recentRepliers.RemoveAt(MyTelegramServerDomainConsts.MaxRecentRepliersCount - 1);
        }
        recentRepliers.Insert(0, replierPeer);
        Emit(new ReplyToMessageStartedEvent(replyToMsgId,
            _state.MessageItem.IsOut,
            _state.InboxItems,
            _state.MessageItem.OwnerPeer,
            _state.MessageItem.SenderPeer,
            _state.MessageItem.ToPeer,
            _state.SenderMessageId,
            _state.MessageItem.FwdHeader?.SavedFromPeer?.PeerId,
            _state.MessageItem.FwdHeader?.SavedFromMsgId,
            recentRepliers,
            correlationId));
    }

    public void StartSendMessage(RequestInfo request,
        MessageItem outMessageItem,
        bool clearDraft,
        int groupItemCount,
        bool forwardFromLinkedChannel,
        Guid correlationId)
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new SendMessageStartedEvent(request,
            outMessageItem,
            clearDraft,
            groupItemCount,
            forwardFromLinkedChannel,
            correlationId));
    }

    public void StartUpdatePinnedMessage(RequestInfo request,
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
        Emit(new UpdatePinnedMessageStartedEvent(request,
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

    public void UpdateInboxMessagePinned(bool pinned,
        bool pmOneSide,
        bool silent,
        int date,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var item = _state.MessageItem;
        Emit(new InboxMessagePinnedUpdatedEvent(item.OwnerPeer.PeerId,
            item.MessageId,
            pinned,
            pmOneSide,
            silent,
            date,
            item.ToPeer,
            _state.Pts,
            correlationId));
    }

    public void UpdateOutboxMessagePinned(bool pinned,
        bool pmOneSide,
        bool silent,
        int date,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var item = _state.MessageItem;
        Emit(new OutboxMessagePinnedUpdatedEvent(item.OwnerPeer.PeerId,
            item.MessageId,
            pinned,
            pmOneSide,
            silent,
            date,
            _state.InboxItems,
            item.SenderPeer.PeerId,
            _state.SenderMessageId,
            item.ToPeer,
            _state.Pts,
            correlationId
        ));
    }
}
