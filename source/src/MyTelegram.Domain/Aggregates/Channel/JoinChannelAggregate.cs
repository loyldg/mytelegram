namespace MyTelegram.Domain.Aggregates.Channel;

public class JoinChannelId(string value) : Identity<JoinChannelId>(value)
{
    public static JoinChannelId Create(long channelId, long userId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands, $"joinchannlid-{channelId}-{userId}");
    }
}

public record JoinChannelSnapshot(long ChannelId, long UserId, int Date, long? InviteId, /*long? InviterUserId, */bool Approved) : ISnapshot;

public class JoinChannelState : AggregateState<JoinChannelAggregate, JoinChannelId, JoinChannelState>,
    IApply<JoinChannelRequestCreatedEvent>,
    IApply<JoinChannelRequestUpdatedEvent>
{
    public long ChannelId { get; private set; }
    public long UserId { get; private set; }
    public int Date { get; private set; }
    public long? InviteId { get; private set; }
    public bool Approved { get; private set; }

    public void LoadSnapshot(JoinChannelSnapshot snapshot)
    {
        ChannelId = snapshot.ChannelId;
        UserId = snapshot.UserId;
        Date = snapshot.Date;
        InviteId = snapshot.InviteId;
        Approved = snapshot.Approved;
    }

    public void Apply(JoinChannelRequestCreatedEvent aggregateEvent)
    {
        ChannelId = aggregateEvent.ChannelId;
        UserId = aggregateEvent.UserId;
        Date = aggregateEvent.Date;
        InviteId = aggregateEvent.InviteId;
    }

    public void Apply(JoinChannelRequestUpdatedEvent aggregateEvent)
    {
        Approved = aggregateEvent.Approved;
    }
}

[EnableAutoGeneration]
public class JoinChannelAggregate :
    MyInMemorySnapshotAggregateRoot<JoinChannelAggregate, JoinChannelId, JoinChannelSnapshot>
{
    private readonly JoinChannelState _state = new();

    public JoinChannelAggregate(JoinChannelId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void HideChatJoinRequest(RequestInfo requestInfo, long userId, bool approved, int topMessageId, int channelHistoryMinId, bool broadcast)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new JoinChannelRequestUpdatedEvent(requestInfo, _state.ChannelId, userId, approved, _state.InviteId, topMessageId, channelHistoryMinId, broadcast));
    }

    public void CreateJoinChannelRequest(RequestInfo requestInfo, long channelId, long? inviteId)
    {
        var date = DateTime.UtcNow.ToTimestamp();
        Emit(new JoinChannelRequestCreatedEvent(requestInfo, channelId, requestInfo.UserId, date, inviteId));
    }

    protected override Task<JoinChannelSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new JoinChannelSnapshot(_state.ChannelId, _state.UserId, _state.Date, _state.InviteId, _state.Approved));
    }

    protected override Task LoadSnapshotAsync(JoinChannelSnapshot snapshot, ISnapshotMetadata metadata, CancellationToken cancellationToken)
    {
        _state.LoadSnapshot(snapshot);

        return Task.CompletedTask;
    }
}
