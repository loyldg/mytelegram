// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Email verification code or token
/// See <a href="https://corefork.telegram.org/constructor/EmailVerification" />
///</summary>
[JsonDerivedType(typeof(TEmailVerificationCode), nameof(TEmailVerificationCode))]
[JsonDerivedType(typeof(TEmailVerificationGoogle), nameof(TEmailVerificationGoogle))]
[JsonDerivedType(typeof(TEmailVerificationApple), nameof(TEmailVerificationApple))]
public interface IEmailVerification : IObject
{

}
