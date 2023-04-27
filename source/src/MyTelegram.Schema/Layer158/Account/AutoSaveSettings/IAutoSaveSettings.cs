// ReSharper disable All

namespace MyTelegram.Schema.Account;

public interface IAutoSaveSettings : IObject
{
    MyTelegram.Schema.IAutoSaveSettings UsersSettings { get; set; }
    MyTelegram.Schema.IAutoSaveSettings ChatsSettings { get; set; }
    MyTelegram.Schema.IAutoSaveSettings BroadcastsSettings { get; set; }
    TVector<MyTelegram.Schema.IAutoSaveException> Exceptions { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
