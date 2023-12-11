namespace MyTelegram.Domain.Aggregates.User;

public class UserAggregate : MyInMemorySnapshotAggregateRoot<UserAggregate, UserId, UserSnapshot>
{
    private readonly UserState _state = new();
    private readonly int AccountDefaultTtl = 365;

    public UserAggregate(UserId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void UpdateColor(RequestInfo requestInfo, PeerColor? color, bool forProfile)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new UserColorUpdatedEvent(requestInfo, _state.UserId, color, forProfile));
    }

    public void UploadProfilePhoto(RequestInfo requestInfo,
        long photoId,
        bool fallback,
        //byte[]? photo,
        VideoSizeEmojiMarkup? videoEmojiMarkup /*, bool hasVideo, double videoStartTs*/)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new UserProfilePhotoUploadedEvent(requestInfo,
            photoId,
            fallback,
            //new UserItem(_state.UserId,
            //    _state.AccessHash,
            //    _state.PhoneNumber,
            //    _state.FirstName,
            //    _state.LastName,
            //    _state.UserName),
            videoEmojiMarkup
            /*, hasVideo, videoStartTs*/));
    }

    public void CheckUserState(Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new CheckUserStateCompletedEvent(correlationId));
    }

    private void CheckUserDeletionState()
    {
        if (_state.IsDeleted)
        {
            RpcErrors.RpcErrors400.PeerIdInvalid.ThrowRpcError();
        }
    }

    public void CheckUserStatus(RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        CheckUserDeletionState();

        Emit(new CheckUserStatusCompletedEvent(
            requestInfo,
            _state.UserId,
            _state.AccessHash,
            _state.PhoneNumber,
            _state.FirstName,
            _state.LastName,
            _state.HasPassword,
            false));
    }

    public void Create(RequestInfo requestInfo,
        long userId,
        long accessHash,
        string phoneNumber,
        string firstName,
        string? lastName = null,
        string? userName=null,
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
            userName,   
            bot,
            bot ? 0 : null,
            AccountDefaultTtl,
            DateTime.UtcNow
        ));
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
            _state.Photo,
            _state.IsBot,
            _state.IsDeleted,
            _state.EmojiStatusDocumentId,
            _state.EmojiStatusValidUntil,
            _state.RecentEmojiStatus.ToList(),
            _state.PhotoId,
            _state.FallbackPhotoId,
            _state.Color,
            _state.ProfileColor
        ));
    }

    protected override Task LoadSnapshotAsync(UserSnapshot snapshot,
        ISnapshotMetadata metadata,
        CancellationToken cancellationToken)
    {
        _state.LoadFromSnapshot(snapshot);
        return Task.CompletedTask;
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

    public void UpdateProfile(RequestInfo requestInfo,
        string? firstName,
        string? lastName,
        string? about)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new UserProfileUpdatedEvent(requestInfo,
            _state.UserId,
            firstName,
            lastName,
            about));
    }

    public void UpdateProfilePhoto(RequestInfo requestInfo,
            //long userId,
            long photoId,
            bool fallback)//,
        //byte[]? photo, 
        //VideoSizeEmojiMarkup? videoEmojiMarkup /*, bool hasVideo, double videoStartTs*/)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new UserProfilePhotoChangedEvent(requestInfo,
            _state.UserId,
            photoId,
            fallback
            //videoEmojiMarkup
            /*, hasVideo, videoStartTs*/));
    }


    public void UpdateUserName(RequestInfo requestInfo,
        string userName)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new UserNameUpdatedEvent(requestInfo,
            new UserItem(_state.UserId,
                _state.AccessHash,
                _state.PhoneNumber,
                _state.FirstName,
                _state.LastName,
                userName),
            _state.UserName));
    }
}
