namespace MyTelegram.Domain.CommandHandlers.User;

public class SetSupportCommandCommandHandler : CommandHandler<UserAggregate, UserId, SetSupportCommand>
{
    public override Task ExecuteAsync(UserAggregate aggregate,
        SetSupportCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.SetSupport(command.Support);
        return Task.CompletedTask;
    }
}
