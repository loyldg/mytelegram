namespace MyTelegram.Domain.Events.Contact;

public class SingleContactImportedEvent : RequestAggregateEvent2<ImportedContactAggregate, ImportedContactId>
{
    public SingleContactImportedEvent(
        RequestInfo requestInfo,
        long selfUserId,
        PhoneContact phoneContact) : base(requestInfo)
    {
        SelfUserId = selfUserId;
        PhoneContact = phoneContact;

    }

    public PhoneContact PhoneContact { get; }

    public long SelfUserId { get; }

}
