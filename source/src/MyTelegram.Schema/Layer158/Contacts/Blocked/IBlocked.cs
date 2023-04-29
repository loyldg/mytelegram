// ReSharper disable All

namespace MyTelegram.Schema.Contacts;

public interface IBlocked : IObject
{
    TVector<Schema.IPeerBlocked> Blocked { get; set; }
    TVector<Schema.IChat> Chats { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
