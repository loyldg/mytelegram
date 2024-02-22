namespace MyTelegram.Domain.Commands.Contact;

public class ImportSingleContactCommand : RequestCommand2<ImportedContactAggregate, ImportedContactId, IExecutionResult>
{
    public ImportSingleContactCommand(ImportedContactId aggregateId,
        RequestInfo requestInfo,
        long selfUserId,
        PhoneContact phoneContact) : base(aggregateId, requestInfo)
    {
        SelfUserId = selfUserId;
        PhoneContact = phoneContact;
    }

    public PhoneContact PhoneContact { get; }
    public long SelfUserId { get; }
}
