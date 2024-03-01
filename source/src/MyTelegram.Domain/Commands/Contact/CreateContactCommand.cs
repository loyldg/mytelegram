namespace MyTelegram.Domain.Commands.Contact;

public class CreateContactCommand : Command<ContactAggregate, ContactId, IExecutionResult>
{
    public long SelfUserId { get; }
    public long TargetUserId { get; }
    public string Phone { get; }
    public string FirstName { get; }
    public string? LastName { get; }
    public bool AddPhonePrivacyException { get; }

    public CreateContactCommand(ContactId aggregateId,
        long selfUserId,
        long targetUserId,
        string phone,
        string firstName,
        string? lastName,
        bool addPhonePrivacyException
        ) : base(aggregateId)
    {
        SelfUserId = selfUserId;
        TargetUserId = targetUserId;
        Phone = phone;
        FirstName = firstName;
        LastName = lastName;
        AddPhonePrivacyException = addPhonePrivacyException;
    }
}