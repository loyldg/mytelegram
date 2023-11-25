using EventFlow.ReadStores;

namespace MyTelegram.Messenger.TLObjectConverters.Impl.Layer166;

public class PeerNotifySettingsConverterLayer166 : IPeerNotifySettingsConverterLayer166
{
    private readonly IObjectMapper _objectMapper;

    public PeerNotifySettingsConverterLayer166(IObjectMapper objectMapper)
    {
        _objectMapper = objectMapper;
    }

    public int RequestLayer { get; set; }
    public virtual int Layer => Layers.Layer166;
    public IPeerNotifySettings ToPeerNotifySettings(IPeerNotifySettingsReadModel? readModel)
    {
        return ToPeerNotifySettings(readModel?.NotifySettings);
    }

    public virtual IPeerNotifySettings ToPeerNotifySettings(PeerNotifySettings? peerNotifySettings)
    {
        var settings =
            _objectMapper.Map<PeerNotifySettings, TPeerNotifySettings>(peerNotifySettings ??
                                                                       PeerNotifySettings.DefaultSettings);
        settings.IosSound = new TNotificationSoundDefault();
        settings.AndroidSound = new TNotificationSoundLocal
        {
            Title = "default",
            Data = "default"
        };
        settings.OtherSound = new TNotificationSoundLocal
        {
            Title = "default",
            Data = "default"
        };

        return settings;
    }
}

