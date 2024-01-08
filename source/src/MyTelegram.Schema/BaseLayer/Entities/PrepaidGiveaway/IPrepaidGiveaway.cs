// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Contains info about a <a href="https://corefork.telegram.org/api/giveaways">prepaid giveaway »</a>.
/// See <a href="https://corefork.telegram.org/constructor/PrepaidGiveaway" />
///</summary>
[JsonDerivedType(typeof(TPrepaidGiveaway), nameof(TPrepaidGiveaway))]
public interface IPrepaidGiveaway : IObject
{
    ///<summary>
    /// Prepaid giveaway ID.
    ///</summary>
    long Id { get; set; }

    ///<summary>
    /// Duration in months of each gifted <a href="https://corefork.telegram.org/api/premium">Telegram Premium</a> subscription.
    ///</summary>
    int Months { get; set; }

    ///<summary>
    /// Number of given away <a href="https://corefork.telegram.org/api/premium">Telegram Premium</a> subscriptions.
    ///</summary>
    int Quantity { get; set; }

    ///<summary>
    /// Payment date.
    ///</summary>
    int Date { get; set; }
}
