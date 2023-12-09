// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/PeerColor" />
///</summary>
[JsonDerivedType(typeof(TPeerColor), nameof(TPeerColor))]
public interface IPeerColor : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int? Color { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    long? BackgroundEmojiId { get; set; }
}
