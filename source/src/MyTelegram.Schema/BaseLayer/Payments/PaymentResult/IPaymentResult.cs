// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// Payment result
/// See <a href="https://corefork.telegram.org/constructor/payments.PaymentResult" />
///</summary>
[JsonDerivedType(typeof(TPaymentResult), nameof(TPaymentResult))]
[JsonDerivedType(typeof(TPaymentVerificationNeeded), nameof(TPaymentVerificationNeeded))]
public interface IPaymentResult : IObject
{

}
