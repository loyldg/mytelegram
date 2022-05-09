namespace MyTelegram.Domain.Aggregates.UserName;

public class UserNameState : AggregateState<UserNameAggregate, UserNameId, UserNameState>,
    IApply<SetUserNameSuccessEvent>,
    IApply<UserNameDeletedEvent>
{
    public bool IsDeleted { get; private set; }
    public string UserName { get; private set; } = default!;

    public void Apply(SetUserNameSuccessEvent aggregateEvent)
    {
        IsDeleted = false;
        UserName = aggregateEvent.UserName;
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
