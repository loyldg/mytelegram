// ReSharper disable All

namespace MyTelegram.Schema.Premium;

///<summary>
/// Contains info about the current <a href="https://corefork.telegram.org/api/boost">boost status</a> of a peer.
/// See <a href="https://corefork.telegram.org/constructor/premium.BoostsStatus" />
///</summary>
[JsonDerivedType(typeof(TBoostsStatus), nameof(TBoostsStatus))]
public interface IBoostsStatus : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether we're currently boosting this channel, <code>my_boost_slots</code> will also be set.
    ///</summary>
    bool MyBoost { get; set; }

    ///<summary>
    /// The current boost level of the channel.
    ///</summary>
    int Level { get; set; }

    ///<summary>
    /// The number of boosts acquired so far in the current level.
    ///</summary>
    int CurrentLevelBoosts { get; set; }

    ///<summary>
    /// Total number of boosts acquired so far.
    ///</summary>
    int Boosts { get; set; }

    ///<summary>
    /// The number of boosts acquired from created Telegram Premium <a href="https://corefork.telegram.org/api/giveaways">gift codes</a> and <a href="https://corefork.telegram.org/api/giveaways">giveaways</a>; only returned to channel admins.
    ///</summary>
    int? GiftBoosts { get; set; }

    ///<summary>
    /// Total number of boosts needed to reach the next level; if absent, the next level isn't available.
    ///</summary>
    int? NextLevelBoosts { get; set; }

    ///<summary>
    /// Only returned to channel admins: contains the approximated number of Premium users subscribed to the channel, related to the total number of subscribers.
    /// See <a href="https://corefork.telegram.org/type/StatsPercentValue" />
    ///</summary>
    MyTelegram.Schema.IStatsPercentValue? PremiumAudience { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/links#boost-links">Boost deep link »</a> that can be used to boost the chat.
    ///</summary>
    string BoostUrl { get; set; }

    ///<summary>
    /// A list of prepaid <a href="https://corefork.telegram.org/api/giveaways">giveaways</a> available for the chat; only returned to channel admins.
    /// See <a href="https://corefork.telegram.org/type/PrepaidGiveaway" />
    ///</summary>
    TVector<MyTelegram.Schema.IPrepaidGiveaway>? PrepaidGiveaways { get; set; }

    ///<summary>
    /// Indicates which of our <a href="https://corefork.telegram.org/api/boost">boost slots</a> we've assigned to this peer (populated if <code>my_boost</code> is set).
    ///</summary>
    TVector<int>? MyBoostSlots { get; set; }
}
