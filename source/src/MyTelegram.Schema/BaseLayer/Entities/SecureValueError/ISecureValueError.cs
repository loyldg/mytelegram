// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Secure value error
/// See <a href="https://corefork.telegram.org/constructor/SecureValueError" />
///</summary>
[JsonDerivedType(typeof(TSecureValueErrorData), nameof(TSecureValueErrorData))]
[JsonDerivedType(typeof(TSecureValueErrorFrontSide), nameof(TSecureValueErrorFrontSide))]
[JsonDerivedType(typeof(TSecureValueErrorReverseSide), nameof(TSecureValueErrorReverseSide))]
[JsonDerivedType(typeof(TSecureValueErrorSelfie), nameof(TSecureValueErrorSelfie))]
[JsonDerivedType(typeof(TSecureValueErrorFile), nameof(TSecureValueErrorFile))]
[JsonDerivedType(typeof(TSecureValueErrorFiles), nameof(TSecureValueErrorFiles))]
[JsonDerivedType(typeof(TSecureValueError), nameof(TSecureValueError))]
[JsonDerivedType(typeof(TSecureValueErrorTranslationFile), nameof(TSecureValueErrorTranslationFile))]
[JsonDerivedType(typeof(TSecureValueErrorTranslationFiles), nameof(TSecureValueErrorTranslationFiles))]
public interface ISecureValueError : IObject
{
    ///<summary>
    /// One of <a href="https://corefork.telegram.org/constructor/secureValueTypePersonalDetails">secureValueTypePersonalDetails</a>, <a href="https://corefork.telegram.org/constructor/secureValueTypePassport">secureValueTypePassport</a>, <a href="https://corefork.telegram.org/constructor/secureValueTypeDriverLicense">secureValueTypeDriverLicense</a>, <a href="https://corefork.telegram.org/constructor/secureValueTypeIdentityCard">secureValueTypeIdentityCard</a>, <a href="https://corefork.telegram.org/constructor/secureValueTypeInternalPassport">secureValueTypeInternalPassport</a>, <a href="https://corefork.telegram.org/constructor/secureValueTypeUtilityBill">secureValueTypeUtilityBill</a>, <a href="https://corefork.telegram.org/constructor/secureValueTypeBankStatement">secureValueTypeBankStatement</a>, <a href="https://corefork.telegram.org/constructor/secureValueTypeRentalAgreement">secureValueTypeRentalAgreement</a>, <a href="https://corefork.telegram.org/constructor/secureValueTypePassportRegistration">secureValueTypePassportRegistration</a>, <a href="https://corefork.telegram.org/constructor/secureValueTypeTemporaryRegistration">secureValueTypeTemporaryRegistration</a>
    /// See <a href="https://corefork.telegram.org/type/SecureValueType" />
    ///</summary>
    MyTelegram.Schema.ISecureValueType Type { get; set; }

    ///<summary>
    /// Error message
    ///</summary>
    string Text { get; set; }
}
