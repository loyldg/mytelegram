// ReSharper disable All

namespace MyTelegram.Schema.Auth;

///<summary>
/// Recovery info of a <a href="https://corefork.telegram.org/api/srp">2FA password</a>, only for accounts with a <a href="https://corefork.telegram.org/api/srp#email-verification">recovery email configured</a>.
/// See <a href="https://corefork.telegram.org/constructor/auth.PasswordRecovery" />
///</summary>
[JsonDerivedType(typeof(TPasswordRecovery), nameof(TPasswordRecovery))]
public interface IPasswordRecovery : IObject
{
    ///<summary>
    /// The email to which the recovery code was sent must match this <a href="https://corefork.telegram.org/api/pattern">pattern</a>.
    ///</summary>
    string EmailPattern { get; set; }
}
