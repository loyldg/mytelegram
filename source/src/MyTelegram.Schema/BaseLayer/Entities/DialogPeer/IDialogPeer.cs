// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Peer, or all peers in a folder
/// See <a href="https://corefork.telegram.org/constructor/DialogPeer" />
///</summary>
[JsonDerivedType(typeof(TDialogPeer), nameof(TDialogPeer))]
[JsonDerivedType(typeof(TDialogPeerFolder), nameof(TDialogPeerFolder))]
public interface IDialogPeer : IObject
{

}
