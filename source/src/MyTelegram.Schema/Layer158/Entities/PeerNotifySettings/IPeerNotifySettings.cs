// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPeerNotifySettings : IObject
{
    BitArray Flags { get; set; }
    bool? ShowPreviews { get; set; }
    bool? Silent { get; set; }
    int? MuteUntil { get; set; }
    MyTelegram.Schema.INotificationSound? IosSound { get; set; }
    MyTelegram.Schema.INotificationSound? AndroidSound { get; set; }
    MyTelegram.Schema.INotificationSound? OtherSound { get; set; }
}
