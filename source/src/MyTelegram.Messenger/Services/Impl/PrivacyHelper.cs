namespace MyTelegram.Messenger.Services.Impl;

public class PrivacyHelper : IPrivacyHelper //, ISingletonDependency
{
    private readonly IJsonSerializer _jsonSerializer;

    public PrivacyHelper(IJsonSerializer jsonSerializer)
    {
        _jsonSerializer = jsonSerializer;
    }

    public void ApplyPrivacy(IPrivacyReadModel? privacyReadModel,
        Action executeOnPrivacyNotMatch,
        long selfUserId,
        bool isContact)
    {
        
    }
}