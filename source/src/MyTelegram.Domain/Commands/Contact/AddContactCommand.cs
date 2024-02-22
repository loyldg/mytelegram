namespace MyTelegram.Domain.Commands.Contact;

public class AddContactCommand : RequestCommand2<ContactAggregate, ContactId, IExecutionResult>
{
    public AddContactCommand(ContactId aggregateId,
        RequestInfo requestInfo,
        long selfUserId,
        long targetUserId,
        string phone,
        string firstName,
        string? lastName,
        bool addPhonePrivacyException
    ) : base(aggregateId, requestInfo)
    {
        SelfUserId = selfUserId;
        TargetUserId = targetUserId;
        Phone = phone;
        FirstName = firstName;
        LastName = lastName;
        AddPhonePrivacyException = addPhonePrivacyException;
    }

    public bool AddPhonePrivacyException { get; }
    public string FirstName { get; }
    public string? LastName { get; }
    public string Phone { get; }
    public long SelfUserId { get; }
    public long TargetUserId { get; }
}