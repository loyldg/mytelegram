namespace MyTelegram.Domain.CommandHandlers.User;

public class UpdateUserNameCommandHandler : CommandHandler<UserAggregate, UserId, UpdateUserNameCommand>
{
    public override Task ExecuteAsync(UserAggregate aggregate,
        UpdateUserNameCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.UpdateUserName(command.RequestInfo, command.UserName);
        return Task.CompletedTask;
    }
}