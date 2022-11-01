namespace MyTelegram.Domain.Aggregates.User;

public class UserAggregate : MyInMemorySnapshotAggregateRoot<UserAggregate, UserId, UserSnapshot>
{
    private readonly UserState _state = new();
    private readonly int AccountDefaultTtl = 365;

    public UserAggregate(UserId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void CheckUserState(Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new CheckUserStateCompletedEvent(correlationId));
    }

    public void CheckUserStatus(
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new CheckUserStatusCompletedEvent(_state.UserId,
            _state.PhoneNumber,
            _state.FirstName,
            _state.LastName,
            _state.HasPassword,
            false,
            correlationId));
    }

    public void Create(RequestInfo requestInfo,
        long userId,
        long accessHash,
        string phoneNumber,
        string firstName,
        string? lastName = null,
        bool bot = false)
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        Specs.IsNotEmptyOrNull.ThrowDomainErrorIfNotSatisfied(firstName);

        Emit(new UserCreatedEvent(requestInfo,
            userId,
            accessHash,
            phoneNumber,
            firstName,
            lastName,
            bot,
            bot ? 0 : null,
            AccountDefaultTtl,
            DateTime.UtcNow
        ));
    }

    public void SetSupport(bool support)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new UserSupportHasSetEvent(support));
    }

    public void SetVerified(bool verified)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new UserVerifiedHasSetEvent(verified));
    }

    public void UpdateProfile(long reqMsgId,
        string? firstName,
        string? lastName,
        string? about)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new UserProfileUpdatedEvent(reqMsgId,
            _state.UserId,
            firstName,
            lastName,
            about));
    }

    public void UpdateProfilePhoto(long reqMsgId,
        long fileId,
        byte[] photo /*, bool hasVideo, double videoStartTs*/)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new UserProfilePhotoChangedEvent(reqMsgId,
            fileId,
            new UserItem(_state.UserId,
                _state.AccessHash,
                _state.PhoneNumber,
                _state.FirstName,
                _state.LastName,
                _state.UserName,
                photo) /*, hasVideo, videoStartTs*/));
    }

    public void UpdateUserName(long reqMsgId,
        string userName,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new UserNameUpdatedEvent(reqMsgId,
            new UserItem(_state.UserId,
                _state.AccessHash,
                _state.PhoneNumber,
                _state.FirstName,
                _state.LastName,
                userName,
                _state.Photo),
            _state.UserName,
            correlationId));
    }

    protected override Task<UserSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new UserSnapshot(_state.UserId,
            _state.IsOnline,
            _state.AccessHash,
            _state.FirstName,
            _state.LastName,
            _state.PhoneNumber,
            _state.UserName,
            _state.HasPassword,
            _state.Photo));
    }

    protected override Task LoadSnapshotAsync(UserSnapshot snapshot,
        ISnapshotMetadata metadata,
        CancellationToken cancellationToken)
    {
        _state.LoadFromSnapshot(snapshot);
        return Task.CompletedTask;
    }
}
