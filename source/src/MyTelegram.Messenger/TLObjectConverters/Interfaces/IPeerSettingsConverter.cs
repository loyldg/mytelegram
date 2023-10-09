using IPeerSettings = MyTelegram.Schema.IPeerSettings;

namespace MyTelegram.Messenger.TLObjectConverters.Interfaces;

public interface IPeerSettingsConverter : ILayeredConverter
{
    IPeerSettings ToPeerSettings(IPeerSettingsReadModel? readModel, bool isContact = false);
}