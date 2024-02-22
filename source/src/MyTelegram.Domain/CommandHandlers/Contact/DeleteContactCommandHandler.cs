namespace MyTelegram.Domain.CommandHandlers.Contact;

public class DeleteContactCommandHandler : CommandHandler<ContactAggregate, ContactId, DeleteContactCommand>
{
    public override Task ExecuteAsync(ContactAggregate aggregate,
        DeleteContactCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.DeleteContact(command.RequestInfo, command.TargetUserId);
        return Task.CompletedTask;
    }
}
