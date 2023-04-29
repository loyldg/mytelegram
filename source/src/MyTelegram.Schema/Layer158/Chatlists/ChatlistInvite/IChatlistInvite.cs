// ReSharper disable All

namespace MyTelegram.Schema.Chatlists;

public interface IChatlistInvite : IObject
{
    TVector<Schema.IChat> Chats { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
