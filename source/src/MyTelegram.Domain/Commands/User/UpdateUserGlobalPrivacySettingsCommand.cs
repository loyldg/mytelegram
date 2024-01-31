namespace MyTelegram.Domain.Commands.User;

public class UpdateUserGlobalPrivacySettingsCommand : RequestCommand2<UserAggregate, UserId, IExecutionResult>
{
    public GlobalPrivacySettings GlobalPrivacySettings { get; }

    public UpdateUserGlobalPrivacySettingsCommand(UserId aggregateId, RequestInfo requestInfo, GlobalPrivacySettings globalPrivacySettings) : base(aggregateId, requestInfo)
    {
        GlobalPrivacySettings = globalPrivacySettings;
    }
}