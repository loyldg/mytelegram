// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// Web authorizations
/// See <a href="https://corefork.telegram.org/constructor/account.WebAuthorizations" />
///</summary>
public interface IWebAuthorizations : IObject
{
    ///<summary>
    /// Web authorization list
    /// See <a href="https://corefork.telegram.org/type/WebAuthorization" />
    ///</summary>
    TVector<MyTelegram.Schema.IWebAuthorization> Authorizations { get; set; }

    ///<summary>
    /// Users
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
