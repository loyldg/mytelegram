// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/StoryView" />
///</summary>
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
    /// &nbsp;
    ///</summary>
    long UserId { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int Date { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/Reaction" />
    ///</summary>
    MyTelegram.Schema.IReaction? Reaction { get; set; }
}
