// ReSharper disable All

namespace MyTelegram.Schema.Auth;

///<summary>
/// Type of the verification code that was sent
/// See <a href="https://corefork.telegram.org/constructor/auth.SentCodeType" />
///</summary>
[JsonDerivedType(typeof(TSentCodeTypeApp), nameof(TSentCodeTypeApp))]
[JsonDerivedType(typeof(TSentCodeTypeSms), nameof(TSentCodeTypeSms))]
[JsonDerivedType(typeof(TSentCodeTypeCall), nameof(TSentCodeTypeCall))]
[JsonDerivedType(typeof(TSentCodeTypeFlashCall), nameof(TSentCodeTypeFlashCall))]
[JsonDerivedType(typeof(TSentCodeTypeMissedCall), nameof(TSentCodeTypeMissedCall))]
[JsonDerivedType(typeof(TSentCodeTypeEmailCode), nameof(TSentCodeTypeEmailCode))]
[JsonDerivedType(typeof(TSentCodeTypeSetUpEmailRequired), nameof(TSentCodeTypeSetUpEmailRequired))]
[JsonDerivedType(typeof(TSentCodeTypeFragmentSms), nameof(TSentCodeTypeFragmentSms))]
[JsonDerivedType(typeof(TSentCodeTypeFirebaseSms), nameof(TSentCodeTypeFirebaseSms))]
public interface ISentCodeType : IObject
{

}
