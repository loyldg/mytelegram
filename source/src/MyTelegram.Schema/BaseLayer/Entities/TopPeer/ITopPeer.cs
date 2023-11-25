// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Top peer
/// See <a href="https://corefork.telegram.org/constructor/TopPeer" />
///</summary>
[JsonDerivedType(typeof(TTopPeer), nameof(TTopPeer))]
public interface ITopPeer : IObject
{
    ///<summary>
    /// Peer
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    MyTelegram.Schema.IPeer Peer { get; set; }

    ///<summary>
    /// Rating as computed in <a href="https://corefork.telegram.org/api/top-rating">top peer rating »</a>
    ///</summary>
    double Rating { get; set; }
}
