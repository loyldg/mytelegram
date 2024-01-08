using IPeerSettings = MyTelegram.Schema.IPeerSettings;
using TPeerSettings = MyTelegram.Schema.TPeerSettings;

namespace MyTelegram.Messenger.TLObjectConverters.Impl.LatestLayer;

public class PeerSettingsConverterLatest : IPeerSettingsConverterLatest
{
    public int Layer => Layers.LayerLatest;
    public int RequestLayer { get; set; }
    public virtual IPeerSettings ToPeerSettings(IPeerSettingsReadModel? readModel, bool isContact = false)
    {
        var settings = new TPeerSettings();
        if (readModel == null)
        {
            //settings.BlockContact = !isContact;
            //settings.AddContact = !isContact;

            settings.BlockContact = false;
            settings.AddContact = false;

            return settings;
        }


        //if (readModel.PeerSettings != null)
        //{
        //    settings = _objectMapper.Map<PeerSettings, TPeerSettings>(readModel.PeerSettings);

        //    if (!readModel.HiddenPeerSettingsBar)
        //    {
        //        settings.BlockContact = !isContact;
        //        settings.AddContact = !isContact;
        //    }
        //}
        //else
        //{
        //    if (readModel.HiddenPeerSettingsBar)
        //    {
        //        settings.BlockContact = false;
        //        settings.AddContact = false;
        //    }
        //    else
        //    {
        //        settings.BlockContact = !isContact;
        //        settings.AddContact = !isContact;
        //    }
        //}

        return settings;
    }
}