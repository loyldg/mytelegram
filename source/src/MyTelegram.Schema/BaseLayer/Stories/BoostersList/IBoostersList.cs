// ReSharper disable All

namespace MyTelegram.Schema.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/stories.BoostersList" />
///</summary>
public interface IBoostersList : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int Count { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/Booster" />
    ///</summary>
    TVector<MyTelegram.Schema.IBooster> Boosters { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    string? NextOffset { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
