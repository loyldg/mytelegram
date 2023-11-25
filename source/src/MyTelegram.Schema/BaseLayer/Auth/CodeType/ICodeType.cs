// ReSharper disable All

namespace MyTelegram.Schema.Auth;

///<summary>
/// Type of verification code that will be sent next if you call the resendCode method
/// See <a href="https://corefork.telegram.org/constructor/auth.CodeType" />
///</summary>
[JsonDerivedType(typeof(TCodeTypeSms), nameof(TCodeTypeSms))]
[JsonDerivedType(typeof(TCodeTypeCall), nameof(TCodeTypeCall))]
[JsonDerivedType(typeof(TCodeTypeFlashCall), nameof(TCodeTypeFlashCall))]
[JsonDerivedType(typeof(TCodeTypeMissedCall), nameof(TCodeTypeMissedCall))]
[JsonDerivedType(typeof(TCodeTypeFragmentSms), nameof(TCodeTypeFragmentSms))]
public interface ICodeType : IObject
{

}
