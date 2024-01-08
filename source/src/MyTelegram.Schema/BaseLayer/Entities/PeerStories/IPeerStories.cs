// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// <a href="https://corefork.telegram.org/api/stories">Stories</a> associated to a peer
/// See <a href="https://corefork.telegram.org/constructor/PeerStories" />
///</summary>
[JsonDerivedType(typeof(TPeerStories), nameof(TPeerStories))]
public interface IPeerStories : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// The peer
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    MyTelegram.Schema.IPeer Peer { get; set; }

    ///<summary>
    /// If set, contains the ID of the maximum read story
    ///</summary>
    int? MaxReadId { get; set; }

    ///<summary>
    /// Stories
    /// See <a href="https://corefork.telegram.org/type/StoryItem" />
    ///</summary>
    TVector<MyTelegram.Schema.IStoryItem> Stories { get; set; }
}
