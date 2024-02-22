namespace MyTelegram.Domain.CommandHandlers.Contact;

public class
    ImportContactsCommandHandler : CommandHandler<ImportedContactAggregate, ImportedContactId, ImportContactsCommand>
{
    public override Task ExecuteAsync(ImportedContactAggregate aggregate,
        ImportContactsCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.ImportContacts(command.RequestInfo, command.SelfUserId, command.PhoneContacts);
        return Task.CompletedTask;
    }
}
