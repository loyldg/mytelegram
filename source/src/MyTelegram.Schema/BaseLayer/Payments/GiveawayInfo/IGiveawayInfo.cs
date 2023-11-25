// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/payments.GiveawayInfo" />
///</summary>
[JsonDerivedType(typeof(TGiveawayInfo), nameof(TGiveawayInfo))]
[JsonDerivedType(typeof(TGiveawayInfoResults), nameof(TGiveawayInfoResults))]
public interface IGiveawayInfo : IObject
{
    BitArray Flags { get; set; }
    int StartDate { get; set; }
}
