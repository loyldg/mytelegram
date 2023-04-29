// ReSharper disable All

namespace MyTelegram.Schema.Users;

public interface IUserFull : IObject
{
    Schema.IUserFull FullUser { get; set; }
    TVector<Schema.IChat> Chats { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
