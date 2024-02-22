namespace MyTelegram.Domain.Aggregates.Contact;

public class
    ImportedContactAggregate : MySnapshotAggregateRoot<ImportedContactAggregate, ImportedContactId,
        ImportedContactSnapshot>
{
    private readonly ImportedContactState _state = new();

    public ImportedContactAggregate(ImportedContactId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void ImportContacts(RequestInfo requestInfo,
        long selfUserId,
        IReadOnlyCollection<PhoneContact> contacts)
    {
        Emit(new ContactsImportedEvent(requestInfo, selfUserId, contacts));
    }

    public void ImportSingleContact(RequestInfo requestInfo, long selfUserId,
        PhoneContact phoneContact)
    {
        Emit(new SingleContactImportedEvent(requestInfo, selfUserId, phoneContact));
    }

    protected override Task<ImportedContactSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new ImportedContactSnapshot(_state.SelfUserId,
            _state.TargetUserId,
            _state.Phone,
            _state.FirstName,
            _state.LastName));
    }

    protected override Task LoadSnapshotAsync(ImportedContactSnapshot snapshot,
        ISnapshotMetadata metadata,
        CancellationToken cancellationToken)
    {
        _state.LoadSnapshot(snapshot);
        return Task.CompletedTask;
    }
}
