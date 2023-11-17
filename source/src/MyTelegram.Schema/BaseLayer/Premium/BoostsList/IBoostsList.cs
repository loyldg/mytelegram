// ReSharper disable All

namespace MyTelegram.Schema.Premium;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/premium.BoostsList" />
///</summary>
public interface IBoostsList : IObject
{
    BitArray Flags { get; set; }
    int Count { get; set; }
    TVector<MyTelegram.Schema.IBoost> Boosts { get; set; }
    string? NextOffset { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
