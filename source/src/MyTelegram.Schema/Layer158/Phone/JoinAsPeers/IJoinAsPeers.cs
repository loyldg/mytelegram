// ReSharper disable All

namespace MyTelegram.Schema.Phone;

public interface IJoinAsPeers : IObject
{
    TVector<Schema.IPeer> Peers { get; set; }
    TVector<Schema.IChat> Chats { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
