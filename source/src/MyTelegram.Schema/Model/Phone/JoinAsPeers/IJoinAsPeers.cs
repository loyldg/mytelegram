// ReSharper disable All

namespace MyTelegram.Schema.Phone;

public interface IJoinAsPeers : IObject
{
    TVector<MyTelegram.Schema.IPeer> Peers { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }

}
