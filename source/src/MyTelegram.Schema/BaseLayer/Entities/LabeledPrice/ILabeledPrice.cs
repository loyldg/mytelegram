// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Labeled pricetag
/// See <a href="https://corefork.telegram.org/constructor/LabeledPrice" />
///</summary>
[JsonDerivedType(typeof(TLabeledPrice), nameof(TLabeledPrice))]
public interface ILabeledPrice : IObject
{
    ///<summary>
    /// Portion label
    ///</summary>
    string Label { get; set; }

    ///<summary>
    /// Price of the product in the smallest units of the currency (integer, not float/double). For example, for a price of <code>US$ 1.45</code> pass <code>amount = 145</code>. See the exp parameter in <a href="https://corefork.telegram.org/bots/payments/currencies.json">currencies.json</a>, it shows the number of digits past the decimal point for each currency (2 for the majority of currencies).
    ///</summary>
    long Amount { get; set; }
}
