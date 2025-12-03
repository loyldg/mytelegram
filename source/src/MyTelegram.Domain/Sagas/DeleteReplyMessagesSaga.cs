namespace MyTelegram.Domain.Sagas;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<DeleteReplyMessagesSagaId>))]
public class DeleteReplyMessagesSagaId(string value) : Identity<DeleteReplyMessagesSagaId>(value), ISagaId;

public class DeleteReplyMessagesSagaLocator : DefaultSagaLocator<DeleteReplyMessagesSaga, DeleteReplyMessagesSagaId>
{
    protected override DeleteReplyMessagesSagaId CreateSagaId(string requestId)
    {
        return new DeleteReplyMessagesSagaId(requestId);
    }
}

public class DeleteReplyMessagesSagaState : AggregateState<DeleteReplyMessagesSaga, DeleteReplyMessagesSagaId,
    DeleteReplyMessagesSagaState>,
    IApply<DeleteReplyMessagesSagaStartedSagaEvent>,
    IApply<DeleteReplyMessagePtsIncrementedSagaEvent>,
    IApply<DeleteReplyMessagesCompletedSagaEvent>
{
    public long ChannelId { get; private set; }
    public int Pts { get; set; }
    public List<int> MessageIds { get; set; } = new();
    public int TotalCount { get; private set; }
    public int DeletedCount { get; private set; }
    public int NewTopMessageId { get; private set; }
    public void Apply(DeleteReplyMessagesSagaStartedSagaEvent aggregateEvent)
    {
        ChannelId = aggregateEvent.ChannelId;
        MessageIds = aggregateEvent.MessageIds;
        TotalCount = MessageIds.Count;
        NewTopMessageId = aggregateEvent.NewTopMessageId;
    }

    public void Apply(DeleteReplyMessagePtsIncrementedSagaEvent aggregateEvent)
    {
        if (Pts < aggregateEvent.Pts)
        {
            Pts = aggregateEvent.Pts;
        }
        DeletedCount++;
    }

    public void Apply(DeleteReplyMessagesCompletedSagaEvent aggregateEvent)
    {

    }
}

public class DeleteReplyMessagesSaga : MyInMemoryAggregateSaga<DeleteReplyMessagesSaga, DeleteReplyMessagesSagaId,
        DeleteReplyMessagesSagaLocator>,
    ISagaIsStartedBy<TempAggregate, TempId, DeleteReplyMessagesStartedEvent>,
    ISagaHandles<MessageAggregate, MessageId, ChannelMessageDeletedEvent>
{
    private readonly IIdGenerator _idGenerator;
    private readonly DeleteReplyMessagesSagaState _state = new();

    public DeleteReplyMessagesSaga(DeleteReplyMessagesSagaId id, IEventStore eventStore, IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
        Register(_state);
    }

    public Task HandleAsync(IDomainEvent<TempAggregate, TempId, DeleteReplyMessagesStartedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        Emit(new DeleteReplyMessagesSagaStartedSagaEvent(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.ChannelId,
            domainEvent.AggregateEvent.MessageIds,
            domainEvent.AggregateEvent.NewTopMessageId
        ));

        foreach (var messageId in domainEvent.AggregateEvent.MessageIds)
        {
            var command = new DeleteChannelMessageCommand(MessageId.Create(domainEvent.AggregateEvent.ChannelId, messageId),
                domainEvent.AggregateEvent.RequestInfo);
            Publish(command);
        }

        return Task.CompletedTask;
    }

    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, ChannelMessageDeletedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        await IncrementPtsAsync(domainEvent.AggregateEvent.ChannelId);

        HandleDeleteCompleted();
    }


    private void HandleDeleteCompleted()
    {
        if (_state.TotalCount == _state.DeletedCount)
        {
            Emit(new DeleteReplyMessagesCompletedSagaEvent(_state.ChannelId, _state.Pts, _state.TotalCount, _state.MessageIds, _state.NewTopMessageId));

            CompleteAsync();
        }
    }

    private async Task IncrementPtsAsync(long peerId)
    {
        var pts = await _idGenerator.NextIdAsync(IdType.Pts, peerId);
        Emit(new DeleteReplyMessagePtsIncrementedSagaEvent(peerId, pts));
    }

}

public class DeleteReplyMessagesCompletedSagaEvent(long channelId, int pts, int ptsCount,
        List<int> messageIds, int newTopMessageId)
    : AggregateEvent<DeleteReplyMessagesSaga, DeleteReplyMessagesSagaId>
{
    public long ChannelId { get; } = channelId;
    public int Pts { get; } = pts;
    public int PtsCount { get; } = ptsCount;
    public List<int> MessageIds { get; } = messageIds;
    public int NewTopMessageId { get; } = newTopMessageId;
}

public class DeleteReplyMessagePtsIncrementedSagaEvent(long channelId, int pts) : AggregateEvent<DeleteReplyMessagesSaga, DeleteReplyMessagesSagaId>
{
    public long ChannelId { get; } = channelId;
    public int Pts { get; } = pts;
}

public class
    DeleteReplyMessagesSagaStartedSagaEvent(RequestInfo requestInfo, long channelId, List<int> messageIds, int newTopMessageId)
    : AggregateEvent<DeleteReplyMessagesSaga, DeleteReplyMessagesSagaId>
{
    public RequestInfo RequestInfo { get; } = requestInfo;
    public long ChannelId { get; } = channelId;
    public List<int> MessageIds { get; } = messageIds;
    public int NewTopMessageId { get; } = newTopMessageId;
}


[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<DeleteChannelMessagesSagaId>))]
public class DeleteChannelMessagesSagaId(string value) : Identity<DeleteChannelMessagesSagaId>(value), ISagaId;

public class
    DeleteChannelMessagesSagaLocator : DefaultSagaLocator<DeleteChannelMessagesSaga, DeleteChannelMessagesSagaId>
{
    protected override DeleteChannelMessagesSagaId CreateSagaId(string requestId)
    {
        return new DeleteChannelMessagesSagaId(requestId);
    }
}

public class DeleteChannelMessagesSagaState : AggregateState<DeleteChannelMessagesSaga, DeleteChannelMessagesSagaId,
    DeleteChannelMessagesSagaState>,
    IApply<DeleteChannelMessagesSagaStartedSagaEvent>,
    IApply<DeleteChannelMessagePtsIncrementedSagaEvent>,
    IApply<DeleteChannelMessagesCompletedSagaEvent>,
    IApply<DeleteChannelHistoryCompletedSagaEvent>
{
    public RequestInfo RequestInfo { get; private set; } = null!;
    public long ChannelId { get; private set; }
    public int Pts { get; set; }
    public List<int> MessageIds { get; set; } = new();
    public int TotalCount { get; private set; }
    public int DeletedCount { get; private set; }
    public bool IsDeleteHistory { get; private set; }

    public long? DiscussionGroupChannelId { get; private set; }
    public List<int>? RepliesMessageIds { get; private set; }

    public int NewTopMessageId { get; private set; }
    public int? NewTopMessageIdForDiscussionGroup { get; private set; }

    public void Apply(DeleteChannelMessagesSagaStartedSagaEvent aggregateEvent)
    {
        RequestInfo = aggregateEvent.RequestInfo;
        ChannelId = aggregateEvent.ChannelId;
        MessageIds = aggregateEvent.MessageIds;
        TotalCount = aggregateEvent.MessageIds.Count;
        IsDeleteHistory = aggregateEvent.IsDeleteHistory;
        DiscussionGroupChannelId = aggregateEvent.DiscussionGroupChannelId;
        RepliesMessageIds = aggregateEvent.RepliesMessageIds?.ToList() ?? null;
        NewTopMessageId = aggregateEvent.NewTopMessageId;
        NewTopMessageIdForDiscussionGroup = aggregateEvent.NewTopMessageIdForDiscussionGroup;
    }

    public void Apply(DeleteChannelMessagePtsIncrementedSagaEvent aggregateEvent)
    {
        DeletedCount++;
        if (Pts < aggregateEvent.Pts)
        {
            Pts = aggregateEvent.Pts;
        }
    }

    public void Apply(DeleteChannelMessagesCompletedSagaEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }

    public void Apply(DeleteChannelHistoryCompletedSagaEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }
}

public class DeleteChannelMessagesSaga : MyInMemoryAggregateSaga<DeleteChannelMessagesSaga, DeleteChannelMessagesSagaId,
    DeleteChannelMessagesSagaLocator>,
    ISagaIsStartedBy<TempAggregate, TempId, DeleteChannelMessagesStartedEvent>,
    ISagaIsStartedBy<TempAggregate, TempId, DeleteParticipantHistoryStartedEvent>,
    ISagaHandles<MessageAggregate, MessageId, ChannelMessageDeletedEvent>
{
    private readonly IIdGenerator _idGenerator;
    private readonly DeleteChannelMessagesSagaState _state = new();

    public DeleteChannelMessagesSaga(DeleteChannelMessagesSagaId id, IEventStore eventStore, IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
        Register(_state);
    }

    public Task HandleAsync(IDomainEvent<TempAggregate, TempId, DeleteChannelMessagesStartedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        Emit(new DeleteChannelMessagesSagaStartedSagaEvent(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.ChannelId,
            domainEvent.AggregateEvent.MessageIds,
            domainEvent.AggregateEvent.NewTopMessageId,
            domainEvent.AggregateEvent.NewTopMessageIdForDiscussionGroup,
            false,
            domainEvent.AggregateEvent.DiscussionGroupChannelId,
            domainEvent.AggregateEvent.RepliesMessageIds
            ));
        foreach (var messageId in domainEvent.AggregateEvent.MessageIds)
        {
            var command = new DeleteChannelMessageCommand(MessageId.Create(domainEvent.AggregateEvent.ChannelId, messageId),
                domainEvent.AggregateEvent.RequestInfo);
            Publish(command);
        }

        return Task.CompletedTask;
    }

    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, ChannelMessageDeletedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        await IncrementPtsAsync(domainEvent.AggregateEvent.ChannelId);
        if (domainEvent.AggregateEvent.IsForwardFromChannelPost &&
            domainEvent.AggregateEvent.PostChannelId.HasValue &&
            domainEvent.AggregateEvent.PostMessageId.HasValue
            )
        {
            await HandleUpdateChannelPostMessageReplyAsync(domainEvent.AggregateEvent.PostChannelId.Value, domainEvent.AggregateEvent.PostMessageId.Value);
        }

        HandleDeleteCompleted();
    }

    private async Task HandleUpdateChannelPostMessageReplyAsync(long postChannelId, int postChannelMessageId)
    {
        var pts = await _idGenerator.NextIdAsync(IdType.Pts, postChannelId);

        var command = new UpdateMessageRelyCommand(MessageId.Create(postChannelId, postChannelMessageId), pts);
        Publish(command);
    }

    private void HandleDeleteCompleted()
    {
        if (_state.TotalCount == _state.DeletedCount)
        {
            if (_state.IsDeleteHistory)
            {
                Emit(new DeleteChannelHistoryCompletedSagaEvent(_state.RequestInfo, _state.ChannelId, _state.Pts, _state.TotalCount, _state.MessageIds, _state.NewTopMessageId));
            }
            else
            {
                Emit(new DeleteChannelMessagesCompletedSagaEvent(_state.RequestInfo, _state.ChannelId, _state.Pts, _state.TotalCount, _state.MessageIds, _state.NewTopMessageId));
                HandleDeleteReplyMessages();
            }

            CompleteAsync();
        }
    }

    private void HandleDeleteReplyMessages()
    {
        if (_state is { DiscussionGroupChannelId: not null, RepliesMessageIds.Count: > 0 })
        {
            var command = new StartDeleteReplyMessagesCommand(TempId.New,
                _state.RequestInfo with { RequestId = Guid.NewGuid() }, _state.DiscussionGroupChannelId.Value,
                _state.RepliesMessageIds,
                _state.NewTopMessageIdForDiscussionGroup ?? 0
                );
            Publish(command);
        }
    }

    private async Task IncrementPtsAsync(long peerId)
    {
        var pts = await _idGenerator.NextIdAsync(IdType.Pts, peerId);
        Emit(new DeleteChannelMessagePtsIncrementedSagaEvent(peerId, pts));
    }

    public Task HandleAsync(IDomainEvent<TempAggregate, TempId, DeleteParticipantHistoryStartedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        Emit(new DeleteChannelMessagesSagaStartedSagaEvent(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.ChannelId,
            domainEvent.AggregateEvent.MessageIds,
            domainEvent.AggregateEvent.NewTopMessageId,
            null,
            true, null, null));
        foreach (var messageId in domainEvent.AggregateEvent.MessageIds)
        {
            var command = new DeleteChannelMessageCommand(MessageId.Create(domainEvent.AggregateEvent.ChannelId, messageId),
                domainEvent.AggregateEvent.RequestInfo);
            Publish(command);
        }

        return Task.CompletedTask;
    }
}

public class DeleteChannelMessagesCompletedSagaEvent(RequestInfo requestInfo, long channelId, int pts, int ptsCount,
        List<int> messageIds, int newTopMessageId)
    : RequestAggregateEvent2<DeleteChannelMessagesSaga, DeleteChannelMessagesSagaId>(requestInfo)
{
    public long ChannelId { get; } = channelId;
    public int Pts { get; } = pts;
    public int PtsCount { get; } = ptsCount;
    public List<int> MessageIds { get; } = messageIds;
    public int NewTopMessageId { get; } = newTopMessageId;
}

public class DeleteChannelHistoryCompletedSagaEvent(RequestInfo requestInfo, long channelId, int pts, int ptsCount,
        List<int> messageIds,int newTopMessageId)
    : RequestAggregateEvent2<DeleteChannelMessagesSaga, DeleteChannelMessagesSagaId>(requestInfo)
{
    public long ChannelId { get; } = channelId;
    public int Pts { get; } = pts;
    public int PtsCount { get; } = ptsCount;
    public List<int> MessageIds { get; } = messageIds;
    public int NewTopMessageId { get; } = newTopMessageId;
}

public class DeleteChannelMessagePtsIncrementedSagaEvent(long channelId, int pts) : AggregateEvent<DeleteChannelMessagesSaga, DeleteChannelMessagesSagaId>
{
    public long ChannelId { get; } = channelId;
    public int Pts { get; } = pts;
}

public class
    DeleteChannelMessagesSagaStartedSagaEvent(RequestInfo requestInfo, long channelId, List<int> messageIds,
        int newTopMessageId,
        int? newTopMessageIdForDiscussionGroup,
        bool isDeleteHistory, long? discussionGroupChannelId, IReadOnlyCollection<int>? repliesMessageIds)
    : AggregateEvent<DeleteChannelMessagesSaga, DeleteChannelMessagesSagaId>
{
    public RequestInfo RequestInfo { get; } = requestInfo;
    public long ChannelId { get; } = channelId;
    public List<int> MessageIds { get; } = messageIds;
    public int NewTopMessageId { get; } = newTopMessageId;
    public int? NewTopMessageIdForDiscussionGroup { get; } = newTopMessageIdForDiscussionGroup;
    public bool IsDeleteHistory { get; } = isDeleteHistory;
    public long? DiscussionGroupChannelId { get; } = discussionGroupChannelId;
    public IReadOnlyCollection<int>? RepliesMessageIds { get; } = repliesMessageIds;
}
