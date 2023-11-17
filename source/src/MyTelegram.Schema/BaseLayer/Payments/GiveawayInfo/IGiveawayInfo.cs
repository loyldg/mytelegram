// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/payments.GiveawayInfo" />
///</summary>
public interface IGiveawayInfo : IObject
{
    BitArray Flags { get; set; }
    int StartDate { get; set; }
}
