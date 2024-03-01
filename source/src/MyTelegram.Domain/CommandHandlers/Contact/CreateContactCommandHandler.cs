namespace MyTelegram.Domain.CommandHandlers.Contact;

public class CreateContactCommandHandler : CommandHandler<ContactAggregate, ContactId, CreateContactCommand>
{
    public override Task ExecuteAsync(ContactAggregate aggregate,
        CreateContactCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.CreateContact(command.SelfUserId,
            command.TargetUserId,
            command.Phone,
            command.FirstName,
            command.LastName,
            command.AddPhonePrivacyException);
        return Task.CompletedTask;
    }
}