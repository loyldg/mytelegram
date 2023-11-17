// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/Boost" />
///</summary>
public interface IBoost : IObject
{
    BitArray Flags { get; set; }
    bool Gift { get; set; }
    bool Giveaway { get; set; }
    bool Unclaimed { get; set; }
    string Id { get; set; }
    long? UserId { get; set; }
    int? GiveawayMsgId { get; set; }
    int Date { get; set; }
    int Expires { get; set; }
    string? UsedGiftSlug { get; set; }
    int? Multiplier { get; set; }
}
