using TPeerNotifySettings = MyTelegram.Schema.TPeerNotifySettings;

namespace MyTelegram.Messenger.TLObjectConverters.Mappers.PeerNotifySetting;

public class PeerNotifySettingMapperLayer164 : ILayeredMapper,
    IObjectMapper<PeerNotifySettings, TPeerNotifySettings>
{
    public TPeerNotifySettings? Map(PeerNotifySettings source)
    {
        return Map(source, new TPeerNotifySettings());
    }

    public TPeerNotifySettings? Map(PeerNotifySettings source, TPeerNotifySettings destination)
    {
        destination.Silent = source.Silent;
        destination.MuteUntil = source.MuteUntil;
        //destination.Sound = source.Sound;
        destination.ShowPreviews = source.ShowPreviews;

        return destination;
    }
}