// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/help.PeerColorOption" />
///</summary>
[JsonDerivedType(typeof(TPeerColorOption), nameof(TPeerColorOption))]
public interface IPeerColorOption : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    bool Hidden { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int ColorId { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/help.PeerColorSet" />
    ///</summary>
    MyTelegram.Schema.Help.IPeerColorSet? Colors { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/help.PeerColorSet" />
    ///</summary>
    MyTelegram.Schema.Help.IPeerColorSet? DarkColors { get; set; }
}
