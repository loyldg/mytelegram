// ReSharper disable All

namespace MyTelegram.Schema.Chatlists;

public interface IChatlistUpdates : IObject
{
    TVector<Schema.IPeer> MissingPeers { get; set; }
    TVector<Schema.IChat> Chats { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
