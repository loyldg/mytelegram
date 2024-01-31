namespace MyTelegram.Domain.CommandHandlers.User;

public class
    UpdateUserGlobalPrivacySettingsCommandHandler : CommandHandler<UserAggregate, UserId,
        UpdateUserGlobalPrivacySettingsCommand>
{
    public override Task ExecuteAsync(UserAggregate aggregate, UpdateUserGlobalPrivacySettingsCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.UpdateGlobalPrivacySettings(command.RequestInfo, command.GlobalPrivacySettings);
        return Task.CompletedTask;
    }
}