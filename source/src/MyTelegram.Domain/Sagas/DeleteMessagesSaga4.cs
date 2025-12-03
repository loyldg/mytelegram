namespace MyTelegram.Domain.Sagas;

public class DeleteMessageSaga4StartedEvent(RequestInfo requestInfo,
    //List<int> messageIds,
    IReadOnlyCollection<MessageItemToBeDeleted> messageItems,
    bool revoke,
    bool deleteGroupMessagesForEveryone,
    bool isDeleteHistory,
    int? newTopMessageId,
    int? newTopMessageIdForOtherParticipant,
    bool isDeletePhoneCallHistory
    ) : AggregateEvent<DeleteMessagesSaga4, DeleteMessagesSaga4Id>
{
    public bool DeleteGroupMessagesForEveryone { get; } = deleteGroupMessagesForEveryone;
    public bool IsDeleteHistory { get; } = isDeleteHistory;
    public int? NewTopMessageId { get; } = newTopMessageId;
    public int? NewTopMessageIdForOtherParticipant { get; } = newTopMessageIdForOtherParticipant;

    public bool IsDeletePhoneCallHistory { get; } = isDeletePhoneCallHistory;

    //public List<int> MessageIds { get; } = messageIds;
    public RequestInfo RequestInfo { get; } = requestInfo;
    public IReadOnlyCollection<MessageItemToBeDeleted> MessageItems { get; } = messageItems;
    public bool Revoke { get; } = revoke;
}

public class MessageDeletedSagaEvent(MessageItemToBeDeleted messageItem, int pts)
    : AggregateEvent<DeleteMessagesSaga4, DeleteMessagesSaga4Id>
{
    public MessageItemToBeDeleted MessageItem { get; } = messageItem;
    public int Pts { get; } = pts;
}

public class
    DeleteMessagesSaga4 : MyInMemoryAggregateSaga<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteMessagesSaga4Locator>,
    ISagaIsStartedBy<TempAggregate, TempId, DeleteMessagesStartedEvent>,
    ISagaIsStartedBy<TempAggregate, TempId, DeleteHistoryStartedEvent>,

    ISagaHandles<MessageAggregate, MessageId, MessageDeleted4Event>,
    //ISagaHandles<MessageAggregate, MessageId, OutboxMessageDeletedEvent>,
    //ISagaHandles<MessageAggregate, MessageId, InboxMessageDeletedEvent>,
    IApply<DeleteSelfMessagesCompletedSagaEvent>,
    IApply<DeleteOtherParticipantMessagesCompletedSagaEvent>,
    IApply<DeleteSelfHistoryCompletedSagaEvent>,
    IApply<DeleteOtherParticipantHistoryCompletedSagaEvent>

{
    private readonly IIdGenerator _idGenerator;
    private readonly DeleteMessagesSaga4State _state = new();

    public DeleteMessagesSaga4(DeleteMessagesSaga4Id id, IEventStore eventStore, IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
        Register(_state);
    }

    public DeleteMessagesSaga4State SagaState => _state;
    public void Apply(DeleteSelfMessagesCompletedSagaEvent aggregateEvent)
    {
        HandleDeleteAllMessagesCompleted();
    }

    public void Apply(DeleteOtherParticipantMessagesCompletedSagaEvent aggregateEvent)
    {
        HandleDeleteAllMessagesCompleted();
    }

    public Task HandleAsync(IDomainEvent<TempAggregate, TempId, DeleteMessagesStartedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        //Emit(new DeleteMessageSaga4StartedEvent(domainEvent.AggregateEvent.RequestInfo,
        //    domainEvent.AggregateEvent.MessageIds,
        //    domainEvent.AggregateEvent.Revoke,
        //    domainEvent.AggregateEvent.DeleteGroupMessagesForEveryone
        //    ));
        //foreach (var messageId in domainEvent.AggregateEvent.MessageIds)
        //{
        //    var command = new DeleteMessageCommand(
        //        MessageId.Create(domainEvent.AggregateEvent.RequestInfo.UserId, messageId),
        //        domainEvent.AggregateEvent.RequestInfo);
        //    Publish(command);
        //}

        StartDeleteMessagesOrDeleteHistory(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.MessageItems, domainEvent.AggregateEvent.Revoke,
            domainEvent.AggregateEvent.DeleteGroupMessagesForEveryone, false,
            domainEvent.AggregateEvent.NewTopMessageId,
            domainEvent.AggregateEvent.NewTopMessageIdForOtherParticipant,
            false
            );

        //foreach (var item in domainEvent.AggregateEvent.MessageItems)
        //{
        //    var command = new DeleteMessageCommand(MessageId.Create(item.OwnerUserId, item.MessageId),
        //        domainEvent.AggregateEvent.RequestInfo);
        //    Publish(command);
        //}

        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<TempAggregate, TempId, DeleteHistoryStartedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        StartDeleteMessagesOrDeleteHistory(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.MessageItems,
            domainEvent.AggregateEvent.Revoke,
            domainEvent.AggregateEvent.DeleteGroupMessagesForEveryone,
            true,
            null,
            null,
            domainEvent.AggregateEvent.IsDeletePhoneCallHistory
            );

        return Task.CompletedTask;
    }

    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, MessageDeleted4Event> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        var pts = await IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId);
        Emit(new MessageDeletedSagaEvent(new MessageItemToBeDeleted(domainEvent.AggregateEvent.OwnerPeerId,
            domainEvent.AggregateEvent.MessageId,
            domainEvent.AggregateEvent.ToPeer.PeerType,
            domainEvent.AggregateEvent.ToPeer.PeerId), pts));
        HandleDeleteMessagesCompleted();
    }

    //public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, OutboxMessageDeletedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    //{
    //    var pts = await IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId);
    //    Emit(new OutboxMessageDeletedSagaEvent(domainEvent.AggregateEvent.OwnerPeerId, domainEvent.AggregateEvent.MessageId, domainEvent.AggregateEvent.InboxItems ?? Array.Empty<InboxItem>()));
    //    Emit(new OtherParticipantMessageDeletedSagaEvent(domainEvent.AggregateEvent.OwnerPeerId, domainEvent.AggregateEvent.MessageId, domainEvent.AggregateEvent.MessageId, pts));
    //    HandleDeleteOtherParticipantMessages(true, domainEvent.AggregateEvent.OwnerPeerId,
    //        domainEvent.AggregateEvent.MessageId, domainEvent.AggregateEvent.InboxItems);

    //    HandleDeleteOtherParticipantMessagesCompleted(domainEvent.AggregateEvent.OwnerPeerId, pts);
    //}

    //public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, InboxMessageDeletedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    //{
    //    var pts = await IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId);
    //    Emit(new OtherParticipantMessageDeletedSagaEvent(domainEvent.AggregateEvent.OwnerPeerId, domainEvent.AggregateEvent.SenderMessageId, domainEvent.AggregateEvent.MessageId, pts));
    //    HandleDeleteOtherParticipantMessagesCompleted(domainEvent.AggregateEvent.OwnerPeerId, pts);
    //}

    private void HandleDeleteAllMessagesCompleted()
    {
        if (_state.DeletedCount == _state.TotalCount)
        {
            CompleteAsync();
        }
    }

    private void HandleDeleteMessagesCompleted()
    {
        if (_state.DeletedCount == _state.TotalCount)
        {
            if (_state.NewTopMessageId.HasValue)
            {
                var command = new UpdateDialogTopMessageIdCommand(
                    DialogId.Create(_state.RequestInfo.UserId, _state.ToPeer), _state.NewTopMessageId.Value);
                Publish(command);
            }

            if (_state.NewTopMessageIdForOtherParticipant.HasValue)
            {
                var command = new UpdateDialogTopMessageIdCommand(
                    DialogId.Create(_state.ToPeer.PeerId, PeerType.User, _state.RequestInfo.UserId),
                    _state.NewTopMessageIdForOtherParticipant.Value);
                Publish(command);
            }

            foreach (var kv in _state.DeletedMessageIds)
            {
                var userId = kv.Key;
                var item = kv.Value;
                var pts = item.Pts;
                if (userId == _state.RequestInfo.UserId)
                {
                    if (_state.IsDeleteHistory)
                    {
                        Emit(new DeleteSelfHistoryCompletedSagaEvent(_state.RequestInfo,
                            pts,
                            item.MessageIds.Count,
                            item.MessageIds.Min(),
                            item.MessageIds,
                            _state.IsDeletePhoneCallHistory
                            ));
                    }
                    else
                    {
                        Emit(new DeleteSelfMessagesCompletedSagaEvent(_state.RequestInfo, pts, item.MessageIds.Count, item.MessageIds));
                    }
                }
                else
                {
                    if (_state.IsDeleteHistory)
                    {
                        Emit(new DeleteOtherParticipantHistoryCompletedSagaEvent(userId, pts, item.MessageIds.Count, item.MessageIds));
                    }
                    else
                    {
                        Emit(new DeleteOtherParticipantMessagesCompletedSagaEvent(userId, pts, item.MessageIds.Count, item.MessageIds));
                    }
                }
            }
        }

        HandleDeleteAllMessagesCompleted();
    }

    private async Task<int> IncrementPtsAsync(long userId)
    {
        var pts = await _idGenerator.NextIdAsync(IdType.Pts, userId);
        Emit(new DeleteMessagePtsIncrementedSagaEvent(userId, pts));

        return pts;
    }

    private void StartDeleteMessagesOrDeleteHistory(RequestInfo requestInfo,
        //List<int> messageIds,
        IReadOnlyCollection<MessageItemToBeDeleted> messageItems,
        bool revoke,
        bool deleteGroupMessagesForEveryone,
        bool isDeleteHistory,
        int? newTopMessageId,
        int? newTopMessageIdForOtherParticipant,
        bool isDeletePhoneCallHistory
        )
    {
        Emit(new DeleteMessageSaga4StartedEvent(requestInfo, messageItems, revoke, deleteGroupMessagesForEveryone, isDeleteHistory, newTopMessageId, newTopMessageIdForOtherParticipant, isDeletePhoneCallHistory));
        foreach (var item in messageItems)
        {
            var command = new DeleteMessageCommand(
                MessageId.Create(item.OwnerUserId, item.MessageId),
                requestInfo);
            Publish(command);
        }
    }

    public void Apply(DeleteSelfHistoryCompletedSagaEvent aggregateEvent)
    {
        HandleDeleteAllMessagesCompleted();
    }

    public void Apply(DeleteOtherParticipantHistoryCompletedSagaEvent aggregateEvent)
    {
        HandleDeleteAllMessagesCompleted();
    }
}

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<DeleteMessagesSaga4Id>))]
public class DeleteMessagesSaga4Id(string value) : SingleValueObject<string>(value), ISagaId;

public class DeleteMessagesSaga4Locator : DefaultSagaLocator<DeleteMessagesSaga4, DeleteMessagesSaga4Id>
{
    protected override DeleteMessagesSaga4Id CreateSagaId(string requestId)
    {
        return new DeleteMessagesSaga4Id(requestId);
    }
}
public class DeleteMessagesSaga4State : AggregateState<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteMessagesSaga4State>,
    IApply<DeleteMessageSaga4StartedEvent>,
    //IApply<SelfMessageDeletedSagaEvent>,
    //IApply<OtherParticipantMessageDeletedSagaEvent>,
    IApply<DeleteMessagePtsIncrementedSagaEvent>,
    //IApply<OutboxMessageDeletedSagaEvent>,
    //IApply<DeleteOtherParticipantMessagesCompletedEvent4>,
    IApply<MessageDeletedSagaEvent>
//,
//IApply<DeleteSelfMessagesCompletedEvent4>
{
    public bool DeleteGroupMessagesForEveryone { get; private set; }
    public bool IsDeleteHistory { get; private set; }
    //public Dictionary<int, MessageIdToDeletedItem> MessageIdToDeletedItems { get; set; } = new();
    public RequestInfo RequestInfo { get; private set; } = default!;
    public bool Revoke { get; private set; }
    public Peer ToPeer { get; private set; } = default!;
    //public Dictionary<long, List<int>> UserIdToDeletedMessageIds { get; set; } = new();
    //public Dictionary<long, int> UserIdToPtsDict { get; set; } = new();
    //public Dictionary<long, DeletedItem> UserIdToDeletedItems { get; set; } = new();
    //public bool WaitingDeleteOutboxMessage { get; set; }

    public int? NewTopMessageId { get; private set; }
    public int? NewTopMessageIdForOtherParticipant { get; private set; }
    public Dictionary<long, DeletedItem> DeletedMessageIds { get; private set; } = new();
    public int TotalCount { get; private set; }
    public int DeletedCount { get; private set; }
    public Dictionary<long, int> UserIdToPts = new();
    public bool IsDeletePhoneCallHistory { get; private set; }

    public void Apply(DeleteMessageSaga4StartedEvent aggregateEvent)
    {
        RequestInfo = aggregateEvent.RequestInfo;
        Revoke = aggregateEvent.Revoke;
        DeleteGroupMessagesForEveryone = aggregateEvent.DeleteGroupMessagesForEveryone;
        IsDeleteHistory = aggregateEvent.IsDeleteHistory;
        NewTopMessageId = aggregateEvent.NewTopMessageId;
        NewTopMessageIdForOtherParticipant = aggregateEvent.NewTopMessageIdForOtherParticipant;
        IsDeletePhoneCallHistory = aggregateEvent.IsDeletePhoneCallHistory;
        //UserIdToDeletedItems.TryAdd(aggregateEvent.RequestInfo.UserId, new DeletedItem
        //{
        //    TotalCount = aggregateEvent.MessageItems.Count,
        //});

        TotalCount = aggregateEvent.MessageItems.Count;

        var item = aggregateEvent.MessageItems.FirstOrDefault(p => p.OwnerUserId == aggregateEvent.RequestInfo.UserId);
        if (item != null)
        {
            ToPeer = new Peer(item.ToPeerType, item.ToPeerId);
        }
    }

    public void Apply(DeleteOtherParticipantMessagesCompletedSagaEvent aggregateEvent)
    {
    }

    public void Apply(DeleteMessagePtsIncrementedSagaEvent aggregateEvent)
    {
    }

    public void Apply(MessageDeletedSagaEvent aggregateEvent)
    {
        if (!DeletedMessageIds.TryGetValue(aggregateEvent.MessageItem.OwnerUserId, out var item))
        {
            item = new(aggregateEvent.MessageItem.OwnerUserId, new(), aggregateEvent.Pts);
            DeletedMessageIds.TryAdd(aggregateEvent.MessageItem.OwnerUserId, item);
        }
        item.MessageIds.Add(aggregateEvent.MessageItem.MessageId);
        item.Pts = aggregateEvent.Pts;

        DeletedCount++;
    }

    public class DeletedItem(long userId, List<int> messageIds, int pts)
    {
        public long UserId { get; init; } = userId;
        public List<int> MessageIds { get; init; } = messageIds;
        public int Pts { get; set; } = pts;
    }
}



public class DeleteOtherParticipantHistoryCompletedSagaEvent(long userId, int pts, int ptsCount, List<int> messageIds)
    : AggregateEvent<DeleteMessagesSaga4, DeleteMessagesSaga4Id>
{
    public List<int> MessageIds { get; } = messageIds;
    public int Pts { get; } = pts;
    public int PtsCount { get; } = ptsCount;
    public long UserId { get; } = userId;
}

public class DeleteOtherParticipantMessagesCompletedSagaEvent(long userId, int pts, int ptsCount, List<int> messageIds)
    : AggregateEvent<DeleteMessagesSaga4, DeleteMessagesSaga4Id>
{
    public List<int> MessageIds { get; } = messageIds;
    public int Pts { get; } = pts;
    public int PtsCount { get; } = ptsCount;
    public long UserId { get; } = userId;
}

public class DeleteSelfHistoryCompletedSagaEvent(RequestInfo requestInfo, int pts, int ptsCount, int offset, List<int> messageIds, bool isDeletePhoneCallHistory)
    : RequestAggregateEvent2<DeleteMessagesSaga4, DeleteMessagesSaga4Id>(requestInfo)
{
    public int Offset { get; } = offset;
    public List<int> MessageIds { get; } = messageIds;
    public bool IsDeletePhoneCallHistory { get; } = isDeletePhoneCallHistory;
    public int Pts { get; } = pts;
    public int PtsCount { get; } = ptsCount;
}

public class DeleteSelfMessagesCompletedSagaEvent(RequestInfo requestInfo, int pts, int ptsCount, List<int> messageIds)
    : RequestAggregateEvent2<DeleteMessagesSaga4, DeleteMessagesSaga4Id>(requestInfo)
{
    public int Pts { get; } = pts;
    public int PtsCount { get; } = ptsCount;
    public List<int> MessageIds { get; } = messageIds;
}

public class DeleteMessagePtsIncrementedSagaEvent(
    long userId,
    int pts) : AggregateEvent<DeleteMessagesSaga4, DeleteMessagesSaga4Id>
{
    public int Pts { get; } = pts;
    public long UserId { get; } = userId;
}