namespace MyTelegram.Domain.CommandHandlers.User;

public class CreateUserCommandHandler : CommandHandler<UserAggregate, UserId, CreateUserCommand>
{
    public override Task ExecuteAsync(UserAggregate aggregate,
        CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        //Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        aggregate.Create(command.Request,
            command.UserId,
            command.AccessHash,
            command.PhoneNumber,
            command.FirstName,
            command.LastName,
            command.Bot);
        return Task.CompletedTask;
    }
}
