// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/PeerStories" />
///</summary>
public interface IPeerStories : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    MyTelegram.Schema.IPeer Peer { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int? MaxReadId { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/StoryItem" />
    ///</summary>
    TVector<MyTelegram.Schema.IStoryItem> Stories { get; set; }
}
