// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/MyBoost" />
///</summary>
public interface IMyBoost : IObject
{
    BitArray Flags { get; set; }
    int Slot { get; set; }
    MyTelegram.Schema.IPeer? Peer { get; set; }
    int Date { get; set; }
    int Expires { get; set; }
    int? CooldownUntilDate { get; set; }
}
