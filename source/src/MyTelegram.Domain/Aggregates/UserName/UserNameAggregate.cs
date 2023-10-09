namespace MyTelegram.Domain.Aggregates.UserName;

public class UserNameAggregate : MySnapshotAggregateRoot<UserNameAggregate, UserNameId, UserNameSnapshot>
{
    private readonly UserNameState _state = new();
    public UserNameAggregate(UserNameId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void Delete()
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new UserNameDeletedEvent());
    }

    public void Create(long userId, string userName)
    {
        if (userName.Length > 32 || userName.Length < 5)
        {
            RpcErrors.RpcErrors400.UsernameInvalid.ThrowRpcError();
        }

        if (IsNew)
        {
            Emit(new UserNameCreatedEvent(userId, userName));
        }
        else
        {
            if (_state.IsDeleted)
            {
                Emit(new UserNameCreatedEvent(userId, userName));
            }
            else
            {
                RpcErrors.RpcErrors400.UsernameOccupied.ThrowRpcError();
            }
        }
    }

    public void SetUserName(RequestInfo requestInfo,
        long selfUserId,
        PeerType peerType,
        long peerId,
        string userName)
    {
        if (userName.Length > 32 || userName.Length < 5)
        {
            //ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.UserNameInvalid);
            RpcErrors.RpcErrors400.UsernameInvalid.ThrowRpcError();
        }

        if (IsNew)
        {
            Emit(new SetUserNameSuccessEvent(requestInfo,
                selfUserId,
                userName,
                peerType,
                peerId));
        }
        else
        {
            if (_state.IsDeleted)
            {
                Emit(new SetUserNameSuccessEvent(requestInfo,
                    selfUserId,
                    userName,
                    peerType,
                    peerId));
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
