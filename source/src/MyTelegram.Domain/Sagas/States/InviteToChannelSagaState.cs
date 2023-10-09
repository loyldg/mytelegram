namespace MyTelegram.Domain.Sagas.States;

public class
    InviteToChannelSagaState : AggregateState<InviteToChannelSaga, InviteToChannelSagaId, InviteToChannelSagaState>,
        //IHasCorrelationId,
        IApply<InviteToChannelSagaStartEvent>,
        IApply<InviteToChannelSagaMemberCreatedEvent>
{
    public int ChannelHistoryMinId { get; private set; }
    public long ChannelId { get; private set; }
    public bool Broadcast { get; private set; }

    public bool Completed => TotalCount == IncrementedCount;
    public int Date { get; private set; }

    public int IncrementedCount { get; private set; }
    public long InviterId { get; private set; }
    public int MaxMessageId { get; private set; }
    public IReadOnlyList<long> MemberUidList { get; private set; } = null!;
    public IReadOnlyList<long>? PrivacyRestrictedUserId { get; private set; }
    public string MessageActionData { get; private set; } = null!;

    public long RandomId { get; private set; }
    public RequestInfo RequestInfo { get; private set; } = default!;
    public int TotalCount { get; private set; }

    public void Apply(InviteToChannelSagaMemberCreatedEvent aggregateEvent)
    {
        IncrementedCount++;
    }

    public void Apply(InviteToChannelSagaStartEvent aggregateEvent)
    {
        RequestInfo = aggregateEvent.RequestInfo;
        ChannelId = aggregateEvent.ChannelId;
        InviterId = aggregateEvent.InviterId;
        Date = aggregateEvent.Date;
        TotalCount = aggregateEvent.TotalCount;
        MemberUidList = aggregateEvent.MemberUidList;
        PrivacyRestrictedUserId = aggregateEvent.PrivacyRestrictedUserId;
        MaxMessageId = aggregateEvent.MaxMessageId;
        RandomId = aggregateEvent.RandomId;
        MessageActionData = aggregateEvent.MessageActionData;
        ChannelHistoryMinId = aggregateEvent.ChannelHistoryMinId;
        Broadcast = aggregateEvent.Broadcast;
    }
}