// ReSharper disable All

namespace MyTelegram.Schema.Account;

public interface IPrivacyRules : IObject
{
    TVector<MyTelegram.Schema.IPrivacyRule> Rules { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
