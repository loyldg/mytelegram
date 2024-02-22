namespace MyTelegram.Domain.CommandHandlers.Contact;

public class
    ImportSingleContactCommandHandler : CommandHandler<ImportedContactAggregate, ImportedContactId,
        ImportSingleContactCommand>
{
    public override Task ExecuteAsync(ImportedContactAggregate aggregate,
        ImportSingleContactCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.ImportSingleContact(command.RequestInfo, command.SelfUserId, command.PhoneContact);
        return Task.CompletedTask;
    }
}
