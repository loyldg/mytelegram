// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Info about one or more <a href="https://corefork.telegram.org/api/boost">boosts</a> applied by a specific user.
/// See <a href="https://corefork.telegram.org/constructor/Boost" />
///</summary>
[JsonDerivedType(typeof(TBoost), nameof(TBoost))]
public interface IBoost : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether this boost was applied because the channel <a href="https://corefork.telegram.org/api/giveaways">directly gifted a subscription to the user</a>.
    ///</summary>
    bool Gift { get; set; }

    ///<summary>
    /// Whether this boost was applied because the user was chosen in a <a href="https://corefork.telegram.org/api/giveaways">giveaway started by the channel</a>.
    ///</summary>
    bool Giveaway { get; set; }

    ///<summary>
    /// If set, the user hasn't yet invoked <a href="https://corefork.telegram.org/method/payments.applyGiftCode">payments.applyGiftCode</a> to claim a subscription gifted <a href="https://corefork.telegram.org/api/giveaways">directly or in a giveaway by the channel</a>.
    ///</summary>
    bool Unclaimed { get; set; }

    ///<summary>
    /// Unique ID for this set of boosts.
    ///</summary>
    string Id { get; set; }

    ///<summary>
    /// ID of the user that applied the boost.
    ///</summary>
    long? UserId { get; set; }

    ///<summary>
    /// The message ID of the <a href="https://corefork.telegram.org/api/giveaways">giveaway</a>
    ///</summary>
    int? GiveawayMsgId { get; set; }

    ///<summary>
    /// When was the boost applied
    ///</summary>
    int Date { get; set; }

    ///<summary>
    /// When does the boost expire
    ///</summary>
    int Expires { get; set; }

    ///<summary>
    /// The created Telegram Premium gift code, only set if either <code>gift</code> or <code>giveaway</code> are set AND it is either a gift code for the currently logged in user or if it was already claimed.
    ///</summary>
    string? UsedGiftSlug { get; set; }

    ///<summary>
    /// If set, this boost counts as <code>multiplier</code> boosts, otherwise it counts as a single boost.
    ///</summary>
    int? Multiplier { get; set; }
}
