// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Secure value error
/// See <a href="https://corefork.telegram.org/constructor/SecureValueError" />
///</summary>
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
