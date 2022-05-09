namespace MyTelegram.Domain.CommandHandlers.User;

public class SetVerifiedCommandHandler : CommandHandler<UserAggregate, UserId, SetVerifiedCommand>
{
    public override Task ExecuteAsync(UserAggregate aggregate,
        SetVerifiedCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.SetVerified(command.Verified);
        return Task.CompletedTask;
    }
}
