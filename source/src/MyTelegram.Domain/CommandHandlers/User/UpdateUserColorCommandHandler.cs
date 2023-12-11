namespace MyTelegram.Domain.CommandHandlers.User;

public class UpdateUserColorCommandHandler : CommandHandler<UserAggregate, UserId, UpdateUserColorCommand>
{
    public override Task ExecuteAsync(UserAggregate aggregate, UpdateUserColorCommand command, CancellationToken cancellationToken)
    {
        aggregate.UpdateColor(command.RequestInfo,command.Color,command.ForProfile);
        return Task.CompletedTask;
    }
}