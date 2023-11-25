// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/StoryView" />
///</summary>
[JsonDerivedType(typeof(TStoryView), nameof(TStoryView))]
public interface IStoryView : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    bool Blocked { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    bool BlockedMyStoriesFrom { get; set; }

    ///<summary>
    /// The user that viewed the story
    ///</summary>
    long UserId { get; set; }

    ///<summary>
    /// When did the user view the story
    ///</summary>
    int Date { get; set; }

    ///<summary>
    /// If present, contains the reaction that the user left on the story
    /// See <a href="https://corefork.telegram.org/type/Reaction" />
    ///</summary>
    MyTelegram.Schema.IReaction? Reaction { get; set; }
}
