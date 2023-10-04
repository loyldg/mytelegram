// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Charged payment
/// See <a href="https://corefork.telegram.org/constructor/PaymentCharge" />
///</summary>
public interface IPaymentCharge : IObject
{
    ///<summary>
    /// Telegram payment identifier
    ///</summary>
    string Id { get; set; }

    ///<summary>
    /// Provider payment identifier
    ///</summary>
    string ProviderChargeId { get; set; }
}
