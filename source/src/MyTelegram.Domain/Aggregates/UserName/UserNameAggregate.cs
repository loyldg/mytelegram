namespace MyTelegram.Domain.Aggregates.UserName;

[EnableAutoGeneration]
public class UserNameAggregate : SnapshotAggregateRoot<UserNameAggregate, UserNameId, UserNameSnapshot>
{
    private readonly UserNameState _state = new();
    public UserNameAggregate(UserNameId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void CreateUserName(Peer peer, string userName, int date)
    {
        Emit(new UserNameCreatedEvent(peer, userName, date));
    }

    public void DeleteUserName()
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new UserNameDeletedEvent(_state.Peer));
    }

    [DoNotInheritRequestCommand]
    public void UpdateUserName(RequestInfo requestInfo,
        Peer peer,
        string? userName,
        string? oldUserName
        )
    {
        var date = DateTime.UtcNow.ToTimestamp();
        if (string.IsNullOrEmpty(userName))
        {
            Emit(new UserNameChangedEvent(requestInfo, peer, userName, oldUserName, date));
            return;
        }

        if (userName.Length > MyTelegramConsts.UsernameMaxLength || userName.Length < MyTelegramConsts.UsernameMinLength)
        {
            RpcErrors.RpcErrors400.UsernameInvalid.ThrowRpcError();
        }
        if (IsNew)
        {
            Emit(new UserNameChangedEvent(requestInfo, peer, userName, oldUserName, date));
        }
        else
        {
            if (_state.IsDeleted)
            {
                Emit(new UserNameChangedEvent(requestInfo, peer, userName, oldUserName, date));
            }
            else
            {
                RpcErrors.RpcErrors400.UsernameOccupied.ThrowRpcError();
            }
        }
    }

    protected override Task<UserNameSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new UserNameSnapshot(_state.UserName, _state.IsDeleted));
    }
    protected override Task LoadSnapshotAsync(UserNameSnapshot snapshot,
        ISnapshotMetadata metadata,
        CancellationToken cancellationToken)
    {
        _state.LoadSnapshot(snapshot);
        return Task.CompletedTask;
    }
}
