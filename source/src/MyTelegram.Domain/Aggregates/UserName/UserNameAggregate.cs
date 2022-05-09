namespace MyTelegram.Domain.Aggregates.UserName;

public class UserNameAggregate : SnapshotAggregateRoot<UserNameAggregate, UserNameId, UserNameSnapshot>
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

    public void SetUserName(long reqMsgId,
        long selfUserId,
        PeerType peerType,
        long peerId,
        string userName,
        Guid correlationId)
    {
        if (userName.Length > 32 || userName.Length < 5)
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.UserNameInvalid);
        }

        if (IsNew)
        {
            Emit(new SetUserNameSuccessEvent(reqMsgId,
                selfUserId,
                userName,
                peerType,
                peerId,
                correlationId));
        }
        else
        {
            if (_state.IsDeleted)
            {
                Emit(new SetUserNameSuccessEvent(reqMsgId,
                    selfUserId,
                    userName,
                    peerType,
                    peerId,
                    correlationId));
            }
            else
            {
                ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.UserNameOccupied);
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
