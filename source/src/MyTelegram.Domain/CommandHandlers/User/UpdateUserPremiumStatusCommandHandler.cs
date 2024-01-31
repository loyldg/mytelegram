namespace MyTelegram.Domain.CommandHandlers.User;

public class UpdateUserPremiumStatusCommandHandler : CommandHandler<UserAggregate, UserId, UpdateUserPremiumStatusCommand>
{
    public override Task ExecuteAsync(UserAggregate aggregate, UpdateUserPremiumStatusCommand command, CancellationToken cancellationToken)
    {
        aggregate.UpdateUserPremiumStatus(command.Premium);
        return Task.CompletedTask;
    }
}