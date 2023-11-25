// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Invoice
/// See <a href="https://corefork.telegram.org/constructor/Invoice" />
///</summary>
[JsonDerivedType(typeof(TInvoice), nameof(TInvoice))]
public interface IInvoice : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Test invoice
    ///</summary>
    bool Test { get; set; }

    ///<summary>
    /// Set this flag if you require the user's full name to complete the order
    ///</summary>
    bool NameRequested { get; set; }

    ///<summary>
    /// Set this flag if you require the user's phone number to complete the order
    ///</summary>
    bool PhoneRequested { get; set; }

    ///<summary>
    /// Set this flag if you require the user's email address to complete the order
    ///</summary>
    bool EmailRequested { get; set; }

    ///<summary>
    /// Set this flag if you require the user's shipping address to complete the order
    ///</summary>
    bool ShippingAddressRequested { get; set; }

    ///<summary>
    /// Set this flag if the final price depends on the shipping method
    ///</summary>
    bool Flexible { get; set; }

    ///<summary>
    /// Set this flag if user's phone number should be sent to provider
    ///</summary>
    bool PhoneToProvider { get; set; }

    ///<summary>
    /// Set this flag if user's email address should be sent to provider
    ///</summary>
    bool EmailToProvider { get; set; }

    ///<summary>
    /// Whether this is a recurring payment
    ///</summary>
    bool Recurring { get; set; }

    ///<summary>
    /// Three-letter ISO 4217 <a href="https://corefork.telegram.org/bots/payments#supported-currencies">currency</a> code
    ///</summary>
    string Currency { get; set; }

    ///<summary>
    /// Price breakdown, a list of components (e.g. product price, tax, discount, delivery cost, delivery tax, bonus, etc.)
    /// See <a href="https://corefork.telegram.org/type/LabeledPrice" />
    ///</summary>
    TVector<MyTelegram.Schema.ILabeledPrice> Prices { get; set; }

    ///<summary>
    /// The maximum accepted amount for tips in the smallest units of the currency (integer, not float/double). For example, for a price of <code>US$ 1.45</code> pass <code>amount = 145</code>. See the exp parameter in <a href="https://corefork.telegram.org/bots/payments/currencies.json">currencies.json</a>, it shows the number of digits past the decimal point for each currency (2 for the majority of currencies).
    ///</summary>
    long? MaxTipAmount { get; set; }

    ///<summary>
    /// A vector of suggested amounts of tips in the <em>smallest units</em> of the currency (integer, not float/double). At most 4 suggested tip amounts can be specified. The suggested tip amounts must be positive, passed in a strictly increased order and must not exceed <code>max_tip_amount</code>.
    ///</summary>
    TVector<long>? SuggestedTipAmounts { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    string? TermsUrl { get; set; }
}
