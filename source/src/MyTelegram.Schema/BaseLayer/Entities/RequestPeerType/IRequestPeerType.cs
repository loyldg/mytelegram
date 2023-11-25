// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Filtering criteria to use for the peer selection list shown to the user.
/// See <a href="https://corefork.telegram.org/constructor/RequestPeerType" />
///</summary>
[JsonDerivedType(typeof(TRequestPeerTypeUser), nameof(TRequestPeerTypeUser))]
[JsonDerivedType(typeof(TRequestPeerTypeChat), nameof(TRequestPeerTypeChat))]
[JsonDerivedType(typeof(TRequestPeerTypeBroadcast), nameof(TRequestPeerTypeBroadcast))]
public interface IRequestPeerType : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }
}
