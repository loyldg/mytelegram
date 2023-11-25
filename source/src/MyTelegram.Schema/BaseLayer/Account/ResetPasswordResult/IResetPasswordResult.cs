// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// Result of an <a href="https://corefork.telegram.org/method/account.resetPassword">account.resetPassword</a> request.
/// See <a href="https://corefork.telegram.org/constructor/account.ResetPasswordResult" />
///</summary>
[JsonDerivedType(typeof(TResetPasswordFailedWait), nameof(TResetPasswordFailedWait))]
[JsonDerivedType(typeof(TResetPasswordRequestedWait), nameof(TResetPasswordRequestedWait))]
[JsonDerivedType(typeof(TResetPasswordOk), nameof(TResetPasswordOk))]
public interface IResetPasswordResult : IObject
{

}
