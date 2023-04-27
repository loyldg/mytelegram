// ReSharper disable All

namespace MyTelegram.Schema.Contacts;

public interface IResolvedPeer : IObject
{
    MyTelegram.Schema.IPeer Peer { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
