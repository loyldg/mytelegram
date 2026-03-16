namespace MyTelegram.Converters.TLObjects.LatestLayer;

internal sealed class PeerSettingsConverter(IObjectMapper objectMapper, IPeerHelper peerHelper)
    : IPeerSettingsConverter, ITransientDependency
{
    public int Layer => Layers.LayerLatest;

    public IPeerSettings ToPeerSettings(long selfUserId, long targetUserId, IPeerSettingsReadModel? readModel,
        ContactType? contactType)
    {
        if (targetUserId == MyTelegramConsts.NotificationServiceUserId || selfUserId == targetUserId ||
            peerHelper.IsBotUser(targetUserId))
        {
            return new TPeerSettings();
        }

        var isContact = contactType == ContactType.Mutual || contactType == ContactType.TargetUserIsMyContact;

        var settings = new TPeerSettings
        {
            ShareContact = contactType == ContactType.TargetUserIsMyContact
        };

        if (readModel == null)
        {
            settings.BlockContact = !isContact;
            settings.AddContact = !isContact;

            return settings;
        }

        if (readModel.PeerSettings != null)
        {
            settings = objectMapper.Map<PeerSettings, TPeerSettings>(readModel.PeerSettings);

            if (!readModel.HiddenPeerSettingsBar)
            {
                settings.BlockContact = !isContact;
                settings.AddContact = !isContact;
            }
        }
        else
        {
            if (readModel.HiddenPeerSettingsBar)
            {
                settings.BlockContact = false;
                settings.AddContact = false;
            }
            else
            {
                settings.BlockContact = !isContact;
                settings.AddContact = !isContact;
            }
        }

        return settings;
    }
}