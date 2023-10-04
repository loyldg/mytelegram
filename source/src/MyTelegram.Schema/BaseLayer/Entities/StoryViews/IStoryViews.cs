// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/StoryViews" />
///</summary>
public interface IStoryViews : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    bool HasViewers { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int ViewsCount { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int? ForwardsCount { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/ReactionCount" />
    ///</summary>
    TVector<MyTelegram.Schema.IReactionCount>? Reactions { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int? ReactionsCount { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    TVector<long>? RecentViewers { get; set; }
}
