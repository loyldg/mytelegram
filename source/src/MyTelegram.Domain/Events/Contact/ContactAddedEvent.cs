namespace MyTelegram.Domain.Events.Contact;

public class ContactAddedEvent : RequestAggregateEvent2<ContactAggregate, ContactId>
{
    public ContactAddedEvent(RequestInfo requestInfo,
        long selfUserId,
        long targetUserId,
        string phone,
        string firstName,
        string? lastName,
        bool addPhonePrivacyException) : base(requestInfo)
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