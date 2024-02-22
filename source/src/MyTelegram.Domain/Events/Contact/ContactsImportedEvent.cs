namespace MyTelegram.Domain.Events.Contact;

public class ContactsImportedEvent : RequestAggregateEvent2<ImportedContactAggregate, ImportedContactId>
{
    public ContactsImportedEvent(RequestInfo requestInfo,
        long selfUserId,
        IReadOnlyCollection<PhoneContact> phoneContacts) : base(requestInfo)
    {
        SelfUserId = selfUserId;
        PhoneContacts = phoneContacts;

    }

    public IReadOnlyCollection<PhoneContact> PhoneContacts { get; }

    public long SelfUserId { get; }

}
