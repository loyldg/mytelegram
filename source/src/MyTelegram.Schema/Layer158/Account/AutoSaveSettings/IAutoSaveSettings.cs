// ReSharper disable All

namespace MyTelegram.Schema.Account;

public interface IAutoSaveSettings : IObject
{
    Schema.IAutoSaveSettings UsersSettings { get; set; }
    Schema.IAutoSaveSettings ChatsSettings { get; set; }
    Schema.IAutoSaveSettings BroadcastsSettings { get; set; }
    TVector<Schema.IAutoSaveException> Exceptions { get; set; }
    TVector<Schema.IChat> Chats { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
