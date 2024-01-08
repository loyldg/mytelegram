// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a <a href="https://corefork.telegram.org/api/colors">color palette »</a>.
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
    /// <a href="https://corefork.telegram.org/api/colors">Color palette ID, see here »</a> for more info.
    ///</summary>
    int? Color { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/custom-emoji">Custom emoji ID</a> used to generate the pattern.
    ///</summary>
    long? BackgroundEmojiId { get; set; }
}
