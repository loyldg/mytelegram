namespace MyTelegram.Messenger.Services.Interfaces;

public interface IPrivacyHelper
{
    void ApplyPrivacy(IPrivacyReadModel? privacyReadModel,
        Action executeOnPrivacyNotMatch,
        long selfUserId,
        bool isContact);
}