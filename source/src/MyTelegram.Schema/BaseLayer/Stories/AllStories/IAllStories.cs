// ReSharper disable All

namespace MyTelegram.Schema.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/stories.AllStories" />
///</summary>
[JsonDerivedType(typeof(TAllStoriesNotModified), nameof(TAllStoriesNotModified))]
[JsonDerivedType(typeof(TAllStories), nameof(TAllStories))]
public interface IAllStories : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// State to use for pagination
    ///</summary>
    string State { get; set; }

    ///<summary>
    /// Current <a href="https://corefork.telegram.org/api/stories#stealth-mode">stealth mode</a> information
    /// See <a href="https://corefork.telegram.org/type/StoriesStealthMode" />
    ///</summary>
    MyTelegram.Schema.IStoriesStealthMode StealthMode { get; set; }
}
