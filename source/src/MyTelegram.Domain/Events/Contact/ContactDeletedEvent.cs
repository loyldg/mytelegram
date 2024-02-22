namespace MyTelegram.Domain.Events.Contact;

public class ContactDeletedEvent : RequestAggregateEvent2<ContactAggregate, ContactId>
{
    public ContactDeletedEvent(RequestInfo requestInfo,
        long targetUid) : base(requestInfo)
    {
        TargetUid = targetUid;
    }

    public long TargetUid { get; }
}
