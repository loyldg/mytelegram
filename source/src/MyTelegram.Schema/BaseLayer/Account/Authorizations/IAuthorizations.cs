// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// Logged-in sessions
/// See <a href="https://corefork.telegram.org/constructor/account.Authorizations" />
///</summary>
public interface IAuthorizations : IObject
{
    ///<summary>
    /// Time-to-live of session
    ///</summary>
    int AuthorizationTtlDays { get; set; }

    ///<summary>
    /// Logged-in sessions
    /// See <a href="https://corefork.telegram.org/type/Authorization" />
    ///</summary>
    TVector<MyTelegram.Schema.IAuthorization> Authorizations { get; set; }
}
