// ReSharper disable All

namespace MyTelegram.Schema.Users;

public interface IUserFull : IObject
{
    MyTelegram.Schema.IUserFull FullUser { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }

}
