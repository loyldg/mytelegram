namespace MyTelegram.Domain.Commands.Contact;

public class ImportSingleContactCommand : Command<ImportedContactAggregate, ImportedContactId, IExecutionResult>
{
    public ImportSingleContactCommand(ImportedContactId aggregateId,
        RequestInfo requestInfo,
        long selfUserId,
        PhoneContact phoneContact) : base(aggregateId)
    {
        RequestInfo = requestInfo;
        SelfUserId = selfUserId;
        PhoneContact = phoneContact;
    }

    public PhoneContact PhoneContact { get; }
    public RequestInfo RequestInfo { get; }
    public long SelfUserId { get; }
}
