// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Top peers by top peer category
/// See <a href="https://corefork.telegram.org/constructor/TopPeerCategoryPeers" />
///</summary>
[JsonDerivedType(typeof(TTopPeerCategoryPeers), nameof(TTopPeerCategoryPeers))]
public interface ITopPeerCategoryPeers : IObject
{
    ///<summary>
    /// Top peer category of peers
    /// See <a href="https://corefork.telegram.org/type/TopPeerCategory" />
    ///</summary>
    MyTelegram.Schema.ITopPeerCategory Category { get; set; }

    ///<summary>
    /// Count of peers
    ///</summary>
    int Count { get; set; }

    ///<summary>
    /// Peers
    /// See <a href="https://corefork.telegram.org/type/TopPeer" />
    ///</summary>
    TVector<MyTelegram.Schema.ITopPeer> Peers { get; set; }
}
