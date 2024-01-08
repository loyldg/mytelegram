namespace MyTelegram.Messenger.TLObjectConverters.Impl.LatestLayer;

public class PeerNotifySettingsConverterLatest : IPeerNotifySettingsConverterLatest
{
    private readonly IObjectMapper _objectMapper;

    public PeerNotifySettingsConverterLatest(IObjectMapper objectMapper)
    {
        _objectMapper = objectMapper;
    }

    public virtual int Layer => Layers.LayerLatest;
    public int RequestLayer { get; set; }
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