namespace MyTelegram.Domain.Aggregates.Contact;

[EnableAutoGeneration]
public class ContactAggregate : SnapshotAggregateRoot<ContactAggregate, ContactId, ContactSnapshot>
{
    private readonly ContactState _state = new();

    public ContactAggregate(ContactId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void UpdateContactProfilePhoto(RequestInfo requestInfo, long selfUserId, long targetUserId, long photoId,
        bool suggest, IPhoto? suggestPhoto)
    {
        Specs.AggregateIsCreated.ThrowFirstDomainErrorIfNotSatisfied(this);
        Emit(new ContactProfilePhotoChangedEvent(requestInfo, selfUserId, targetUserId, photoId, suggest,
            suggestPhoto));
    }

    public void AddContact(RequestInfo requestInfo,
        long selfUserId,
        long targetUserId,
        string phone,
        string firstName,
        string? lastName,
        bool addPhonePrivacyException)
    {
        Emit(new ContactAddedEvent(requestInfo,
            selfUserId,
            targetUserId,
            phone,
            firstName,
            lastName,
            addPhonePrivacyException));
    }

    public void CreateContact(long selfUserId,
        long targetUserId,
        string phone,
        string firstName,
        string? lastName,
        bool addPhonePrivacyException)
    {
        Emit(new ContactCreatedEvent(selfUserId,
            targetUserId,
            phone,
            firstName,
            lastName,
            addPhonePrivacyException));
    }

    public void DeleteContact(RequestInfo requestInfo,
        long targetUserId)
    {
        Specs.AggregateIsCreated.ThrowFirstDomainErrorIfNotSatisfied(this);
        Emit(new ContactDeletedEvent(requestInfo, targetUserId));
    }

    protected override Task<ContactSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new ContactSnapshot(_state.SelfUserId,
            _state.TargetUserId,
            _state.Phone,
            _state.FirstName,
            _state.LastName,
            _state.PhotoId
        ));
    }

    protected override Task LoadSnapshotAsync(ContactSnapshot snapshot,
        ISnapshotMetadata metadata,
        CancellationToken cancellationToken)
    {
        _state.LoadSnapshot(snapshot);
        return Task.CompletedTask;
    }
}