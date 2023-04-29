// ReSharper disable All

namespace MyTelegram.Schema;

public interface IAttachMenuBot : IObject
{
    BitArray Flags { get; set; }
    bool Inactive { get; set; }
    bool HasSettings { get; set; }
    bool RequestWriteAccess { get; set; }
    long BotId { get; set; }
    string ShortName { get; set; }
    TVector<Schema.IAttachMenuPeerType> PeerTypes { get; set; }
    TVector<Schema.IAttachMenuBotIcon> Icons { get; set; }
}
