// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Secure value type
/// See <a href="https://corefork.telegram.org/constructor/SecureValueType" />
///</summary>
[JsonDerivedType(typeof(TSecureValueTypePersonalDetails), nameof(TSecureValueTypePersonalDetails))]
[JsonDerivedType(typeof(TSecureValueTypePassport), nameof(TSecureValueTypePassport))]
[JsonDerivedType(typeof(TSecureValueTypeDriverLicense), nameof(TSecureValueTypeDriverLicense))]
[JsonDerivedType(typeof(TSecureValueTypeIdentityCard), nameof(TSecureValueTypeIdentityCard))]
[JsonDerivedType(typeof(TSecureValueTypeInternalPassport), nameof(TSecureValueTypeInternalPassport))]
[JsonDerivedType(typeof(TSecureValueTypeAddress), nameof(TSecureValueTypeAddress))]
[JsonDerivedType(typeof(TSecureValueTypeUtilityBill), nameof(TSecureValueTypeUtilityBill))]
[JsonDerivedType(typeof(TSecureValueTypeBankStatement), nameof(TSecureValueTypeBankStatement))]
[JsonDerivedType(typeof(TSecureValueTypeRentalAgreement), nameof(TSecureValueTypeRentalAgreement))]
[JsonDerivedType(typeof(TSecureValueTypePassportRegistration), nameof(TSecureValueTypePassportRegistration))]
[JsonDerivedType(typeof(TSecureValueTypeTemporaryRegistration), nameof(TSecureValueTypeTemporaryRegistration))]
[JsonDerivedType(typeof(TSecureValueTypePhone), nameof(TSecureValueTypePhone))]
[JsonDerivedType(typeof(TSecureValueTypeEmail), nameof(TSecureValueTypeEmail))]
public interface ISecureValueType : IObject
{

}
