namespace MyTelegram.Domain.CommandHandlers.User;

public class CheckUserStatusCommandHandler : CommandHandler<UserAggregate, UserId, CheckUserStatusCommand>
{
    public override Task ExecuteAsync(UserAggregate aggregate,
        CheckUserStatusCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.CheckUserStatus(command.RequestInfo);
        return Task.CompletedTask;
    }
}