namespace MyTelegram.Domain.Events.Contact;

public class ContactCreatedEvent : AggregateEvent<ContactAggregate, ContactId>
{
    public ContactCreatedEvent(long selfUserId,
        long targetUserId,
        string phone,
        string firstName,
        string? lastName,
        bool addPhonePrivacyException)
    {
        SelfUserId = selfUserId;
        TargetUserId = targetUserId;
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        AddPhonePrivacyException = addPhonePrivacyException;
    }

    public bool AddPhonePrivacyException { get; }
    public string FirstName { get; }
    public string? LastName { get; }
    public string Phone { get; }
    public long SelfUserId { get; }
    public long TargetUserId { get; }
}