// ReSharper disable All

namespace MyTelegram.Schema.Account;

public interface IPrivacyRules : IObject
{
    TVector<Schema.IPrivacyRule> Rules { get; set; }
    TVector<Schema.IChat> Chats { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
