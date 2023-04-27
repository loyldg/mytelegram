// ReSharper disable All

namespace MyTelegram.Schema.Chatlists;

public interface IChatlistUpdates : IObject
{
    TVector<MyTelegram.Schema.IPeer> MissingPeers { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
