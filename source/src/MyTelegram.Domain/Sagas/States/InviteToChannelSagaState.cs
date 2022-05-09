namespace MyTelegram.Domain.Sagas.States;

public class
    InviteToChannelSagaState : AggregateState<InviteToChannelSaga, InviteToChannelSagaId, InviteToChannelSagaState>,
        IHasCorrelationId,
        IApply<InviteToChannelSagaStartEvent>,
        IApply<InviteToChannelSagaMemberCreatedEvent>
{
    public int ChannelHistoryMinId { get; private set; }
    public long ChannelId { get; private set; }

    public bool Completed => TotalCount == IncrementedCount;
    public int Date { get; private set; }

    public int IncrementedCount { get; private set; }
    public long InviterId { get; private set; }
    public int MaxMessageId { get; private set; }
    public IReadOnlyList<long> MemberUidList { get; private set; } = null!;
    public string MessageActionData { get; private set; } = null!;

    public long RandomId { get; private set; }
    public RequestInfo Request { get; private set; } = default!;
    public int TotalCount { get; private set; }

    public void Apply(InviteToChannelSagaMemberCreatedEvent aggregateEvent)
    {
        IncrementedCount++;
    }

    public void Apply(InviteToChannelSagaStartEvent aggregateEvent)
    {
        Request=aggregateEvent.Request;
        ChannelId = aggregateEvent.ChannelId;
        InviterId = aggregateEvent.InviterId;
        Date = aggregateEvent.Date;
        TotalCount = aggregateEvent.TotalCount;
        MemberUidList = aggregateEvent.MemberUidList;
        CorrelationId = aggregateEvent.CorrelationId;
        MaxMessageId = aggregateEvent.MaxMessageId;
        RandomId = aggregateEvent.RandomId;
        MessageActionData = aggregateEvent.MessageActionData;
        ChannelHistoryMinId = aggregateEvent.ChannelHistoryMinId;
    }

    public Guid CorrelationId { get; private set; }
}
