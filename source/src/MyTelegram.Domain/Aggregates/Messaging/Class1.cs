//namespace MyTelegram.Domain.Aggregates.Messaging;





//[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<MessageId>))]
//public class MessageId : MyIdentity<MessageId>
//{
//    public MessageId(string value) : base(value)
//    {
//    }

//    public static MessageId Create(long ownerPeerId, int messageId)
//    {
//        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands,
//            $"message_box_{ownerPeerId}_{messageId}");
//    }

//    public static MessageId CreateWithRandomId(long ownerPeerId,
//        long randomId)
//    {
//        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands,
//            $"message_box_{ownerPeerId}_randomId_{randomId}");
//    }
//}

//public class OutboxMessageCreatedEvent : AggregateEvent<MessageAggregate, MessageId>, IHasCorrelationId
//{
//    public long ReqMsgId { get; }
//    public MessageItem OutboxMessageItem { get; }
//    public bool ClearDraft { get; }
//    public int GroupItemCount { get; }
//    public Guid CorrelationId { get; }

//    public OutboxMessageCreatedEvent(long reqMsgId, MessageItem outboxMessageItem, bool clearDraft, int groupItemCount, Guid correlationId)
//    {
//        ReqMsgId = reqMsgId;
//        OutboxMessageItem = outboxMessageItem;
//        ClearDraft = clearDraft;
//        GroupItemCount = groupItemCount;
//        CorrelationId = correlationId;
//    }
//}

//public class InboxMessageCreatedEvent : AggregateEvent<MessageAggregate, MessageId>, IHasCorrelationId
//{
//    public MessageItem InboxMessageItem { get; }
//    public int SenderMessageId { get; }

//    public InboxMessageCreatedEvent(MessageItem inboxMessageItem, int senderMessageId,
//        Guid correlationId)
//    {
//        InboxMessageItem = inboxMessageItem;
//        SenderMessageId = senderMessageId;
//        CorrelationId = correlationId;
//    }

//    public Guid CorrelationId { get; }
//}

//public class InboxMessageIdAddedToOutboxMessageEvent : AggregateEvent<MessageAggregate, MessageId>
//{
//    public InboxItem InboxItem { get; }

//    public InboxMessageIdAddedToOutboxMessageEvent(InboxItem inboxItem)
//    {
//        InboxItem = inboxItem;
//    }
//}

//public class InboxMessageEditedEvent : AggregateEvent<MessageAggregate, MessageId>, IHasCorrelationId
//{
//    public InboxMessageEditedEvent(
//        long inboxOwnerPeerId,
//        int messageId,
//        string newMessage,
//        byte[]? entities,
//        int editDate,
//        Peer toPeer,
//        byte[]? media,
//        Guid correlationId)
//    {
//        InboxOwnerPeerId = inboxOwnerPeerId;
//        MessageId = messageId;
//        NewMessage = newMessage;
//        Entities = entities;
//        EditDate = editDate;
//        ToPeer = toPeer;
//        Media = media;
//        CorrelationId = correlationId;
//    }

//    public Peer ToPeer { get; }
//    public byte[]? Entities { get; }
//    public int EditDate { get; }
//    public long InboxOwnerPeerId { get; }
//    public byte[]? Media { get; }

//    public int MessageId { get; }
//    public string NewMessage { get; }
//    public Guid CorrelationId { get; }
//}

//public class OutboxMessageEditedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>, IHasCorrelationId
//{
//    public OutboxMessageEditedEvent(RequestInfo request,
//        IReadOnlyCollection<InboxItem>? inboxItems,
//        MessageItem oldMessageItem,
//        int messageId,
//        string newMessage,
//        int editDate,
//        byte[]? entities,
//        byte[]? media,
//        Guid correlationId) : base(request)
//    {
//        InboxItems = inboxItems;
//        OldMessageItem = oldMessageItem;
//        MessageId = messageId;
//        NewMessage = newMessage;
//        Entities = entities;
//        Media = media;
//        EditDate = editDate;
//        CorrelationId = correlationId;
//    }

//    public Guid CorrelationId { get; }
//    public IReadOnlyCollection<InboxItem>? InboxItems { get; }
//    public MessageItem OldMessageItem { get; }
//    public int MessageId { get; }
//    public string NewMessage { get; }
//    public byte[]? Entities { get; }
//    public byte[]? Media { get; }
//    public int EditDate { get; }
//}

//public class MessageDeletedEvent : AggregateEvent<MessageAggregate, MessageId>, IHasCorrelationId
//{
//    public MessageDeletedEvent(
//        long ownerPeerId,
//        int messageId,
//        bool isOut,
//        long senderPeerId,
//        int senderMessageId,
//        IReadOnlyList<InboxItem>? inboxItems,
//        Guid correlationId)
//    {
//        OwnerPeerId = ownerPeerId;
//        MessageId = messageId;
//        IsOut = isOut;
//        SenderPeerId = senderPeerId;
//        SenderMessageId = senderMessageId;
//        InboxItems = inboxItems;
//        CorrelationId = correlationId;
//    }

//    public IReadOnlyList<InboxItem>? InboxItems { get; }
//    public bool IsOut { get; }
//    public int MessageId { get; }

//    public long OwnerPeerId { get; }
//    public int SenderMessageId { get; }
//    public long SenderPeerId { get; }
//    public Guid CorrelationId { get; }
//}

//public class MessageForwardedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>, IHasCorrelationId
//{
//    public long RandomId { get; }
//    public MessageItem OriginalMessageItem { get; }
//    public Guid CorrelationId { get; }

//    public MessageForwardedEvent(RequestInfo request, long randomId, MessageItem originalMessageItem, Guid correlationId) : base(request)
//    {
//        RandomId = randomId;
//        OriginalMessageItem = originalMessageItem;
//        CorrelationId = correlationId;
//    }
//}

//public class ReplyToMessageEvent : AggregateEvent<MessageAggregate, MessageId>, IHasCorrelationId
//{
//    public ReplyToMessageEvent(
//        int senderMessageId,
//        IReadOnlyList<InboxItem>? inboxItems,
//        Guid correlationId)
//    {
//        SenderMessageId = senderMessageId;
//        InboxItems = inboxItems;
//        CorrelationId = correlationId;
//    }

//    public int SenderMessageId { get; }
//    public IReadOnlyList<InboxItem>? InboxItems { get; }
//    public Guid CorrelationId { get; }
//}

//public class InboxMessageHasReadEvent : AggregateEvent<MessageAggregate, MessageId>, IHasCorrelationId
//{
//    public long ReqMsgId { get; }
//    public long ReaderUid { get; }
//    public int MaxMessageId { get; }
//    public long SenderPeerId { get; }
//    public int SenderMessageId { get; }
//    public Peer ToPeer { get; }
//    public bool IsOut { get; }
//    public Guid CorrelationId { get; }

//    public InboxMessageHasReadEvent(long reqMsgId, long readerUid, int maxMessageId,
//        long senderPeerId,
//        int senderMessageId,
//        Peer toPeer,
//        bool isOut,
//        Guid correlationId
//    )
//    {
//        ReqMsgId = reqMsgId;
//        ReaderUid = readerUid;
//        MaxMessageId = maxMessageId;
//        SenderPeerId = senderPeerId;
//        SenderMessageId = senderMessageId;
//        ToPeer = toPeer;
//        IsOut = isOut;
//        CorrelationId = correlationId;
//    }
//}

//public class MessageSnapshot : ISnapshot
//{
//    public RequestInfo Request { get; }

//    public MessageItem? MessageItem { get; }

//    public int SenderMessageId { get; }

//    public bool ClearDraft { get; }

//    public int GroupItemCount { get; }

//    public Dictionary<long, int> InboxItems { get; }
//    public string? ChatTitle { get; }

//    public IReadOnlyList<long>? ChatMemberUidList { get; }
//    public IReadOnlyList<long>? BotUidList { get; }
//    public long? LinkedChannelId { get; }
//    //public bool Post { get; private set; }
//    //public int? Views { get; private set; }
//    public Guid CorrelationId { get; }
//    public MessageSnapshot(RequestInfo request, MessageItem? messageItem, int senderMessageId, bool clearDraft,
//        int groupItemCount, Dictionary<long, int> inboxItems, string? chatTitle, IReadOnlyList<long>? chatMemberUidList,
//        IReadOnlyList<long>? botUidList, long? linkedChannelId,

//        Guid correlationId/*, int editDate, bool pmOneSide*/)
//    {
//        Request = request;
//        MessageItem = messageItem;
//        SenderMessageId = senderMessageId;
//        ClearDraft = clearDraft;
//        GroupItemCount = groupItemCount;
//        InboxItems = inboxItems;
//        ChatTitle = chatTitle;
//        ChatMemberUidList = chatMemberUidList;
//        BotUidList = botUidList;
//        LinkedChannelId = linkedChannelId;
//        CorrelationId = correlationId;
//    }
//}

////public class MessageSnapshot : ISnapshot
////{
////    public MessageSnapshot(MessageItem messageItem,
////        int senderMessageId,
////        bool pinned,
////        bool pmOneSide,
////        int editDate,
////        int pts)
////    {
////        MessageItem = messageItem;
////        SenderMessageId = senderMessageId;
////        Pinned = pinned;
////        PmOneSide = pmOneSide;
////        EditDate = editDate;
////        Pts = pts;
////    }

////    public MessageItem MessageItem { get; }
////    public List<InboxItem> InboxItems { get; } = new();
////    public int SenderMessageId { get; }

////    public bool Pinned { get; }
////    public bool PmOneSide { get; }
////    public int EditDate { get; }
////    public int Pts { get; }


////}

//public class MessageState : AggregateState<MessageAggregate, MessageId, MessageState>,
//    IApply<SendMessageStartedEvent>,
//    IApply<OutboxMessageCreatedEvent>,
//    IApply<InboxMessageCreatedEvent>,
//    IApply<InboxMessageIdAddedToOutboxMessageEvent>,
//    IApply<MessageDeletedEvent>,
//    IApply<OutboxMessageEditedEvent>,
//    IApply<InboxMessageEditedEvent>,
//    IApply<MessageForwardedEvent>,
//    IApply<InboxMessageHasReadEvent>,
//    IApply<ReplyToMessageEvent>,
//    IApply<ReplyToMessageStartedEvent>,
//    IApply<MessageViewsIncrementedEvent>,
//    IApply<DeleteMessagesStartedEvent>,
//    IApply<UpdatePinnedMessageStartedEvent>,
//    IApply<InboxMessagePinnedUpdatedEvent>,
//    IApply<OutboxMessagePinnedUpdatedEvent>,
//    IApply<OtherPartyMessageDeletedEvent>,
//    IApply<ForwardMessageStartedEvent>

//{
//    public MessageItem MessageItem { get; private set; } = null!;
//    public List<InboxItem> InboxItems { get; private set; } = new();
//    public int SenderMessageId { get; private set; }

//    public bool Pinned { get; private set; }
//    public bool PmOneSide { get; private set; }
//    public int EditDate { get; private set; }
//    public int Pts { get; private set; }

//    //public void LoadSnapshot(MessageSnapshot snapshot)
//    //{
//    //    MessageItem = snapshot.MessageItem;
//    //    InboxItems = snapshot.InboxItems;
//    //    SenderMessageId = snapshot.SenderMessageId;
//    //    Pinned = snapshot.Pinned;
//    //    Pts = snapshot.Pts;
//    //    PmOneSide = snapshot.PmOneSide;
//    //}

//    //public MessageItem InMessageItem { get; private set; }
//    public void Apply(OutboxMessageCreatedEvent aggregateEvent)
//    {
//        MessageItem = aggregateEvent.OutboxMessageItem;
//        SenderMessageId = aggregateEvent.OutboxMessageItem.MessageId;
//    }

//    public void Apply(InboxMessageCreatedEvent aggregateEvent)
//    {
//        MessageItem = aggregateEvent.InboxMessageItem;
//        SenderMessageId = aggregateEvent.SenderMessageId;
//    }

//    public void Apply(InboxMessageIdAddedToOutboxMessageEvent aggregateEvent)
//    {
//        InboxItems.Add(aggregateEvent.InboxItem);
//    }

//    public void Apply(MessageDeletedEvent aggregateEvent)
//    {
//        //throw new NotImplementedException();
//    }

//    public void Apply(OutboxMessageEditedEvent aggregateEvent)
//    {
//        EditDate = aggregateEvent.EditDate;
//    }

//    public void Apply(InboxMessageEditedEvent aggregateEvent)
//    {
//        EditDate = aggregateEvent.EditDate;
//    }

//    public void Apply(MessageForwardedEvent aggregateEvent)
//    {
//        //throw new NotImplementedException();
//    }

//    public void Apply(InboxMessageHasReadEvent aggregateEvent)
//    {
//        //throw new NotImplementedException();
//    }

//    public void Apply(ReplyToMessageEvent aggregateEvent)
//    {
//        //throw new NotImplementedException();
//    }

//    public void Apply(ReplyToMessageStartedEvent aggregateEvent)
//    {
//        //throw new NotImplementedException();
//    }

//    public void Apply(SendMessageStartedEvent aggregateEvent)
//    {
//        MessageItem = aggregateEvent.OutMessageItem;
//        SenderMessageId = aggregateEvent.OutMessageItem.MessageId;
//        //throw new NotImplementedException();
//    }

//    public void Apply(MessageViewsIncrementedEvent aggregateEvent)
//    {
//        MessageItem.Views++;
//    }

//    public void Apply(DeleteMessagesStartedEvent aggregateEvent)
//    {
//        //throw new NotImplementedException();

//    }

//    public void Apply(UpdatePinnedMessageStartedEvent aggregateEvent)
//    {
//        Pinned = aggregateEvent.Pinned;
//        PmOneSide = aggregateEvent.PmOneSide;
//    }

//    public void Apply(InboxMessagePinnedUpdatedEvent aggregateEvent)
//    {
//        Pinned = aggregateEvent.Pinned;
//        //PmOneSide = false;
//    }

//    public void Apply(OutboxMessagePinnedUpdatedEvent aggregateEvent)
//    {
//        Pinned = aggregateEvent.Pinned;
//        //PmOneSide = false;
//    }

//    public void Apply(OtherPartyMessageDeletedEvent aggregateEvent)
//    {
//        //throw new NotImplementedException();
//    }

//    public void Apply(ForwardMessageStartedEvent aggregateEvent)
//    {
//        //throw new NotImplementedException();
//    }
//}

//public class EditOutboxMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>
//{
//    public int MessageId { get; }
//    public string NewMessage { get; }
//    public byte[]? Entities { get; }
//    public int EditDate { get; }
//    public byte[]? Media { get; }
//    public Guid CorrelationId { get; }

//    public EditOutboxMessageCommand(MessageId aggregateId,
//        RequestInfo request,
//        int messageId, string newMessage, byte[]? entities, int editDate, byte[]? media, Guid correlationId) : base(aggregateId, request)
//    {
//        MessageId = messageId;
//        NewMessage = newMessage;
//        Entities = entities;
//        EditDate = editDate;
//        Media = media;
//        CorrelationId = correlationId;
//    }
//}

//public class EditOutboxMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, EditOutboxMessageCommand>
//{
//    public override Task ExecuteAsync(MessageAggregate aggregate,
//        EditOutboxMessageCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.EditOutboxMessage(command.Request,
//            command.MessageId,
//            command.NewMessage,
//            command.EditDate,
//            command.Entities,
//            command.Media,
//            command.CorrelationId);

//        return Task.CompletedTask;
//    }
//}

//public class MessageAggregate : AggregateRoot<MessageAggregate, MessageId>
//{
//    private readonly MessageState _state = new();
//    public MessageAggregate(MessageId id) : base(id)
//    {
//        Register(_state);
//    }

//    public void CreateOutboxMessage(long reqMsgId, MessageItem outboxMessageItem, bool clearDraft, int groupItemCount, Guid correlationId)
//    {
//        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
//        //Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
//        Emit(new OutboxMessageCreatedEvent(reqMsgId, outboxMessageItem, clearDraft, groupItemCount, correlationId));
//    }

//    public void CreateInboxMessage(MessageItem inboxMessageItem, int senderMessageId, Guid correlationId)
//    {
//        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
//        Emit(new InboxMessageCreatedEvent(inboxMessageItem, senderMessageId, correlationId));
//    }

//    /// <summary>
//    /// Sender's message id and receiver's message id are independent,Add receiver's message id to sender,delete messages or pin messages need this
//    /// </summary>
//    /// <param name="inboxOwnerPeerId"></param>
//    /// <param name="inboxMessageId"></param>
//    public void AddInboxMessageIdToOutboxMessage(long inboxOwnerPeerId, int inboxMessageId)
//    {
//        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
//        Emit(new InboxMessageIdAddedToOutboxMessageEvent(new InboxItem(inboxOwnerPeerId, inboxMessageId)));
//    }

//    public void DeleteMessage(int messageId, Guid correlationId)
//    {
//        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
//        Emit(new MessageDeletedEvent(_state.MessageItem.OwnerPeer.PeerId,
//            //_state.MessageItem.MessageId,
//            messageId,
//            _state.MessageItem.IsOut,
//            _state.MessageItem.SenderPeer.PeerId,
//            _state.SenderMessageId,
//            _state.InboxItems,
//            correlationId
//        ));
//    }

//    public void EditOutboxMessage(RequestInfo request,
//        int messageId, string newMessage, int editDate, byte[]? entities, byte[]? media, Guid correlationId)
//    {
//        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
//        if (_state.MessageItem.Date + MyTelegramServerDomainConsts.EditTimeLimit < DateTime.UtcNow.ToTimestamp())
//        {
//            ThrowHelper.ThrowUserFriendlyException("MESSAGE_EDIT_TIME_EXPIRED");
//        }

//        Emit(new OutboxMessageEditedEvent(request,
//            _state.InboxItems,
//            _state.MessageItem,
//            messageId,
//            newMessage,
//            editDate,
//            entities,
//            media,
//            correlationId));
//    }

//    public void EditInboxMessage(
//        //long inboxOwnerPeerId,
//        int messageId,
//        string newMessage,
//        int editDate,
//        byte[]? entities,
//        byte[]? media,
//        Guid correlationId)
//    {
//        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
//        Emit(new InboxMessageEditedEvent(_state.MessageItem.OwnerPeer.PeerId, messageId, newMessage, entities, editDate, _state.MessageItem.ToPeer, media, correlationId));
//    }

//    //public void StartForwardMessage(long )

//    public void ForwardMessage(
//        RequestInfo request,
//        long randomId,
//        Guid correlationId)
//    {
//        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
//        Emit(new MessageForwardedEvent(request, randomId, _state.MessageItem, correlationId));
//    }

//    public void StartForwardMessage(RequestInfo request, Peer fromPeer,
//        Peer toPeer,
//        IReadOnlyList<int> idList,
//        IReadOnlyList<long> randomIdList,
//        Guid correlationId)
//    {
//        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
//        Emit(new ForwardMessageStartedEvent(request, fromPeer, toPeer, idList, randomIdList, correlationId));
//    }


//    public void ReadInboxHistory(long reqMsgId,
//        long readerUid,
//        Guid correlationId)
//    {
//        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
//        Emit(new InboxMessageHasReadEvent(reqMsgId, readerUid,
//            _state.MessageItem.MessageId,
//            _state.MessageItem.SenderPeer.PeerId,
//            _state.SenderMessageId,
//            _state.MessageItem.ToPeer,
//            _state.MessageItem.SenderPeer.PeerId == readerUid,
//            correlationId
//        ));
//    }

//    public void StartReplyToMessage(Guid correlationId)
//    {
//        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
//        Emit(new ReplyToMessageStartedEvent(_state.MessageItem.IsOut, _state.InboxItems, _state.MessageItem.SenderPeer, _state.MessageItem.ToPeer, _state.SenderMessageId, correlationId));
//    }

//    public void ReplyToMessage(/*int messageId*/Guid correlationId)
//    {
//        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
//        Emit(new ReplyToMessageEvent(_state.SenderMessageId, _state.InboxItems, correlationId));
//    }

//    public void StartSendMessage(RequestInfo request, MessageItem outMessageItem, bool clearDraft, int groupItemCount, Guid correlationId)
//    {
//        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
//        Emit(new SendMessageStartedEvent(request, outMessageItem, clearDraft, groupItemCount, correlationId));
//    }

//    public void StartUpdatePinnedMessage(RequestInfo request,
//        bool pinned,
//        bool pmOneSide,
//        bool silent,
//        int date,
//        long randomId,
//        string messageActionData,
//        Guid correlationId)
//    {
//        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
//        var oldPmOneSide = pmOneSide;
//        if (!pinned)
//        {
//            oldPmOneSide = _state.PmOneSide;
//        }

//        var item = _state.MessageItem;
//        Emit(new UpdatePinnedMessageStartedEvent(request, item.OwnerPeer.PeerId,
//            item.MessageId,
//            pinned,
//            oldPmOneSide,
//            silent,
//            date,
//            item.IsOut,
//            _state.InboxItems,
//            item.SenderPeer.PeerId,
//            _state.SenderMessageId,
//            item.ToPeer,
//            randomId,
//            messageActionData,
//            correlationId
//            ));
//    }

//    public void UpdateOutboxMessagePinned(bool pinned,
//        bool pmOneSize,
//        bool silent,
//        int date,
//        Guid correlationId)
//    {
//        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
//        var item = _state.MessageItem;
//        Emit(new OutboxMessagePinnedUpdatedEvent(item.OwnerPeer.PeerId,
//            item.MessageId,
//            pinned,
//            pmOneSize,
//            silent, date, _state.InboxItems,
//            item.SenderPeer.PeerId,
//            _state.SenderMessageId,
//            item.ToPeer, _state.Pts, correlationId
//            ));
//    }

//    public void UpdateInboxMessagePinned(bool pinned,
//        bool pmOneSide,
//        bool silent,
//        int date,
//        Guid correlationId)
//    {
//        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
//        var item = _state.MessageItem;
//        Emit(new InboxMessagePinnedUpdatedEvent(item.OwnerPeer.PeerId,
//            item.MessageId,
//            pinned,
//            pmOneSide,
//            silent,
//            date,
//            item.ToPeer,
//            _state.Pts,
//            correlationId));
//    }

//    public void DeleteMessage(Guid correlationId)
//    {
//        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
//        Emit(new MessageDeletedEvent(_state.MessageItem.OwnerPeer.PeerId,
//            _state.MessageItem.MessageId,
//            _state.MessageItem.IsOut,
//            _state.MessageItem.SenderPeer.PeerId,
//            _state.SenderMessageId,
//            _state.InboxItems,
//            correlationId));
//    }

//    public void DeleteOtherPartyMessage(Guid correlationId)
//    {
//        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
//        Emit(new OtherPartyMessageDeletedEvent(_state.MessageItem.OwnerPeer.PeerId, _state.MessageItem.MessageId, correlationId));
//    }
//    public void StartDeleteMessages(RequestInfo request,
//        bool revoke,
//        IReadOnlyList<int> idList,
//        Guid correlationId)
//    {
//        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
//        Emit(new DeleteMessagesStartedEvent(request,
//            _state.MessageItem.OwnerPeer.PeerId,
//            _state.MessageItem.IsOut,
//            _state.MessageItem.SenderPeer.PeerId,
//            _state.SenderMessageId,
//            _state.MessageItem.ToPeer,
//            idList,
//            revoke,
//            _state.InboxItems,
//            correlationId));
//    }

//    public void IncrementViews()
//    {
//        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
//        Emit(new MessageViewsIncrementedEvent(_state.MessageItem.MessageId, _state.MessageItem.Views ?? 0 + 1));
//    }
//}

//public class MessageViewsIncrementedEvent : AggregateEvent<MessageAggregate, MessageId>
//{
//    public int MessageId { get; }
//    public int Views { get; }

//    public MessageViewsIncrementedEvent(int messageId, int views)
//    {
//        MessageId = messageId;
//        Views = views;
//    }
//}

//public class IncrementViewsCommand : Command<MessageAggregate, MessageId, IExecutionResult>
//{
//    public IncrementViewsCommand(MessageId aggregateId) : base(aggregateId)
//    {
//    }
//}

//public class IncrementViewsCommandHandler : CommandHandler<MessageAggregate, MessageId, IncrementViewsCommand>
//{
//    public override Task ExecuteAsync(MessageAggregate aggregate,
//        IncrementViewsCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.IncrementViews();
//        return Task.CompletedTask;
//    }
//}

//public class
//    StartForwardMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, StartForwardMessageCommand>
//{
//    public override Task ExecuteAsync(MessageAggregate aggregate,
//        StartForwardMessageCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.StartForwardMessage(command.Request,
//            command.FromPeer,
//            command.ToPeer,
//            command.IdList,
//            command.RandomIdList,
//            command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}


//public class StartForwardMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>,
//     IHasCorrelationId
//{
//    public StartForwardMessageCommand(MessageId aggregateId,
//        RequestInfo request,
//        Peer fromPeer,
//        Peer toPeer,
//        IReadOnlyList<int> idList,
//        IReadOnlyList<long> randomIdList,
//        Guid correlationId) : base(aggregateId, request)
//    {
//        FromPeer = fromPeer;
//        ToPeer = toPeer;
//        IdList = idList;
//        RandomIdList = randomIdList;
//        CorrelationId = correlationId;
//    }

//    public Peer FromPeer { get; }
//    public IReadOnlyList<int> IdList { get; }
//    public IReadOnlyList<long> RandomIdList { get; }

//    public Peer ToPeer { get; }
//    public Guid CorrelationId { get; }

//    protected override IEnumerable<byte[]> GetSourceIdComponents()
//    {
//        yield return CorrelationId.ToByteArray();
//    }
//}

//public class ForwardMessageStartedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>, IHasCorrelationId
//{
//    public ForwardMessageStartedEvent(
//        RequestInfo request,
//        Peer fromPeer,
//        Peer toPeer,
//        IReadOnlyList<int> idList,
//        IReadOnlyList<long> randomIdList,
//        Guid correlationId) : base(request)
//    {
//        FromPeer = fromPeer;
//        ToPeer = toPeer;
//        IdList = idList;
//        RandomIdList = randomIdList;
//        CorrelationId = correlationId;
//    }

//    public Peer FromPeer { get; }
//    public IReadOnlyList<int> IdList { get; }
//    public IReadOnlyList<long> RandomIdList { get; }

//    public Peer ToPeer { get; }
//    public Guid CorrelationId { get; }
//}


//public class DeleteMessageCommand : Command<MessageAggregate, MessageId, IExecutionResult>, IHasCorrelationId
//{
//    public DeleteMessageCommand(MessageId aggregateId,
//        //long reqMsgId,

//        //bool revoke,
//        Guid correlationId) : base(aggregateId)
//    {
//        //Revoke = revoke;
//        CorrelationId = correlationId;
//    }

//    //public bool Revoke { get; }
//    public Guid CorrelationId { get; }
//}

//public class DeleteOtherPartyMessageCommand : Command<MessageAggregate, MessageId, IExecutionResult>,
//    IHasCorrelationId
//{
//    public DeleteOtherPartyMessageCommand(MessageId aggregateId,
//        //long reqMsgId,

//        //bool revoke,
//        Guid correlationId) : base(aggregateId)
//    {
//        //Revoke = revoke;
//        CorrelationId = correlationId;
//    }

//    //public bool Revoke { get; }
//    public Guid CorrelationId { get; }
//}
//public class DeleteMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, DeleteMessageCommand>
//{
//    public override Task ExecuteAsync(MessageAggregate aggregate,
//        DeleteMessageCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.DeleteMessage(command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}
//public class DeleteOtherPartyMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, DeleteOtherPartyMessageCommand>
//{
//    public override Task ExecuteAsync(MessageAggregate aggregate,
//        DeleteOtherPartyMessageCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.DeleteOtherPartyMessage(command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}

//public class StartDeleteMessagesCommandHandler : CommandHandler<MessageAggregate, MessageId, StartDeleteMessagesCommand>
//{
//    public override Task ExecuteAsync(MessageAggregate aggregate,
//        StartDeleteMessagesCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.StartDeleteMessages(command.Request, command.Revoke, command.IdList, command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}

//public class StartDeleteMessagesCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>
//{
//    public bool Revoke { get; }
//    public IReadOnlyList<int> IdList { get; }
//    public Guid CorrelationId { get; }

//    public StartDeleteMessagesCommand(MessageId aggregateId,
//        RequestInfo request, bool revoke,
//        IReadOnlyList<int> idList,
//        Guid correlationId) : base(aggregateId, request)
//    {
//        Revoke = revoke;
//        IdList = idList;
//        CorrelationId = correlationId;
//    }
//}

//public class OtherPartyMessageDeletedEvent : AggregateEvent<MessageAggregate, MessageId>, IHasCorrelationId
//{
//    public OtherPartyMessageDeletedEvent(long ownerPeerId,
//        int messageId,
//        Guid correlationId)
//    {
//        OwnerPeerId = ownerPeerId;
//        MessageId = messageId;
//        CorrelationId = correlationId;
//    }

//    public int MessageId { get; }

//    public long OwnerPeerId { get; }
//    public Guid CorrelationId { get; }
//}

//public class DeleteMessagesStartedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>, IHasCorrelationId
//{
//    public DeleteMessagesStartedEvent(
//        RequestInfo request,
//        long ownerPeerId,
//        bool isOut,
//        long senderPeerId,
//        int senderMessageId,
//        Peer toPeer,
//        IReadOnlyList<int> idList,
//        bool revoke,
//        IReadOnlyList<InboxItem> inboxItems,
//        Guid correlationId) : base(request)
//    {
//        OwnerPeerId = ownerPeerId;
//        IsOut = isOut;
//        SenderPeerId = senderPeerId;
//        SenderMessageId = senderMessageId;
//        ToPeer = toPeer;
//        IdList = idList;
//        Revoke = revoke;
//        InboxItems = inboxItems;
//        CorrelationId = correlationId;
//    }

//    public IReadOnlyList<int> IdList { get; }
//    public IReadOnlyList<InboxItem> InboxItems { get; }
//    public bool IsOut { get; }
//    public long OwnerPeerId { get; }
//    public bool Revoke { get; }
//    public int SenderMessageId { get; }
//    public Peer ToPeer { get; }
//    public long SenderPeerId { get; }
//    public Guid CorrelationId { get; }
//}

//public class UpdateOutboxMessagePinnedCommand : Command<MessageAggregate, MessageId, IExecutionResult>
//{
//    public bool Pinned { get; }
//    public bool PmOneSize { get; }
//    public bool Silent { get; }
//    public int Date { get; }
//    public Guid CorrelationId { get; }

//    public UpdateOutboxMessagePinnedCommand(MessageId aggregateId, bool pinned,
//        bool pmOneSize,
//        bool silent,
//        int date,
//        Guid correlationId) : base(aggregateId)
//    {
//        Pinned = pinned;
//        PmOneSize = pmOneSize;
//        Silent = silent;
//        Date = date;
//        CorrelationId = correlationId;
//    }
//}

//public class
//    UpdateOutboxMessagePinnedCommandHandler : CommandHandler<MessageAggregate, MessageId,
//        UpdateOutboxMessagePinnedCommand>
//{
//    public override Task ExecuteAsync(MessageAggregate aggregate,
//        UpdateOutboxMessagePinnedCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.UpdateOutboxMessagePinned(command.Pinned,
//            command.PmOneSize,
//            command.Silent,
//            command.Date,
//            command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}
//public class
//    UpdateInboxMessagePinnedCommandHandler : CommandHandler<MessageAggregate, MessageId,
//        UpdateInboxMessagePinnedCommand>
//{
//    public override Task ExecuteAsync(MessageAggregate aggregate,
//        UpdateInboxMessagePinnedCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.UpdateInboxMessagePinned(command.Pinned,
//            command.PmOneSize,
//            command.Silent,
//            command.Date,
//            command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}

//public class UpdateInboxMessagePinnedCommand : Command<MessageAggregate, MessageId, IExecutionResult>
//{
//    public bool Pinned { get; }
//    public bool PmOneSize { get; }
//    public bool Silent { get; }
//    public int Date { get; }
//    public Guid CorrelationId { get; }

//    public UpdateInboxMessagePinnedCommand(MessageId aggregateId, bool pinned,
//        bool pmOneSize,
//        bool silent,
//        int date,
//        Guid correlationId) : base(aggregateId)
//    {
//        Pinned = pinned;
//        PmOneSize = pmOneSize;
//        Silent = silent;
//        Date = date;
//        CorrelationId = correlationId;
//    }
//}

//public class
//    StartUpdatePinnedMessageCommandHandler : CommandHandler<MessageAggregate, MessageId,
//        StartUpdatePinnedMessageCommand>
//{
//    public override Task ExecuteAsync(MessageAggregate aggregate,
//        StartUpdatePinnedMessageCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.StartUpdatePinnedMessage(command.Request,
//            command.Pinned,
//            command.PmOneSide,
//            command.Silent,
//            command.Date,
//            command.RandomId,
//            command.MessageActionData,
//            command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}

//public class StartUpdatePinnedMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>
//{
//    public bool Pinned { get; }
//    public bool PmOneSide { get; }
//    public bool Silent { get; }
//    public int Date { get; }
//    public long RandomId { get; }
//    public string MessageActionData { get; }
//    public Guid CorrelationId { get; }

//    public StartUpdatePinnedMessageCommand(MessageId aggregateId,
//        RequestInfo request, bool pinned,
//        bool pmOneSide,
//        bool silent,
//        int date,
//        long randomId,
//        string messageActionData,
//        Guid correlationId) : base(aggregateId, request)
//    {
//        Pinned = pinned;
//        PmOneSide = pmOneSide;
//        Silent = silent;
//        Date = date;
//        RandomId = randomId;
//        MessageActionData = messageActionData;
//        CorrelationId = correlationId;
//    }
//}

//public class OutboxMessagePinnedUpdatedEvent : AggregateEvent<MessageAggregate, MessageId>, IHasCorrelationId
//{
//    public OutboxMessagePinnedUpdatedEvent(
//        long ownerPeerId,
//        int messageId,
//        //long channelId,
//        bool pinned,
//        bool pmOneSide,
//        bool silent,
//        int date,
//        IReadOnlyList<InboxItem> inboxItems,
//        long senderPeerId,
//        int senderMessageId,
//        Peer toPeer,
//        int pts,
//        Guid correlationId)
//    {
//        OwnerPeerId = ownerPeerId;
//        MessageId = messageId;
//        //ChannelId = channelId;
//        Pinned = pinned;
//        PmOneSide = pmOneSide;
//        Silent = silent;
//        Date = date;
//        InboxItems = inboxItems;
//        SenderPeerId = senderPeerId;
//        SenderMessageId = senderMessageId;
//        ToPeer = toPeer;
//        Pts = pts;
//        CorrelationId = correlationId;
//    }

//    public int Date { get; }
//    public IReadOnlyList<InboxItem> InboxItems { get; }

//    public int MessageId { get; }

//    public long OwnerPeerId { get; }

//    //public long ChannelId { get; }
//    public bool Pinned { get; }
//    public bool PmOneSide { get; }
//    public int Pts { get; }
//    public int SenderMessageId { get; }
//    public Peer ToPeer { get; }
//    public long SenderPeerId { get; }
//    public bool Silent { get; }
//    public Guid CorrelationId { get; }
//}

//public class InboxMessagePinnedUpdatedEvent : AggregateEvent<MessageAggregate, MessageId>, IHasCorrelationId
//{
//    public InboxMessagePinnedUpdatedEvent(
//        long ownerPeerId,
//        int messageId,
//        //long channelId,
//        bool pinned,
//        bool pmOneSide,
//        bool silent,
//        int date,
//       Peer toPeer,
//        int pts,
//        Guid correlationId)
//    {
//        OwnerPeerId = ownerPeerId;
//        MessageId = messageId;
//        Pinned = pinned;
//        PmOneSide = pmOneSide;
//        Silent = silent;
//        Date = date;
//        ToPeer = toPeer;
//        Pts = pts;
//        CorrelationId = correlationId;
//    }

//    public int Date { get; }
//    public Peer ToPeer { get; }

//    public int MessageId { get; }

//    public long OwnerPeerId { get; }
//    public bool Pinned { get; }
//    public bool PmOneSide { get; }
//    public int Pts { get; }
//    public bool Silent { get; }
//    public Guid CorrelationId { get; }
//}


//public class UpdatePinnedMessageStartedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>, IHasCorrelationId
//{
//    public long OwnerPeerId { get; }
//    public int MessageId { get; }
//    public bool Pinned { get; }
//    public bool PmOneSide { get; }
//    public bool Silent { get; }
//    public int Date { get; }
//    public bool IsOut { get; }
//    public IReadOnlyList<InboxItem> InboxItems { get; }
//    public long SenderPeerId { get; }
//    public int SenderMessageId { get; }
//    public Peer ToPeer { get; }
//    public long RandomId { get; }
//    public string MessageActionData { get; }
//    public Guid CorrelationId { get; }

//    public UpdatePinnedMessageStartedEvent(RequestInfo request,
//        long ownerPeerId,
//        int messageId,
//        bool pinned,
//        bool pmOneSide,
//        bool silent,
//        int date,
//        bool isOut,
//        IReadOnlyList<InboxItem> inboxItems,
//        long senderPeerId,
//        int senderMessageId,
//        Peer toPeer,
//        long randomId,
//        string messageActionData,
//        Guid correlationId
//        ) : base(request)
//    {
//        OwnerPeerId = ownerPeerId;
//        MessageId = messageId;
//        Pinned = pinned;
//        PmOneSide = pmOneSide;
//        Silent = silent;
//        Date = date;
//        IsOut = isOut;
//        InboxItems = inboxItems;
//        SenderPeerId = senderPeerId;
//        SenderMessageId = senderMessageId;
//        ToPeer = toPeer;
//        RandomId = randomId;
//        MessageActionData = messageActionData;
//        CorrelationId = correlationId;
//    }
//}

//public class ForwardMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>
//{
//    public long RandomId { get; }
//    public Guid CorrelationId { get; }

//    public ForwardMessageCommand(MessageId aggregateId,
//        RequestInfo request,
//        long randomId,
//        Guid correlationId) : base(aggregateId, request)
//    {
//        RandomId = randomId;
//        CorrelationId = correlationId;
//    }

//    protected override IEnumerable<byte[]> GetSourceIdComponents()
//    {
//        yield return BitConverter.GetBytes(RandomId);
//        yield return CorrelationId.ToByteArray();
//    }
//}

//public class ForwardMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, ForwardMessageCommand>
//{
//    public override Task ExecuteAsync(MessageAggregate aggregate,
//        ForwardMessageCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.ForwardMessage(command.Request, command.RandomId, command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}

//public class ReadInboxHistoryCommand : RequestCommand<MessageAggregate, MessageId, IExecutionResult>
//{
//    public long ReaderUid { get; }
//    public Guid CorrelationId { get; }

//    public ReadInboxHistoryCommand(MessageId aggregateId,
//        long reqMsgId, long readerUid,
//        Guid correlationId) : base(aggregateId, reqMsgId)
//    {
//        ReaderUid = readerUid;
//        CorrelationId = correlationId;
//    }
//}

//public class ReadInboxHistoryCommandHandler : CommandHandler<MessageAggregate, MessageId, ReadInboxHistoryCommand>
//{
//    public override Task ExecuteAsync(MessageAggregate aggregate,
//        ReadInboxHistoryCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.ReadInboxHistory(command.ReqMsgId, command.ReaderUid, command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}

//public class SendMessageStartedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>, IHasCorrelationId
//{
//    public MessageItem OutMessageItem { get; }
//    public bool ClearDraft { get; }
//    public int GroupItemCount { get; }
//    public Guid CorrelationId { get; }

//    public SendMessageStartedEvent(RequestInfo request, MessageItem outMessageItem, bool clearDraft, int groupItemCount, Guid correlationId) : base(request)
//    {
//        OutMessageItem = outMessageItem;
//        ClearDraft = clearDraft;
//        GroupItemCount = groupItemCount;
//        CorrelationId = correlationId;
//    }
//}

//public class MessageItem : Entity<MessageId>
//{
//    public MessageItem(MessageId id,
//        Peer ownerPeer,
//        Peer toPeer,
//        Peer senderPeer,
//        int messageId,
//        string message,
//        int date,
//        long randomId,
//        bool isOut,
//        SendMessageType sendMessageType = SendMessageType.Text,
//        MessageType messageType = MessageType.Text,
//        MessageSubType messageSubType = MessageSubType.Normal,
//        int? replyToMsgId = null,
//        string? messageActionData = null,
//        MessageActionType messageActionType = MessageActionType.None,
//        byte[]? entities = null,
//        byte[]? media = null,
//        long? groupId = null,
//        //int? groupItemCount = null,
//        bool post = false,
//        MessageFwdHeader? fwdHeader = null,
//        int? views = null
//    ) : base(id)
//    {
//        OwnerPeer = ownerPeer;
//        ToPeer = toPeer;
//        SenderPeer = senderPeer;
//        MessageId = messageId;
//        Message = message ?? throw new ArgumentNullException(nameof(message));
//        Date = date;
//        MessageType = messageType;
//        MessageSubType = messageSubType;
//        RandomId = randomId;
//        IsOut = isOut;
//        SendMessageType = sendMessageType;
//        ReplyToMsgId = replyToMsgId;
//        MessageActionData = messageActionData;
//        MessageActionType = messageActionType;
//        FwdHeader = fwdHeader;
//        Entities = entities;
//        Media = media;
//        GroupId = groupId;
//        Post = post;
//        //GroupItemCount = groupItemCount;
//        Views = views;
//    }

//    public int Date { get; }
//    public byte[]? Entities { get; }
//    public MessageFwdHeader? FwdHeader { get; }
//    public long? GroupId { get; }


//    //public int? GroupItemCount { get; }
//    public byte[]? Media { get; }
//    public string Message { get; }
//    public string? MessageActionData { get; }
//    public MessageActionType MessageActionType { get; }
//    public MessageType MessageType { get; }
//    public MessageSubType MessageSubType { get; }
//    public int MessageId { get; internal set; }
//    public Peer OwnerPeer { get; }
//    public Peer ToPeer { get; }
//    public Peer SenderPeer { get; }
//    public long RandomId { get; }
//    public bool IsOut { get; }
//    public SendMessageType SendMessageType { get; }
//    public int? ReplyToMsgId { get; }

//    public bool Post { get; internal set; }
//    public int? Views { get; internal set; }
//}

//public class MessageSagaId : MyIdentity<MessageSagaId>, ISagaId
//{
//    public MessageSagaId(string value) : base(value)
//    {
//    }
//}

//public class MessageSagaLocator : ISagaLocator
//{
//    public Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent,
//        CancellationToken cancellationToken)
//    {
//        if (domainEvent.GetAggregateEvent() is not IHasCorrelationId id)
//        {
//            throw new NotSupportedException(
//                $"Domain event:{domainEvent.GetAggregateEvent().GetType().FullName} should impl IHasCorrelationId ");
//        }

//        var messageSagaId = new MessageSagaId($"MessageSagaId-{id.CorrelationId}");

//        return Task.FromResult<ISagaId>(messageSagaId);
//    }
//}

//public class CreateOutboxMessageCommand : RequestCommand<MessageAggregate, MessageId, IExecutionResult>
//{
//    //public long ReqMsgId { get; }
//    public MessageItem OutboxMessageItem { get; }
//    public bool ClearDraft { get; }
//    public int GroupItemCount { get; }
//    public Guid CorrelationId { get; }

//    public CreateOutboxMessageCommand(MessageId aggregateId,
//        long reqMsgId, MessageItem outboxMessageItem, bool clearDraft, int groupItemCount, Guid correlationId
//    ) : base(aggregateId, reqMsgId)
//    {
//        OutboxMessageItem = outboxMessageItem;
//        ClearDraft = clearDraft;
//        GroupItemCount = groupItemCount;
//        CorrelationId = correlationId;
//    }

//    protected override IEnumerable<byte[]> GetSourceIdComponents()
//    {
//        yield return BitConverter.GetBytes(ReqMsgId);
//        yield return BitConverter.GetBytes(OutboxMessageItem.RandomId);
//    }
//}

//public class CreateOutboxMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, CreateOutboxMessageCommand>
//{
//    public override Task ExecuteAsync(MessageAggregate aggregate,
//        CreateOutboxMessageCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.CreateOutboxMessage(command.ReqMsgId,
//            command.OutboxMessageItem,
//            command.ClearDraft,
//            command.GroupItemCount,
//            command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}

//public class CreateInboxMessageCommand : Command<MessageAggregate, MessageId, IExecutionResult>, IHasCorrelationId
//{
//    public MessageItem InboxMessageItem { get; }
//    public int SenderMessageId { get; }

//    public CreateInboxMessageCommand(MessageId aggregateId, MessageItem inboxMessageItem, int senderMessageId,
//        Guid correlationId) : base(aggregateId)
//    {
//        InboxMessageItem = inboxMessageItem;
//        SenderMessageId = senderMessageId;
//        CorrelationId = correlationId;
//    }

//    public Guid CorrelationId { get; }

//}

//public class CreateInboxMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, CreateInboxMessageCommand>
//{
//    public override Task ExecuteAsync(MessageAggregate aggregate,
//        CreateInboxMessageCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.CreateInboxMessage(command.InboxMessageItem, command.SenderMessageId, command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}

//public class ReplyToMessageCommand : RequestCommand<MessageAggregate, MessageId, IExecutionResult>
//{
//    public int MessageId { get; }
//    public Guid CorrelationId { get; }

//    public ReplyToMessageCommand(MessageId aggregateId,
//        long reqMsgId,
//        int messageId,
//        Guid correlationId
//        ) : base(aggregateId, reqMsgId)
//    {
//        MessageId = messageId;
//        CorrelationId = correlationId;
//    }
//}

//public class ReplyToMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, ReplyToMessageCommand>
//{
//    public override Task ExecuteAsync(MessageAggregate aggregate,
//        ReplyToMessageCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.ReplyToMessage(command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}

//public class AddInboxMessageIdToOutboxMessageCommand : Command<MessageAggregate, MessageId,
//IExecutionResult>, IHasCorrelationId
//{
//    public AddInboxMessageIdToOutboxMessageCommand(MessageId aggregateId,
//        long inboxOwnerPeerId,
//        int inboxMessageId,
//        Guid correlationId) : base(aggregateId)
//    {
//        InboxOwnerPeerId = inboxOwnerPeerId;
//        InboxMessageId = inboxMessageId;
//        CorrelationId = correlationId;
//    }

//    public int InboxMessageId { get; }

//    public long InboxOwnerPeerId { get; }
//    public Guid CorrelationId { get; }
//}

//public class
//    AddInboxMessageIdToOutboxMessageCommandHandler : CommandHandler<MessageAggregate, MessageId,
//        AddInboxMessageIdToOutboxMessageCommand>
//{
//    public override Task ExecuteAsync(MessageAggregate aggregate,
//        AddInboxMessageIdToOutboxMessageCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.AddInboxMessageIdToOutboxMessage(command.InboxOwnerPeerId, command.InboxMessageId);
//        return Task.CompletedTask;
//    }
//}






//public class StartSendMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>
//{
//    public MessageItem OutMessageItem { get; }
//    public bool ClearDraft { get; }
//    public int GroupItemCount { get; }
//    public Guid CorrelationId { get; }

//    public StartSendMessageCommand(MessageId aggregateId,
//        RequestInfo request, MessageItem outMessageItem, bool clearDraft = false, int groupItemCount = 1, Guid correlationId = default) : base(aggregateId, request)
//    {
//        OutMessageItem = outMessageItem;
//        ClearDraft = clearDraft;
//        GroupItemCount = groupItemCount;
//        CorrelationId = correlationId;
//    }

//    protected override IEnumerable<byte[]> GetSourceIdComponents()
//    {
//        yield return BitConverter.GetBytes(OutMessageItem.RandomId);
//    }
//}

//public class StartSendMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, StartSendMessageCommand>
//{
//    public override Task ExecuteAsync(MessageAggregate aggregate,
//        StartSendMessageCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.StartSendMessage(command.Request, command.OutMessageItem, command.ClearDraft, command.GroupItemCount, command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}




//public class StartReplyToMessageCommand : Command<MessageAggregate, MessageId, IExecutionResult>, IHasCorrelationId
//{
//    public int ReplyToMsgId { get; }

//    public StartReplyToMessageCommand(MessageId aggregateId, int replyToMsgId, Guid correlationId) : base(aggregateId)
//    {
//        ReplyToMsgId = replyToMsgId;
//        CorrelationId = correlationId;
//    }

//    public Guid CorrelationId { get; }
//}

//public class
//    StartReplyToMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, StartReplyToMessageCommand>
//{
//    public override Task ExecuteAsync(MessageAggregate aggregate,
//        StartReplyToMessageCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.StartReplyToMessage(command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}


//public class ReplyToMessageStartedEvent : AggregateEvent<MessageAggregate, MessageId>, IHasCorrelationId
//{
//    public bool IsOut { get; }
//    public IReadOnlyList<InboxItem> InboxItems { get; }
//    public Peer SenderPeer { get; }
//    public Peer ToPeer { get; }
//    public int SenderMessageId { get; }
//    public Guid CorrelationId { get; }

//    public ReplyToMessageStartedEvent(bool isOut, IReadOnlyList<InboxItem> inboxItems,
//        Peer senderPeer,
//        Peer toPeer,
//        int senderMessageId,
//        Guid correlationId
//        )
//    {
//        IsOut = isOut;
//        InboxItems = inboxItems;
//        SenderPeer = senderPeer;
//        ToPeer = toPeer;
//        SenderMessageId = senderMessageId;
//        CorrelationId = correlationId;
//    }
//}

////public interface IInMemorySaga
////{
////}

//public class MessageSagaState : AggregateState<MessageSaga, MessageSagaId, MessageSagaState>,
//    IApply<MessageSagaStartedEvent>,
//    IApply<SendChatMessageStartedEvent>,
//    IApply<ReplyToMessageCompletedEvent>,
//    IApply<SendChannelMessageStartedEvent>,
//    IApply<SendOutboxMessageCompletedEvent>,
//    IApply<ReceiveInboxMessageCompletedEvent>,
//    IApply<OutboxMessageIdGeneratedEvent>
//{
//    public RequestInfo Request { get; private set; } = null!;
//    public bool ClearDraft { get; private set; }
//    public int GroupItemCount { get; private set; }
//    public Guid CorrelationId { get; private set; }
//    public MessageItem? MessageItem { get; private set; }
//    public string? ChatTitle { get; private set; }
//    public IReadOnlyList<long>? ChatMemberUidList { get; private set; }
//    public Dictionary<long, int> InboxItems { get; private set; } = new();
//    public int SenderMessageId { get; private set; }
//    public IReadOnlyList<long>? BotUidList { get; private set; }
//    public long? LinkedChannelId { get; private set; }
//    //public bool Post { get; private set; }
//    //public int? Views { get; private set; }

//    public int InboxCount { get; private set; }

//    public bool IsSendMessageCompleted()
//    {
//        if (MessageItem != null)
//        {
//            switch (MessageItem.ToPeer.PeerType)
//            {
//                case PeerType.User:
//                    return InboxCount == 1;
//                case PeerType.Chat:
//                    return InboxCount == ChatMemberUidList!.Count;
//            }
//        }

//        return false;
//    }

//    public void Apply(MessageSagaStartedEvent aggregateEvent)
//    {
//        Request = aggregateEvent.Request;
//        MessageItem = aggregateEvent.MessageItem;
//        ClearDraft = aggregateEvent.ClearDraft;
//        GroupItemCount = aggregateEvent.GroupItemCount;
//        CorrelationId = aggregateEvent.CorrelationId;
//        SenderMessageId = aggregateEvent.MessageItem.MessageId;
//    }

//    public void Apply(SendChatMessageStartedEvent aggregateEvent)
//    {
//        ChatTitle = aggregateEvent.Title;
//        ChatMemberUidList = aggregateEvent.ChatMemberUidList;
//    }

//    public void Apply(ReplyToMessageCompletedEvent aggregateEvent)
//    {
//        foreach (var inboxItem in aggregateEvent.InboxItems)
//        {
//            InboxItems.TryAdd(inboxItem.InboxOwnerPeerId, inboxItem.InboxMessageId);
//        }
//    }

//    public int? GetReplyToMsgId(long inboxOwnerPeerId)
//    {
//        if (InboxItems.TryGetValue(inboxOwnerPeerId, out var messageId))
//        {
//            return messageId;
//        }

//        return null;
//    }

//    public void LoadSnapshot(MessageSnapshot snapshot)
//    {
//        Request = snapshot.Request;
//        MessageItem = snapshot.MessageItem;
//        ClearDraft = snapshot.ClearDraft;
//        GroupItemCount = snapshot.GroupItemCount;
//        SenderMessageId = snapshot.SenderMessageId;
//        CorrelationId = snapshot.CorrelationId;
//        ChatTitle = snapshot.ChatTitle;
//        ChatMemberUidList = snapshot.ChatMemberUidList;
//        InboxItems = snapshot.InboxItems;

//        BotUidList = snapshot.BotUidList;
//        LinkedChannelId = snapshot.LinkedChannelId;
//    }

//    public void Apply(SendChannelMessageStartedEvent aggregateEvent)
//    {
//        BotUidList = aggregateEvent.BotUidList;
//        LinkedChannelId = aggregateEvent.LinkedChannelId;
//        MessageItem!.Post = aggregateEvent.Post;
//        MessageItem.Views = aggregateEvent.Views;
//    }

//    public void Apply(SendOutboxMessageCompletedEvent aggregateEvent)
//    {
//        //throw new NotImplementedException();
//    }

//    public void Apply(ReceiveInboxMessageCompletedEvent aggregateEvent)
//    {
//        InboxCount++;
//    }

//    public void Apply(OutboxMessageIdGeneratedEvent aggregateEvent)
//    {
//        MessageItem!.MessageId = aggregateEvent.OutboxMessageId;
//    }
//}

//public class MessageSagaStartedEvent : RequestAggregateEvent2<MessageSaga, MessageSagaId>
//{
//    public MessageItem MessageItem { get; }
//    public bool ClearDraft { get; }
//    public int GroupItemCount { get; }
//    public Guid CorrelationId { get; }

//    public MessageSagaStartedEvent(RequestInfo request, MessageItem messageItem, bool clearDraft, int groupItemCount, Guid correlationId) : base(request)
//    {
//        MessageItem = messageItem;
//        ClearDraft = clearDraft;
//        GroupItemCount = groupItemCount;
//        CorrelationId = correlationId;
//    }
//}

//public class SendChatMessageStartedEvent : AggregateEvent<MessageSaga, MessageSagaId>
//{
//    public string Title { get; }
//    public IReadOnlyList<long> ChatMemberUidList { get; }

//    public SendChatMessageStartedEvent(string title, IReadOnlyList<long> chatMemberUidList)
//    {
//        Title = title;
//        ChatMemberUidList = chatMemberUidList;
//    }
//}

//public class SendChannelMessageStartedEvent : AggregateEvent<MessageSaga, MessageSagaId>
//{
//    public bool Post { get; }
//    public int? Views { get; }
//    public IReadOnlyList<long> BotUidList { get; }
//    public long? LinkedChannelId { get; }
//    public Guid CorrelationId { get; }

//    public SendChannelMessageStartedEvent(bool post,
//        int? views,
//        IReadOnlyList<long> botUidList,
//        long? linkedChannelId,
//        Guid correlationId)
//    {
//        Post = post;
//        Views = views;
//        BotUidList = botUidList;
//        LinkedChannelId = linkedChannelId;
//        CorrelationId = correlationId;
//    }
//}

//public class ReplyToMessageCompletedEvent : AggregateEvent<MessageSaga, MessageSagaId>
//{
//    public ReplyToMessageCompletedEvent(IReadOnlyList<InboxItem> inboxItems)
//    {
//        InboxItems = inboxItems;
//    }

//    public IReadOnlyList<InboxItem> InboxItems { get; }
//}

//public class SendOutboxMessageCompletedEvent : RequestAggregateEvent2<MessageSaga, MessageSagaId>
//{
//    public MessageItem MessageItem { get; }
//    public int Pts { get; }
//    public int GroupItemCount { get; }

//    public SendOutboxMessageCompletedEvent(RequestInfo request, MessageItem messageItem, int pts, int groupItemCount) : base(request)
//    {
//        MessageItem = messageItem;
//        Pts = pts;
//        GroupItemCount = groupItemCount;
//    }
//}

//public class ReceiveInboxMessageCompletedEvent : AggregateEvent<MessageSaga, MessageSagaId>
//{
//    public MessageItem MessageItem { get; }
//    public int Pts { get; }
//    public string? ChatTitle { get; }

//    public ReceiveInboxMessageCompletedEvent(MessageItem messageItem, int pts, string? chatTitle)
//    {
//        MessageItem = messageItem;
//        Pts = pts;
//        ChatTitle = chatTitle;
//    }
//}

//public class OutboxMessageIdGeneratedEvent : AggregateEvent<MessageSaga, MessageSagaId>
//{
//    public int OutboxMessageId { get; }

//    public OutboxMessageIdGeneratedEvent(int outboxMessageId)
//    {
//        OutboxMessageId = outboxMessageId;
//    }
//}

//public class MessageSaga :
//    MyInMemoryAggregateSaga<MessageSaga, MessageSagaId, MessageSagaLocator>,
//    ISagaIsStartedBy<MessageAggregate, MessageId, SendMessageStartedEvent>,
//    ISagaHandles<UserAggregate, UserId, CheckUserStateCompletedEvent>,
//    ISagaHandles<ChatAggregate, ChatId, CheckChatStateCompletedEvent>,
//    ISagaHandles<ChannelAggregate, ChannelId, CheckChannelStateCompletedEvent>,

//    ISagaHandles<MessageAggregate, MessageId, OutboxMessageCreatedEvent>,
//    ISagaHandles<MessageAggregate, MessageId, InboxMessageCreatedEvent>,
//    ISagaHandles<MessageAggregate, MessageId, ReplyToMessageStartedEvent>,
//    ISagaHandles<MessageAggregate, MessageId, ReplyToMessageEvent>,
//    IInMemoryAggregate
//{
//    private readonly MessageSagaState _state = new();
//    public MessageSaga(MessageSagaId id, IEventStore eventStore) : base(id, eventStore)
//    {
//        Register(_state);
//    }

//    private void SetTopMessageId(OutboxMessageCreatedEvent aggregateEvent)
//    {
//        var item = aggregateEvent.OutboxMessageItem;
//        var command = new SetOutboxTopMessageCommand(DialogId.Create(item.SenderPeer.PeerId,
//            item.ToPeer),
//            item.MessageId,
//            item.SenderPeer.PeerId,
//            item.ToPeer,
//            aggregateEvent.ClearDraft,
//            aggregateEvent.CorrelationId
//            );
//        Console.WriteLine($"top msgId:{item.MessageId}");
//        Publish(command);
//    }

//    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, OutboxMessageCreatedEvent> domainEvent,
//        ISagaContext sagaContext,
//        CancellationToken cancellationToken)
//    {
//        SetTopMessageId(domainEvent.AggregateEvent);
//        // create reply message if ReplyToMsgId has value,then create inbox message 
//        if (domainEvent.AggregateEvent.OutboxMessageItem.ReplyToMsgId.HasValue)
//        {
//            StartReplyToMessage(domainEvent.AggregateEvent.OutboxMessageItem.OwnerPeer.PeerId,
//                domainEvent.AggregateEvent.OutboxMessageItem.ReplyToMsgId.Value,
//                domainEvent.AggregateEvent.CorrelationId);
//        }
//        else
//        {
//            await HandleSendOutboxMessageCompletedAsync().ConfigureAwait(false);
//            await CreateInboxMessageAsync().ConfigureAwait(false);
//        }
//    }

//    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, InboxMessageCreatedEvent> domainEvent,
//        ISagaContext sagaContext,
//        CancellationToken cancellationToken)
//    {
//        var item = domainEvent.AggregateEvent.InboxMessageItem;
//        var receiveInboxMessageCommand = new ReceiveInboxMessageCommand(
//            DialogId.Create(item.OwnerPeer.PeerId,
//                item.ToPeer),
//            item.MessageId,
//            item.OwnerPeer.PeerId,
//            item.ToPeer,
//            domainEvent.AggregateEvent.CorrelationId
//        );
//        Publish(receiveInboxMessageCommand);

//        var command = new AddInboxMessageIdToOutboxMessageCommand(
//            MessageId.Create(item.SenderPeer.PeerId,
//                domainEvent.AggregateEvent.SenderMessageId),
//            item.OwnerPeer.PeerId,
//            item.MessageId,
//            domainEvent.AggregateEvent.CorrelationId);
//        Publish(command);
//        await HandleReceiveInboxMessageCompletedAsync(item).ConfigureAwait(false);
//    }

//    private void ReplyToMessage(long reqMsgId, long ownerPeerId, int messageId, Guid correlationId)
//    {
//        var aggregateId = MessageId.Create(ownerPeerId, messageId);
//        var command = new ReplyToMessageCommand(aggregateId, reqMsgId, messageId, correlationId);
//        Publish(command);
//    }

//    private async Task CreateOutboxMessageAsync(long reqMsgId,
//        MessageItem messageItem,
//        bool clearDraft,
//        int groupItemCount,
//        Guid correlationId)
//    {
//        var idType = messageItem.ToPeer.PeerType == PeerType.Channel ? IdType.ChannelMessageId : IdType.MessageId;
//        var ownerPeerId = messageItem.OwnerPeer.PeerId;
//        var outMessageId = await IdGeneratorFactory.Default.NextIdAsync(idType, ownerPeerId).ConfigureAwait(false);
//        // TODO:create new MessageItem instance
//        messageItem.MessageId = outMessageId;
//        var aggregateId = MessageId.Create(messageItem.OwnerPeer.PeerId, messageItem.MessageId);

//        var command = new CreateOutboxMessageCommand(aggregateId,
//            reqMsgId,
//            messageItem,
//            clearDraft,
//            groupItemCount,
//            correlationId);
//        Publish(command);
//        Emit(new OutboxMessageIdGeneratedEvent(outMessageId));
//    }

//    private async Task CreateInboxMessageAsync()
//    {
//        ArgumentNullException.ThrowIfNull(_state.MessageItem);

//        switch (_state.MessageItem.ToPeer.PeerType)
//        {
//            case PeerType.User:
//                await CreateInboxMessageForUserPeerAsync(_state.MessageItem.ToPeer.PeerId).ConfigureAwait(false);
//                break;
//            case PeerType.Chat:
//                ArgumentNullException.ThrowIfNull(_state.ChatMemberUidList);
//                // todo:send message to removed member(Delete chat member)
//                foreach (var memberUid in _state.ChatMemberUidList)
//                {
//                    if (memberUid == _state.MessageItem.SenderPeer.PeerId)
//                    {
//                        continue;
//                    }

//                    await CreateInboxMessageForUserPeerAsync(memberUid).ConfigureAwait(false);
//                }
//                break;
//        }
//    }

//    private async Task CreateInboxMessageForUserPeerAsync(long inboxOwnerPeerId)
//    {
//        var outMessageItem = _state.MessageItem!;
//        var toPeer = outMessageItem.ToPeer.PeerType == PeerType.Chat ? outMessageItem.ToPeer : outMessageItem.OwnerPeer;

//        // Channel only create outbox message,
//        // the idType can only be IdType.MessageId for inbox message
//        var inboxMessageId = await IdGeneratorFactory.Default.NextIdAsync(IdType.MessageId, inboxOwnerPeerId).ConfigureAwait(false);
//        var aggregateId = MessageId.Create(inboxOwnerPeerId, inboxMessageId);
//        var inboxMessageItem = new MessageItem(aggregateId,
//            new Peer(PeerType.User, inboxOwnerPeerId),
//            toPeer,
//            outMessageItem.SenderPeer,
//            inboxMessageId,
//            outMessageItem.Message,
//            outMessageItem.Date,
//            outMessageItem.RandomId,
//            false,
//            outMessageItem.SendMessageType,
//            outMessageItem.MessageType,
//            outMessageItem.MessageSubType,
//            _state.GetReplyToMsgId(inboxOwnerPeerId),
//            outMessageItem.MessageActionData,
//            outMessageItem.MessageActionType,
//            outMessageItem.Entities,
//            outMessageItem.Media,
//            outMessageItem.GroupId,
//            outMessageItem.Post,
//            outMessageItem.FwdHeader,
//            outMessageItem.Views
//        );

//        var command = new CreateInboxMessageCommand(aggregateId, inboxMessageItem, outMessageItem.MessageId, _state.CorrelationId);
//        Publish(command);
//    }

//    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, ReplyToMessageEvent> domainEvent,
//        ISagaContext sagaContext,
//        CancellationToken cancellationToken)
//    {
//        AddReplyMessageIdToCache(domainEvent.AggregateEvent);
//        await HandleSendOutboxMessageCompletedAsync().ConfigureAwait(false);
//        await CreateInboxMessageAsync().ConfigureAwait(false);
//    }



//    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, ReplyToMessageStartedEvent> domainEvent,
//        ISagaContext sagaContext,
//        CancellationToken cancellationToken)
//    {
//        // Reply outbox message,the reply message sender is current message sender
//        if (domainEvent.AggregateEvent.IsOut)
//        {
//            //_inboxItems.TryAdd(domainEvent.AggregateEvent.SenderPeer.PeerId,
//            //    domainEvent.AggregateEvent.SenderMessageId);
//            await HandleSendOutboxMessageCompletedAsync().ConfigureAwait(false);
//            Emit(new ReplyToMessageCompletedEvent(domainEvent.AggregateEvent.InboxItems));
//            await CreateInboxMessageAsync().ConfigureAwait(false);
//        }
//        else
//        {
//            Emit(new ReplyToMessageCompletedEvent(new List<InboxItem> { new(domainEvent.AggregateEvent.SenderPeer.PeerId, domainEvent.AggregateEvent.SenderMessageId) }));
//            ReplyToMessage(_state.Request.ReqMsgId,
//                domainEvent.AggregateEvent.SenderPeer.PeerId,
//                domainEvent.AggregateEvent.SenderMessageId,
//                domainEvent.AggregateEvent.CorrelationId);
//        }
//    }

//    public Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, SendMessageStartedEvent> domainEvent,
//        ISagaContext sagaContext,
//        CancellationToken cancellationToken)
//    {
//        Emit(new MessageSagaStartedEvent(domainEvent.AggregateEvent.Request,
//            domainEvent.AggregateEvent.OutMessageItem,
//            domainEvent.AggregateEvent.ClearDraft,
//            domainEvent.AggregateEvent.GroupItemCount,
//            domainEvent.AggregateEvent.CorrelationId));

//        var item = domainEvent.AggregateEvent.OutMessageItem;
//        switch (domainEvent.AggregateEvent.OutMessageItem.ToPeer.PeerType)
//        {
//            case PeerType.User:
//                {
//                    var command = new CheckUserStateCommand(UserId.Create(item.OwnerPeer.PeerId),
//                        domainEvent.AggregateEvent.Request.ReqMsgId,
//                        domainEvent.AggregateEvent.CorrelationId);
//                    Publish(command);
//                }
//                break;
//            case PeerType.Chat:
//                {
//                    var command = new CheckChatStateCommand(ChatId.Create(item.ToPeer.PeerId),
//                        domainEvent.AggregateEvent.CorrelationId);
//                    Publish(command);
//                }
//                break;
//            case PeerType.Channel:
//                {
//                    Console.WriteLine($"channel msg id={item.MessageId}");
//                    var command = new CheckChannelStateCommand(ChannelId.Create(item.ToPeer.PeerId),
//                        item.SenderPeer.PeerId,
//                        item.MessageId,
//                        item.Date,
//                        item.MessageSubType,
//                        domainEvent.AggregateEvent.CorrelationId);
//                    Publish(command);
//                }
//                break;
//        }

//        return Task.CompletedTask;
//    }

//    public Task HandleAsync(IDomainEvent<UserAggregate, UserId, CheckUserStateCompletedEvent> domainEvent,
//        ISagaContext sagaContext,
//        CancellationToken cancellationToken)
//    {
//        ArgumentNullException.ThrowIfNull(_state.MessageItem);
//        return CreateOutboxMessageAsync(_state.Request.ReqMsgId, _state.MessageItem, _state.ClearDraft, _state.GroupItemCount, _state.CorrelationId);
//    }

//    public Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, CheckChatStateCompletedEvent> domainEvent,
//        ISagaContext sagaContext,
//        CancellationToken cancellationToken)
//    {
//        ArgumentNullException.ThrowIfNull(_state.MessageItem);
//        //_chatMemberUidList = domainEvent.AggregateEvent.MemberUidList;
//        Emit(new SendChatMessageStartedEvent(domainEvent.AggregateEvent.Title, domainEvent.AggregateEvent.MemberUidList));
//        return CreateOutboxMessageAsync(_state.Request.ReqMsgId, _state.MessageItem, _state.ClearDraft, _state.GroupItemCount, _state.CorrelationId);

//        //return Task.CompletedTask;
//    }

//    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, CheckChannelStateCompletedEvent> domainEvent,
//        ISagaContext sagaContext,
//        CancellationToken cancellationToken)
//    {
//        ArgumentNullException.ThrowIfNull(_state.MessageItem);
//        Emit(new SendChannelMessageStartedEvent(domainEvent.AggregateEvent.Post, domainEvent.AggregateEvent.Views, domainEvent.AggregateEvent.BotUidList, domainEvent.AggregateEvent.LinkedChannelId, domainEvent.AggregateEvent.CorrelationId));
//        var outboxMessageItem = _state.MessageItem;
//        outboxMessageItem.Post = domainEvent.AggregateEvent.Post;
//        outboxMessageItem.Views = domainEvent.AggregateEvent.Views;

//        return CreateOutboxMessageAsync(_state.Request.ReqMsgId, _state.MessageItem, _state.ClearDraft, _state.GroupItemCount, _state.CorrelationId);

//        //return Task.CompletedTask;
//    }

//    private async Task HandleSendOutboxMessageCompletedAsync()
//    {
//        var pts = await IdGeneratorFactory.Default.NextIdAsync(IdType.Pts, _state.MessageItem!.OwnerPeer.PeerId)
//            .ConfigureAwait(false);
//        Emit(new SendOutboxMessageCompletedEvent(_state.Request, _state.MessageItem, pts, _state.GroupItemCount));

//        if (_state.MessageItem.ToPeer.PeerType == PeerType.Channel)
//        {
//            SetChannelPts(_state.MessageItem.ToPeer.PeerId, pts, _state.MessageItem.MessageId);

//            if (_state.LinkedChannelId.HasValue && _state.MessageItem.SendMessageType != SendMessageType.MessageService)
//            {
//                ForwardBroadcastMessageToLinkedChannel(_state.LinkedChannelId.Value, _state.MessageItem.MessageId);
//            }

//            //await CompleteAndRemoveSnapshotFromCacheAsync().ConfigureAwait(false);
//            await CompleteAsync().ConfigureAwait(false);
//        }
//    }

//    private void ForwardBroadcastMessageToLinkedChannel(long linkedChannelId, int messageId)
//    {
//        var aggregateId = MessageId.Create(_state.MessageItem!.OwnerPeer.PeerId, messageId);
//        var fromPeer = _state.MessageItem!.ToPeer;
//        var toPeer = new Peer(PeerType.Channel, linkedChannelId);
//        var randomBytes = new byte[8];
//        Random.Shared.NextBytes(randomBytes);
//        var command = new StartForwardMessageCommand(aggregateId,
//            _state.Request,
//            fromPeer,
//            toPeer,
//            new List<int> { messageId },
//            new List<long> { BitConverter.ToInt64(randomBytes) },
//            //_state.CorrelationId
//            Guid.NewGuid()
//        );
//        Publish(command);
//    }

//    private async Task HandleReceiveInboxMessageCompletedAsync(MessageItem inboxMessageItem)
//    {
//        var pts = await IdGeneratorFactory.Default.NextIdAsync(IdType.Pts, inboxMessageItem.OwnerPeer.PeerId).ConfigureAwait(false);
//        Emit(new ReceiveInboxMessageCompletedEvent(inboxMessageItem, pts, _state.ChatTitle));

//        if (_state.IsSendMessageCompleted())
//        {
//            //await CompleteAndRemoveSnapshotFromCacheAsync().ConfigureAwait(false);
//            await CompleteAsync().ConfigureAwait(false);
//        }
//    }

//    //protected override Task<MessageSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
//    //{
//    //    return Task.FromResult(new MessageSnapshot(
//    //        _state.Request,
//    //        _state.MessageItem,
//    //        _state.SenderMessageId,
//    //        _state.ClearDraft, _state.GroupItemCount,
//    //        _state.InboxItems,
//    //        _state.ChatTitle,
//    //        _state.ChatMemberUidList,
//    //        _state.BotUidList,
//    //        _state.LinkedChannelId,
//    //        _state.CorrelationId));
//    //}

//    //protected override Task LoadSnapshotAsync(MessageSnapshot snapshot,
//    //    ISnapshotMetadata metadata,
//    //    CancellationToken cancellationToken)
//    //{
//    //    _state.LoadSnapshot(snapshot);
//    //    return Task.CompletedTask;
//    //}

//    private void AddReplyMessageIdToCache(ReplyToMessageEvent aggregateEvent)
//    {
//        if (aggregateEvent.InboxItems != null)
//        {
//            Emit(new ReplyToMessageCompletedEvent(aggregateEvent.InboxItems));
//        }
//    }

//    private void StartReplyToMessage(long ownerPeerId, int replyToMsgId, Guid correlationId)
//    {
//        var command =
//            new StartReplyToMessageCommand(MessageId.Create(ownerPeerId, replyToMsgId), replyToMsgId, correlationId);
//        Publish(command);
//    }

//    private void SetChannelPts(long channelId, int pts, int messageId)
//    {
//        var command = new SetChannelPtsCommand(ChannelId.Create(channelId), _state.MessageItem!.SenderPeer.PeerId, pts, messageId, _state.MessageItem.Date);
//        Publish(command);
//    }
//}

//[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<ReadingHistoryId>))]
//public class ReadingHistoryId : MyIdentity<ReadingHistoryId>
//{
//    public ReadingHistoryId(string value) : base(value)
//    {
//    }

//    public static ReadingHistoryId Create(long readerPeerId,
//        long targetPeerId,
//        int messageId)
//    {
//        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands,
//            $"ReadingHistoryId_{readerPeerId}_{targetPeerId}_{messageId}");
//    }
//}

//public class ReadingHistoryCreatedEvent : AggregateEvent<ReadingHistoryAggregate, ReadingHistoryId>
//{
//    public long ReaderPeerId { get; }
//    public long TargetPeerId { get; }
//    public int MessageId { get; }

//    public ReadingHistoryCreatedEvent(long readerPeerId, long targetPeerId, int messageId)
//    {
//        ReaderPeerId = readerPeerId;
//        TargetPeerId = targetPeerId;
//        MessageId = messageId;
//    }
//}

//public class ReadingHistoryState : AggregateState<ReadingHistoryAggregate, ReadingHistoryId, ReadingHistoryState>,
//    IApply<ReadingHistoryCreatedEvent>
//{
//    public long ReaderPeerId { get; private set; }
//    public long TargetPeerId { get; private set; }
//    public int MessageId { get; private set; }
//    public void Apply(ReadingHistoryCreatedEvent aggregateEvent)
//    {
//        ReaderPeerId = aggregateEvent.ReaderPeerId;
//        TargetPeerId = aggregateEvent.TargetPeerId;
//        MessageId = aggregateEvent.MessageId;
//    }
//}

//public class CreateReadingHistoryCommand : Command<ReadingHistoryAggregate, ReadingHistoryId, IExecutionResult>
//{
//    public long ReaderPeerId { get; }
//    public long TargetPeerId { get; }
//    public int MessageId { get; }

//    public CreateReadingHistoryCommand(ReadingHistoryId aggregateId,
//        /*RequestInfo request,*/ long readerPeerId,
//        long targetPeerId,
//        int messageId) : base(aggregateId)
//    {
//        ReaderPeerId = readerPeerId;
//        TargetPeerId = targetPeerId;
//        MessageId = messageId;
//    }
//}

//public class
//    CreateReadingHistoryCommandHandler : CommandHandler<ReadingHistoryAggregate, ReadingHistoryId,
//        CreateReadingHistoryCommand>
//{
//    public override Task ExecuteAsync(ReadingHistoryAggregate aggregate,
//        CreateReadingHistoryCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.Create(command.ReaderPeerId, command.TargetPeerId, command.MessageId);
//        return Task.CompletedTask;
//    }
//}

//public class ReadingHistoryAggregate : AggregateRoot<ReadingHistoryAggregate, ReadingHistoryId>
//{
//    private readonly ReadingHistoryState _state = new();
//    public ReadingHistoryAggregate(ReadingHistoryId id) : base(id)
//    {
//        Register(_state);
//    }

//    public void Create(
//        long readerPeerId,
//        long targetPeerId,
//        int messageId)
//    {
//        if (IsNew)
//        {
//            Emit(new ReadingHistoryCreatedEvent(readerPeerId, targetPeerId, messageId));
//        }
//    }
//}

//public class EditInboxMessageCommand : Command<MessageAggregate, MessageId, IExecutionResult>
//{
//    public int MessageId { get; }
//    public string NewMessage { get; }
//    public byte[]? Entities { get; }
//    public byte[]? Media { get; }
//    public int EditDate { get; }
//    public Guid CorrelationId { get; }

//    public EditInboxMessageCommand(MessageId aggregateId, int messageId, string newMessage, int editDate, byte[]? entities, byte[]? media, Guid correlationId) : base(aggregateId)
//    {
//        MessageId = messageId;
//        NewMessage = newMessage;
//        EditDate = editDate;
//        Entities = entities;
//        Media = media;
//        CorrelationId = correlationId;
//    }
//}

//public class EditInboxMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, EditInboxMessageCommand>
//{
//    public override Task ExecuteAsync(MessageAggregate aggregate,
//        EditInboxMessageCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.EditInboxMessage(command.MessageId,
//            command.NewMessage,
//            command.EditDate,
//            command.Entities,
//            command.Media,
//            command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}

