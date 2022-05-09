// ReSharper disable All

namespace MyTelegram.Schema.Contacts;

public interface IFound : IObject
{
    TVector<MyTelegram.Schema.IPeer> MyResults { get; set; }
    TVector<MyTelegram.Schema.IPeer> Results { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }

}
