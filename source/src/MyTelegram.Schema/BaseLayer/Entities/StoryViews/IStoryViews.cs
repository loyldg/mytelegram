// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/StoryViews" />
///</summary>
[JsonDerivedType(typeof(TStoryViews), nameof(TStoryViews))]
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
    /// View counter of the story
    ///</summary>
    int ViewsCount { get; set; }

    ///<summary>
    /// Forward counter of the story
    ///</summary>
    int? ForwardsCount { get; set; }

    ///<summary>
    /// All reactions sent to this story
    /// See <a href="https://corefork.telegram.org/type/ReactionCount" />
    ///</summary>
    TVector<MyTelegram.Schema.IReactionCount>? Reactions { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int? ReactionsCount { get; set; }

    ///<summary>
    /// User IDs of some recent viewers of the story
    ///</summary>
    TVector<long>? RecentViewers { get; set; }
}
