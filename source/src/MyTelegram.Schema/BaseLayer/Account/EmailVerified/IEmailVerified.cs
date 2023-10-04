// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// Email verification status
/// See <a href="https://corefork.telegram.org/constructor/account.EmailVerified" />
///</summary>
public interface IEmailVerified : IObject
{
    ///<summary>
    /// The verified email address.
    ///</summary>
    string Email { get; set; }
}
