namespace MyTelegram.Domain.CommandHandlers.User;

public class CheckUserStateCommandHandler : CommandHandler<UserAggregate, UserId, CheckUserStateCommand>
{
    public override Task ExecuteAsync(UserAggregate aggregate,
        CheckUserStateCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.CheckUserState(command.CorrelationId);
        return Task.CompletedTask;
    }
}