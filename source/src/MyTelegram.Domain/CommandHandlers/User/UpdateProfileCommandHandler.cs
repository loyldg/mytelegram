namespace MyTelegram.Domain.CommandHandlers.User;

public class UpdateProfileCommandHandler : CommandHandler<UserAggregate, UserId, UpdateProfileCommand>
{
    public override Task ExecuteAsync(UserAggregate aggregate,
        UpdateProfileCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.UpdateProfile(command.RequestInfo, command.FirstName, command.LastName, command.About);
        return Task.CompletedTask;
    }
}
