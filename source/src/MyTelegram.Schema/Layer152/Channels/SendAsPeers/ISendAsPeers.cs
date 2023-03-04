// ReSharper disable All

namespace MyTelegram.Schema.Channels;

public interface ISendAsPeers : IObject
{
    TVector<MyTelegram.Schema.ISendAsPeer> Peers { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
