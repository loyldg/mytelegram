// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a payment method
/// See <a href="https://corefork.telegram.org/constructor/PaymentFormMethod" />
///</summary>
public interface IPaymentFormMethod : IObject
{
    ///<summary>
    /// URL to open in a webview to process the payment
    ///</summary>
    string Url { get; set; }

    ///<summary>
    /// Payment method description
    ///</summary>
    string Title { get; set; }
}
