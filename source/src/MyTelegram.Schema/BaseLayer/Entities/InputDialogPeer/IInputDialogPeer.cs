// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Peer, or all peers in a certain folder
/// See <a href="https://corefork.telegram.org/constructor/InputDialogPeer" />
///</summary>
[JsonDerivedType(typeof(TInputDialogPeer), nameof(TInputDialogPeer))]
[JsonDerivedType(typeof(TInputDialogPeerFolder), nameof(TInputDialogPeerFolder))]
public interface IInputDialogPeer : IObject
{

}
