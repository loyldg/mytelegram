// ReSharper disable All

namespace MyTelegram.Schema.Channels;

public interface ISendAsPeers : IObject
{
    TVector<Schema.ISendAsPeer> Peers { get; set; }
    TVector<Schema.IChat> Chats { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
