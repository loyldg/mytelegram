// ReSharper disable All

namespace MyTelegram.Schema.Contacts;

public interface IResolvedPeer : IObject
{
    Schema.IPeer Peer { get; set; }
    TVector<Schema.IChat> Chats { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
