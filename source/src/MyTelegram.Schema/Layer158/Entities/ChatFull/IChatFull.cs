// ReSharper disable All

namespace MyTelegram.Schema;

public interface IChatFull : IObject
{
    BitArray Flags { get; set; }
    long Id { get; set; }
    string About { get; set; }
    Schema.IPeerNotifySettings NotifySettings { get; set; }
    int? FolderId { get; set; }
}
