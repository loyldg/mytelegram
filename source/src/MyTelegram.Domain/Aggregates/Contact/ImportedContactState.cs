namespace MyTelegram.Domain.Aggregates.Contact;

public class ImportedContactState : AggregateState<ImportedContactAggregate, ImportedContactId, ImportedContactState>,
    //IApply<ContactAddedEvent>,
    //IApply<ContactDeletedEvent>,
    IApply<SingleContactImportedEvent>,
    IApply<ContactsImportedEvent>

{
    public string FirstName { get; private set; } = null!;
    public string? LastName { get; private set; }
    public string Phone { get; private set; } = null!;
    public long SelfUserId { get; private set; }
    public long TargetUserId { get; private set; }

    public void Apply(ContactsImportedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }

    public void Apply(SingleContactImportedEvent aggregateEvent)
    {
        SelfUserId = aggregateEvent.SelfUserId;
        TargetUserId = aggregateEvent.PhoneContact.UserId;
        Phone = aggregateEvent.PhoneContact.Phone;
        FirstName = aggregateEvent.PhoneContact.FirstName;
        LastName = aggregateEvent.PhoneContact.LastName;
        //throw new NotImplementedException();
    }
    //public bool AddPhonePrivacyException { get; private set;}

    public void LoadSnapshot(ImportedContactSnapshot snapshot)
    {
        SelfUserId = snapshot.SelfUserId;
        TargetUserId = snapshot.TargetUserId;
        Phone = snapshot.Phone;
        FirstName = snapshot.FirstName;
        LastName = snapshot.LastName;
        //AddPhonePrivacyException = snapshot.AddPhonePrivacyException;
    }
}
