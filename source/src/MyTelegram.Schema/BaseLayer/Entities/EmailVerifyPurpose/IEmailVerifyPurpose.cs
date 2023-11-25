// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Email verification purpose
/// See <a href="https://corefork.telegram.org/constructor/EmailVerifyPurpose" />
///</summary>
[JsonDerivedType(typeof(TEmailVerifyPurposeLoginSetup), nameof(TEmailVerifyPurposeLoginSetup))]
[JsonDerivedType(typeof(TEmailVerifyPurposeLoginChange), nameof(TEmailVerifyPurposeLoginChange))]
[JsonDerivedType(typeof(TEmailVerifyPurposePassport), nameof(TEmailVerifyPurposePassport))]
public interface IEmailVerifyPurpose : IObject
{

}
