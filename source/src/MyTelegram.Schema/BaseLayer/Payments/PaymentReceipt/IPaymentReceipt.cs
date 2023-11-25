// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// Payment receipt
/// See <a href="https://corefork.telegram.org/constructor/payments.PaymentReceipt" />
///</summary>
[JsonDerivedType(typeof(TPaymentReceipt), nameof(TPaymentReceipt))]
public interface IPaymentReceipt : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Date of generation
    ///</summary>
    int Date { get; set; }

    ///<summary>
    /// Bot ID
    ///</summary>
    long BotId { get; set; }

    ///<summary>
    /// Provider ID
    ///</summary>
    long ProviderId { get; set; }

    ///<summary>
    /// Title
    ///</summary>
    string Title { get; set; }

    ///<summary>
    /// Description
    ///</summary>
    string Description { get; set; }

    ///<summary>
    /// Photo
    /// See <a href="https://corefork.telegram.org/type/WebDocument" />
    ///</summary>
    MyTelegram.Schema.IWebDocument? Photo { get; set; }

    ///<summary>
    /// Invoice
    /// See <a href="https://corefork.telegram.org/type/Invoice" />
    ///</summary>
    MyTelegram.Schema.IInvoice Invoice { get; set; }

    ///<summary>
    /// Info
    /// See <a href="https://corefork.telegram.org/type/PaymentRequestedInfo" />
    ///</summary>
    MyTelegram.Schema.IPaymentRequestedInfo? Info { get; set; }

    ///<summary>
    /// Selected shipping option
    /// See <a href="https://corefork.telegram.org/type/ShippingOption" />
    ///</summary>
    MyTelegram.Schema.IShippingOption? Shipping { get; set; }

    ///<summary>
    /// Tipped amount
    ///</summary>
    long? TipAmount { get; set; }

    ///<summary>
    /// Three-letter ISO 4217 <a href="https://corefork.telegram.org/bots/payments#supported-currencies">currency</a> code
    ///</summary>
    string Currency { get; set; }

    ///<summary>
    /// Total amount in the smallest units of the currency (integer, not float/double). For example, for a price of <code>US$ 1.45</code> pass <code>amount = 145</code>. See the exp parameter in <a href="https://corefork.telegram.org/bots/payments/currencies.json">currencies.json</a>, it shows the number of digits past the decimal point for each currency (2 for the majority of currencies).
    ///</summary>
    long TotalAmount { get; set; }

    ///<summary>
    /// Payment credential name
    ///</summary>
    string CredentialsTitle { get; set; }

    ///<summary>
    /// Users
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
