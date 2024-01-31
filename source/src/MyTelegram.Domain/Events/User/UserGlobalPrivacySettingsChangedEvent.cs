namespace MyTelegram.Domain.Events.User;

public class UserGlobalPrivacySettingsChangedEvent : RequestAggregateEvent2<UserAggregate, UserId>
{
    public GlobalPrivacySettings GlobalPrivacySettings { get; }

    public UserGlobalPrivacySettingsChangedEvent(RequestInfo requestInfo,GlobalPrivacySettings globalPrivacySettings) : base(requestInfo)
    {
        GlobalPrivacySettings = globalPrivacySettings;
    }
}