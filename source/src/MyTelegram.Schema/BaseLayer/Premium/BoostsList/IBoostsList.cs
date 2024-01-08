// ReSharper disable All

namespace MyTelegram.Schema.Premium;

///<summary>
/// List of <a href="https://corefork.telegram.org/api/boost">boosts</a> that were applied to a peer by multiple users.
/// See <a href="https://corefork.telegram.org/constructor/premium.BoostsList" />
///</summary>
[JsonDerivedType(typeof(TBoostsList), nameof(TBoostsList))]
public interface IBoostsList : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Total number of results
    ///</summary>
    int Count { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/boost">Boosts</a>
    /// See <a href="https://corefork.telegram.org/type/Boost" />
    ///</summary>
    TVector<MyTelegram.Schema.IBoost> Boosts { get; set; }

    ///<summary>
    /// Offset that can be used for <a href="https://corefork.telegram.org/api/offsets">pagination</a>.
    ///</summary>
    string? NextOffset { get; set; }

    ///<summary>
    /// Mentioned users
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
