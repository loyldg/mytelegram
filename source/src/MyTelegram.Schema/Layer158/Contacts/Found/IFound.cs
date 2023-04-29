// ReSharper disable All

namespace MyTelegram.Schema.Contacts;

public interface IFound : IObject
{
    TVector<Schema.IPeer> MyResults { get; set; }
    TVector<Schema.IPeer> Results { get; set; }
    TVector<Schema.IChat> Chats { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
