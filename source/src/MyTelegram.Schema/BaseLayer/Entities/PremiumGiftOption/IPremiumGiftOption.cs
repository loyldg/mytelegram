// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Telegram Premium gift option
/// See <a href="https://corefork.telegram.org/constructor/PremiumGiftOption" />
///</summary>
public interface IPremiumGiftOption : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Duration of gifted Telegram Premium subscription
    ///</summary>
    int Months { get; set; }

    ///<summary>
    /// Three-letter ISO 4217 <a href="https://corefork.telegram.org/bots/payments#supported-currencies">currency</a> code
    ///</summary>
    string Currency { get; set; }

    ///<summary>
    /// Price of the product in the smallest units of the currency (integer, not float/double). For example, for a price of <code>US$ 1.45</code> pass <code>amount = 145</code>. See the exp parameter in <a href="https://corefork.telegram.org/bots/payments/currencies.json">currencies.json</a>, it shows the number of digits past the decimal point for each currency (2 for the majority of currencies).
    ///</summary>
    long Amount { get; set; }

    ///<summary>
    /// An <a href="https://corefork.telegram.org/api/links#invoice-links">invoice deep link »</a> to an invoice for in-app payment, using the official Premium bot; may be empty if direct payment isn't available.
    ///</summary>
    string BotUrl { get; set; }

    ///<summary>
    /// An identifier for the App Store/Play Store product associated with the Premium gift.
    ///</summary>
    string? StoreProduct { get; set; }
}
