// ReSharper disable All

namespace MyTelegram.Schema.Contacts;

public interface IBlocked : IObject
{
    TVector<MyTelegram.Schema.IPeerBlocked> Blocked { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
