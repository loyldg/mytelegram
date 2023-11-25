// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Peer
/// See <a href="https://corefork.telegram.org/constructor/InputPeer" />
///</summary>
[JsonDerivedType(typeof(TInputPeerEmpty), nameof(TInputPeerEmpty))]
[JsonDerivedType(typeof(TInputPeerSelf), nameof(TInputPeerSelf))]
[JsonDerivedType(typeof(TInputPeerChat), nameof(TInputPeerChat))]
[JsonDerivedType(typeof(TInputPeerUser), nameof(TInputPeerUser))]
[JsonDerivedType(typeof(TInputPeerChannel), nameof(TInputPeerChannel))]
[JsonDerivedType(typeof(TInputPeerUserFromMessage), nameof(TInputPeerUserFromMessage))]
[JsonDerivedType(typeof(TInputPeerChannelFromMessage), nameof(TInputPeerChannelFromMessage))]
public interface IInputPeer : IObject
{

}
