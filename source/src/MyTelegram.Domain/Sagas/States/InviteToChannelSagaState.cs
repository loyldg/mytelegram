namespace MyTelegram.Domain.Sagas.States;

public class
    InviteToChannelSagaState : AggregateState<InviteToChannelSaga, InviteToChannelSagaId, InviteToChannelSagaState>,
        //IHasCorrelationId,
        IApply<InviteToChannelSagaStartSagaEvent>,
        IApply<InviteToChannelSagaMemberCreatedSagaEvent>,
        IApply<InviteToChannelCompletedSagaEvent>
{
    public int ChannelHistoryMinId { get; private set; }
    public long ChannelId { get; private set; }
    public bool Broadcast { get; private set; }

    public bool Completed => TotalCount == IncrementedCount;
    public int IncrementedCount { get; private set; }
    public long InviterId { get; private set; }
    public int MaxMessageId { get; private set; }
    public IReadOnlyCollection<long> MemberUserIds { get; private set; } = [];
    public IReadOnlyCollection<long> BotUserIds { get; private set; } = [];
    public string MessageActionData { get; private set; } = null!;

    public long RandomId { get; private set; }
    public RequestInfo RequestInfo { get; private set; } = default!;
    public int TotalCount { get; private set; }
    public bool HasLink { get; private set; }
    public ChatJoinType ChatJoinType { get; private set; }

    public void Apply(InviteToChannelSagaMemberCreatedSagaEvent aggregateEvent)
    {
        IncrementedCount++;
    }

    public void Apply(InviteToChannelSagaStartSagaEvent aggregateEvent)
    {
        RequestInfo = aggregateEvent.RequestInfo;
        ChannelId = aggregateEvent.ChannelId;
        InviterId = aggregateEvent.InviterId;
        TotalCount = aggregateEvent.MemberUserIds.Count;// + aggregateEvent.BotUserIds.Count;
        MemberUserIds = aggregateEvent.MemberUserIds;
        MaxMessageId = aggregateEvent.MaxMessageId;
        ChannelHistoryMinId = aggregateEvent.ChannelHistoryMinId;
        Broadcast = aggregateEvent.Broadcast;
        HasLink = aggregateEvent.HasLink;
        ChatJoinType = aggregateEvent.ChatJoinType;
    }

    public void Apply(InviteToChannelCompletedSagaEvent aggregateEvent)
    {
        
    }
}
