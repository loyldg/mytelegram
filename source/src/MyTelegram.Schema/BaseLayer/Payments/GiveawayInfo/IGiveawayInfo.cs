// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// Info about a <a href="https://corefork.telegram.org/api/giveaways">Telegram Premium Giveaway</a>.
/// See <a href="https://corefork.telegram.org/constructor/payments.GiveawayInfo" />
///</summary>
[JsonDerivedType(typeof(TGiveawayInfo), nameof(TGiveawayInfo))]
[JsonDerivedType(typeof(TGiveawayInfoResults), nameof(TGiveawayInfoResults))]
public interface IGiveawayInfo : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Start date of the giveaway
    ///</summary>
    int StartDate { get; set; }
}
