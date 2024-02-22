using IPeerSettings = MyTelegram.Schema.IPeerSettings;
using TPeerSettings = MyTelegram.Schema.TPeerSettings;

namespace MyTelegram.Messenger.TLObjectConverters.Impl.LatestLayer;

public class PeerSettingsConverterLatest : IPeerSettingsConverterLatest
{
    private readonly IObjectMapper _objectMapper;

    public PeerSettingsConverterLatest(IObjectMapper objectMapper)
    {
        _objectMapper = objectMapper;
    }

    public int Layer => Layers.LayerLatest;
    public int RequestLayer { get; set; }
    public virtual IPeerSettings ToPeerSettings(IPeerSettingsReadModel? readModel, ContactType? contactType)
    {
        var isContact = contactType == ContactType.Mutual;

        var settings = new TPeerSettings
        {
            ShareContact = contactType == ContactType.Unilateral
        };

        if (readModel == null)
        {
            settings.BlockContact = !isContact;
            settings.AddContact = !isContact;

            return settings;
        }

        if (readModel.PeerSettings != null)
        {
            settings = _objectMapper.Map<PeerSettings, TPeerSettings>(readModel.PeerSettings);

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