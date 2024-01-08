// ReSharper disable All

namespace MyTelegram.Schema.Stories;

///<summary>
/// Reaction and view counters for a <a href="https://corefork.telegram.org/api/stories">story</a>
/// See <a href="https://corefork.telegram.org/constructor/stories.StoryViewsList" />
///</summary>
[JsonDerivedType(typeof(TStoryViewsList), nameof(TStoryViewsList))]
public interface IStoryViewsList : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Total number of results that can be fetched
    ///</summary>
    int Count { get; set; }
    int ViewsCount { get; set; }
    int ForwardsCount { get; set; }

    ///<summary>
    /// Number of reactions that were added to the story
    ///</summary>
    int ReactionsCount { get; set; }

    ///<summary>
    /// Story view date and reaction information
    /// See <a href="https://corefork.telegram.org/type/StoryView" />
    ///</summary>
    TVector<MyTelegram.Schema.IStoryView> Views { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Mentioned users
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }

    ///<summary>
    /// Offset for pagination
    ///</summary>
    string? NextOffset { get; set; }
}
