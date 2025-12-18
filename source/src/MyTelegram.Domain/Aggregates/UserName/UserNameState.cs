namespace MyTelegram.Domain.Aggregates.UserName;

public class UserNameState : AggregateState<UserNameAggregate, UserNameId, UserNameState>,
    IApply<UserNameCreatedEvent>,
    IApply<UserNameChangedEvent>,
    IApply<UserNameDeletedEvent>
{
    public bool IsDeleted { get; private set; }
    public Peer Peer { get; private set; } = default!;
    public string? UserName { get; private set; }

    public void Apply(UserNameCreatedEvent aggregateEvent)
    {
        UserName = aggregateEvent.UserName;

        IsDeleted = false;
    }

    public void Apply(UserNameChangedEvent aggregateEvent)
    {
        UserName = aggregateEvent.UserName;
        Peer = aggregateEvent.Peer;
        IsDeleted = string.IsNullOrEmpty(aggregateEvent.UserName);
    }

    public void Apply(UserNameDeletedEvent aggregateEvent)
    {
        IsDeleted = true;
    }

    public void LoadSnapshot(UserNameSnapshot snapshot)
    {
        UserName = snapshot.UserName;
        IsDeleted = snapshot.IsDeleted;
    }
}
