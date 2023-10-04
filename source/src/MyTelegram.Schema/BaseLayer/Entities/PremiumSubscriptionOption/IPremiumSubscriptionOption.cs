// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Telegram Premium subscription option
/// See <a href="https://corefork.telegram.org/constructor/PremiumSubscriptionOption" />
///</summary>
public interface IPremiumSubscriptionOption : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether this subscription option is currently in use.
    ///</summary>
    bool Current { get; set; }

    ///<summary>
    /// Whether this subscription option can be used to upgrade the existing Telegram Premium subscription. When upgrading Telegram Premium subscriptions bought through stores, make sure that the store transaction ID is equal to <code>transaction</code>, to avoid upgrading someone else's account, if the client is currently logged into multiple accounts.
    ///</summary>
    bool CanPurchaseUpgrade { get; set; }

    ///<summary>
    /// Identifier of the last in-store transaction for the currently used subscription on the current account.
    ///</summary>
    string? Transaction { get; set; }

    ///<summary>
    /// Duration of subscription in months
    ///</summary>
    int Months { get; set; }

    ///<summary>
    /// Three-letter ISO 4217 <a href="https://corefork.telegram.org/bots/payments#supported-currencies">currency</a> code
    ///</summary>
    string Currency { get; set; }

    ///<summary>
    /// Total price in the smallest units of the currency (integer, not float/double). For example, for a price of <code>US$ 1.45</code> pass <code>amount = 145</code>. See the exp parameter in <a href="https://corefork.telegram.org/bots/payments/currencies.json">currencies.json</a>, it shows the number of digits past the decimal point for each currency (2 for the majority of currencies).
    ///</summary>
    long Amount { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/links">Deep link</a> used to initiate payment
    ///</summary>
    string BotUrl { get; set; }

    ///<summary>
    /// Store product ID, only for official apps
    ///</summary>
    string? StoreProduct { get; set; }
}
