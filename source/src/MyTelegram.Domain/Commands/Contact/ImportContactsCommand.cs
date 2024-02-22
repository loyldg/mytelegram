namespace MyTelegram.Domain.Commands.Contact;

public class ImportContactsCommand : RequestCommand2<ImportedContactAggregate, ImportedContactId, IExecutionResult>
{
    public ImportContactsCommand(ImportedContactId aggregateId,
        RequestInfo requestInfo,
        long selfUserId,
        IReadOnlyCollection<PhoneContact> phoneContacts) : base(aggregateId, requestInfo)
    {
        SelfUserId = selfUserId;
        PhoneContacts = phoneContacts;
    }

    public IReadOnlyCollection<PhoneContact> PhoneContacts { get; }
    public long SelfUserId { get; }
}
