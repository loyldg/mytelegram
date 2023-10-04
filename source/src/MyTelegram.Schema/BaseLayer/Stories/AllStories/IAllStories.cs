// ReSharper disable All

namespace MyTelegram.Schema.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/stories.AllStories" />
///</summary>
public interface IAllStories : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    string State { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/StoriesStealthMode" />
    ///</summary>
    MyTelegram.Schema.IStoriesStealthMode StealthMode { get; set; }
}
