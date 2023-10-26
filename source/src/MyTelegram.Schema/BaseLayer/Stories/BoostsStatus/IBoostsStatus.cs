// ReSharper disable All

namespace MyTelegram.Schema.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/stories.BoostsStatus" />
///</summary>
public interface IBoostsStatus : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    bool MyBoost { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int Level { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int CurrentLevelBoosts { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int Boosts { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int? NextLevelBoosts { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/StatsPercentValue" />
    ///</summary>
    MyTelegram.Schema.IStatsPercentValue? PremiumAudience { get; set; }
    string BoostUrl { get; set; }
}
