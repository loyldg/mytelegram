// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// <a href="https://corefork.telegram.org/api/giveaways">Giveaway</a> option.
/// See <a href="https://corefork.telegram.org/constructor/PremiumGiftCodeOption" />
///</summary>
[JsonDerivedType(typeof(TPremiumGiftCodeOption), nameof(TPremiumGiftCodeOption))]
public interface IPremiumGiftCodeOption : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Number of users which will be able to activate the gift codes.
    ///</summary>
    int Users { get; set; }

    ///<summary>
    /// Duration in months of each gifted <a href="https://corefork.telegram.org/api/premium">Telegram Premium</a> subscription.
    ///</summary>
    int Months { get; set; }

    ///<summary>
    /// Identifier of the store product associated with the option, official apps only.
    ///</summary>
    string? StoreProduct { get; set; }

    ///<summary>
    /// Number of times the store product must be paid
    ///</summary>
    int? StoreQuantity { get; set; }

    ///<summary>
    /// Three-letter ISO 4217 <a href="https://corefork.telegram.org/bots/payments#supported-currencies">currency</a> code
    ///</summary>
    string Currency { get; set; }

    ///<summary>
    /// Total price in the smallest units of the currency (integer, not float/double). For example, for a price of <code>US$ 1.45</code> pass <code>amount = 145</code>. See the exp parameter in <a href="https://corefork.telegram.org/bots/payments/currencies.json">currencies.json</a>, it shows the number of digits past the decimal point for each currency (2 for the majority of currencies).
    ///</summary>
    long Amount { get; set; }
}
