// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// Privacy rules
/// See <a href="https://corefork.telegram.org/constructor/account.PrivacyRules" />
///</summary>
[JsonDerivedType(typeof(TPrivacyRules), nameof(TPrivacyRules))]
public interface IPrivacyRules : IObject
{
    ///<summary>
    /// Privacy rules
    /// See <a href="https://corefork.telegram.org/type/PrivacyRule" />
    ///</summary>
    TVector<MyTelegram.Schema.IPrivacyRule> Rules { get; set; }

    ///<summary>
    /// Chats to which the rules apply
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Users to which the rules apply
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
