// ReSharper disable All

namespace MyTelegram.Schema.Premium;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/premium.BoostsStatus" />
///</summary>
[JsonDerivedType(typeof(TBoostsStatus), nameof(TBoostsStatus))]
public interface IBoostsStatus : IObject
{
    BitArray Flags { get; set; }
    bool MyBoost { get; set; }
    int Level { get; set; }
    int CurrentLevelBoosts { get; set; }
    int Boosts { get; set; }
    int? GiftBoosts { get; set; }
    int? NextLevelBoosts { get; set; }
    MyTelegram.Schema.IStatsPercentValue? PremiumAudience { get; set; }
    string BoostUrl { get; set; }
    TVector<MyTelegram.Schema.IPrepaidGiveaway>? PrepaidGiveaways { get; set; }
    TVector<int>? MyBoostSlots { get; set; }
}
