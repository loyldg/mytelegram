// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Aggregated view and reaction information of a <a href="https://corefork.telegram.org/api/stories">story</a>
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
    /// If set, indicates that the viewers list is currently viewable, and was not yet deleted because the story has expired while the user didn't have a <a href="https://corefork.telegram.org/api/premium">Premium</a> account.
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
    /// Number of reactions added to the story
    ///</summary>
    int? ReactionsCount { get; set; }

    ///<summary>
    /// User IDs of some recent viewers of the story
    ///</summary>
    TVector<long>? RecentViewers { get; set; }
}
