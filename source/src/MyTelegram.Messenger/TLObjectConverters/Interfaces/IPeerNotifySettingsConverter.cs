namespace MyTelegram.Messenger.TLObjectConverters.Interfaces;

public interface IPeerNotifySettingsConverter : ILayeredConverter
{
    IPeerNotifySettings ToPeerNotifySettings(IPeerNotifySettingsReadModel? readModel);
    IPeerNotifySettings ToPeerNotifySettings(PeerNotifySettings? peerNotifySettings);
}