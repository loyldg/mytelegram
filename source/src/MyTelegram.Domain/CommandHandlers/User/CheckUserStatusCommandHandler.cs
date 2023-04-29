namespace MyTelegram.Domain.CommandHandlers.User;

public class CheckUserStatusCommandHandler : CommandHandler<UserAggregate, UserId, CheckUserStatusCommand>
{
    public override Task ExecuteAsync(UserAggregate aggregate,
        CheckUserStatusCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.CheckUserStatus(command.CorrelationId);
        return Task.CompletedTask;
    }
}

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
