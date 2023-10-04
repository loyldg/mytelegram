// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Info about a blocked user
/// See <a href="https://corefork.telegram.org/constructor/PeerBlocked" />
///</summary>
public interface IPeerBlocked : IObject
{
    ///<summary>
    /// Peer ID
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    MyTelegram.Schema.IPeer PeerId { get; set; }

    ///<summary>
    /// When was the peer blocked
    ///</summary>
    int Date { get; set; }
}
