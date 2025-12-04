namespace MyTelegram.Domain.Aggregates.User;

[EnableAutoGeneration]
public class UserAggregate : MyInMemorySnapshotAggregateRoot<UserAggregate, UserId, UserSnapshot>
{
    private readonly UserState _state = new();
    private readonly int AccountDefaultTtl = 365;

    public UserAggregate(UserId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }


    public void UpdateAbout(string? about)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new UserAboutUpdatedEvent(_state.UserId, about));
    }

    public void UpdateFirstName(string firstName)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new UserFirstNameUpdatedEvent(_state.UserId, firstName));
    }

    public void UpdateBirthday(Birthday? birthday)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new BirthdayUpdatedEvent(birthday));
    }
    public void UpdatePersonalChannel(RequestInfo requestInfo, long? personalChannelId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new PersonalChannelUpdatedEvent(_state.UserId, personalChannelId));
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
            _state.HasPassword));
    }

    public void CreateUser(RequestInfo requestInfo,
        long userId,
        long accessHash,
        string phoneNumber,
        string firstName,
        string? lastName = null,
        string? userName = null,
        bool bot = false
    )
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        Specs.IsNotEmptyOrNull.ThrowDomainErrorIfNotSatisfied(firstName);

        var creationTime = DateTime.UtcNow;
        var accountTtl = AccountDefaultTtl;
        Emit(new UserCreatedEvent(requestInfo,
            userId,
            accessHash,
            phoneNumber,
            firstName,
            lastName,
            userName,
            bot,
            accountTtl,
            creationTime
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

    public void UpdateColor(RequestInfo requestInfo, PeerColor? color, bool forProfile)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new UserColorUpdatedEvent(requestInfo, _state.UserId, color, forProfile));
    }

    public void UpdateGlobalPrivacySettings(RequestInfo requestInfo, GlobalPrivacySettings globalPrivacySettings)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new UserGlobalPrivacySettingsChangedEvent(requestInfo, globalPrivacySettings));
    }

    public void UpdateProfile(RequestInfo requestInfo,
        string? firstName,
        string? lastName,
        string? about)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);

        firstName ??= _state.FirstName;
        Emit(new UserProfileUpdatedEvent(requestInfo,
            _state.UserId,
            firstName,
            lastName,
            about));
    }

    public void UpdateProfilePhoto(RequestInfo requestInfo,
            long? photoId,
            bool fallback)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var date = DateTime.UtcNow.ToTimestamp();
        Emit(new UserProfilePhotoChangedEvent(requestInfo,
            _state.UserId,
            photoId,
            fallback,
            _state.IsBot,
            date
            ));
    }

    public void UpdateUserName2(RequestInfo requestInfo,
        string userName)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var userItem = new UserItem(_state.UserId,
                _state.AccessHash,
                _state.PhoneNumber,
                _state.FirstName,
                _state.LastName,
                userName);
        var date = DateTime.UtcNow.ToTimestamp();
        var oldUserName = _state.UserName;
        Emit(new UserNameUpdatedEvent(requestInfo,
            userItem,
            oldUserName,
            date)
            );
    }

    public void UpdateUserPremiumStatus(bool premium)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new UserPremiumStatusChangedEvent(_state.UserId, _state.PhoneNumber, premium));
    }
    public void UploadProfilePhoto(RequestInfo requestInfo,
        long photoId,
        bool fallback,
        IVideoSize? videoEmojiMarkup
        )
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var date = DateTime.UtcNow.ToTimestamp();
        Emit(new UserProfilePhotoUploadedEvent(requestInfo,
            photoId,
            fallback,
            videoEmojiMarkup,
            date
            /*, hasVideo, videoStartTs*/));
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
            _state.ProfileColor,
            _state.GlobalPrivacySettings,
            _state.Premium,
            _state.PersonalChannelId,
            _state.Birthday,
            _state.ProfilePhotoUpdateDate,
            _state.UserNameUpdateDate
        ));
    }

    protected override Task LoadSnapshotAsync(UserSnapshot snapshot,
        ISnapshotMetadata metadata,
        CancellationToken cancellationToken)
    {
        _state.LoadFromSnapshot(snapshot);
        return Task.CompletedTask;
    }

    private void CheckUserDeletionState()
    {
        if (_state.IsDeleted)
        {
            RpcErrors.RpcErrors400.PeerIdInvalid.ThrowRpcError();
        }
    }
}
