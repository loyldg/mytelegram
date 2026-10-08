namespace MyTelegram.Domain.Aggregates.Poll;

public class PollVoterId : Identity<PollVoterId>
{
    public PollVoterId(string value) : base(value)
    {
    }

    public static PollVoterId Create(long pollId, string option, long voterPeerId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands, $"pollvoter-{pollId}-{option}-{voterPeerId}");
    }
}

public class PollVoterState : AggregateState<PollVoterAggregate, PollVoterId, PollVoterState>,
    IApply<PollVoterCreatedEvent>,
    IApply<PollVoterDeletedEvent>
{
    public long PollId { get; private set; }
    public long VoterPeerId { get; private set; }
    public string Option { get; private set; } = null!;

    public void LoadSnapshot(PollVoterSnapshot snapshot)
    {
        PollId = snapshot.PollId;
        Option = snapshot.Option;
        VoterPeerId = snapshot.VoterPeerId;
    }

    public void Apply(PollVoterCreatedEvent aggregateEvent)
    {
        PollId = aggregateEvent.PollId;
        Option = aggregateEvent.Option;
        VoterPeerId = aggregateEvent.VoterPeerId;
    }

    public void Apply(PollVoterDeletedEvent aggregateEvent)
    {

    }
}

public record PollVoterSnapshot(long PollId, string Option, long VoterPeerId) : ISnapshot;

[EnableAutoGeneration]
public class PollVoterAggregate : SnapshotAggregateRoot<PollVoterAggregate, PollVoterId, PollVoterSnapshot>
{
    private readonly PollVoterState _state = new();
    public PollVoterAggregate(PollVoterId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void Create(long pollId, string option, long voterPeerId, bool correct)
    {
        var date = DateTime.UtcNow.ToTimestamp();
        Emit(new PollVoterCreatedEvent(Id, pollId, option, voterPeerId, date, correct));
    }

    public void DeletePollVoter()
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new PollVoterDeletedEvent(_state.PollId, _state.VoterPeerId));
    }

    protected override Task<PollVoterSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new PollVoterSnapshot(_state.PollId, _state.Option, _state.VoterPeerId));
    }

    protected override Task LoadSnapshotAsync(PollVoterSnapshot snapshot, ISnapshotMetadata metadata, CancellationToken cancellationToken)
    {
        _state.LoadSnapshot(snapshot);

        return Task.CompletedTask;
    }
}
