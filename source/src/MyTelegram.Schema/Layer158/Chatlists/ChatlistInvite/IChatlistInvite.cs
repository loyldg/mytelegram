// ReSharper disable All

namespace MyTelegram.Schema.Chatlists;

public interface IChatlistInvite : IObject
{
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
