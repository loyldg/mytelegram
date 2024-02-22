#pragma warning disable CS8618

namespace MyTelegram.Domain.Aggregates.Contact;

public class ContactState : AggregateState<ContactAggregate, ContactId, ContactState>,
        IApply<ContactAddedEvent>,
        IApply<ContactDeletedEvent>,
        IApply<ContactProfilePhotoChangedEvent>

{
    public string FirstName { get; private set; }

    public string? LastName { get; private set; }
    public string Phone { get; private set; }
    public long SelfUserId { get; private set; }

    public long TargetUserId { get; private set; }
    //public bool AddPhonePrivacyException { get; private set;}
    public long? PhotoId { get; private set; }
    public void Apply(ContactAddedEvent aggregateEvent)
    {
    }

    public void Apply(ContactDeletedEvent aggregateEvent)
    {
    }

    public void LoadSnapshot(ContactSnapshot snapshot)
    {
        SelfUserId = snapshot.SelfUserId;
        TargetUserId = snapshot.TargetUserId;
        Phone = snapshot.Phone;
        FirstName = snapshot.FirstName;
        LastName = snapshot.LastName;
        PhotoId = snapshot.PhotoId;
        //AddPhonePrivacyException = snapshot.AddPhonePrivacyException;
    }

    public void Apply(ContactProfilePhotoChangedEvent aggregateEvent)
    {
        PhotoId = aggregateEvent.PhotoId;
    }
}
