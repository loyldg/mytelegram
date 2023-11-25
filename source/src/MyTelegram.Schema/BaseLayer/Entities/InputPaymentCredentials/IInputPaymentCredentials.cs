// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Payment credentials
/// See <a href="https://corefork.telegram.org/constructor/InputPaymentCredentials" />
///</summary>
[JsonDerivedType(typeof(TInputPaymentCredentialsSaved), nameof(TInputPaymentCredentialsSaved))]
[JsonDerivedType(typeof(TInputPaymentCredentials), nameof(TInputPaymentCredentials))]
[JsonDerivedType(typeof(TInputPaymentCredentialsApplePay), nameof(TInputPaymentCredentialsApplePay))]
[JsonDerivedType(typeof(TInputPaymentCredentialsGooglePay), nameof(TInputPaymentCredentialsGooglePay))]
public interface IInputPaymentCredentials : IObject
{

}
