namespace MyTelegram.Domain.CommandHandlers.Contact;

public class AddContactCommandHandler : CommandHandler<ContactAggregate, ContactId, AddContactCommand>
{
    public override Task ExecuteAsync(ContactAggregate aggregate,
        AddContactCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.AddContact(command.RequestInfo,
            command.SelfUserId,
            command.TargetUserId,
            command.Phone,
            command.FirstName,
            command.LastName,
            command.AddPhonePrivacyException);
        return Task.CompletedTask;
    }
}