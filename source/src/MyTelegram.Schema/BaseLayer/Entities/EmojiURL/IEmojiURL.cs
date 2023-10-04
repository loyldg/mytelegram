// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Emoji URL
/// See <a href="https://corefork.telegram.org/constructor/EmojiURL" />
///</summary>
public interface IEmojiURL : IObject
{
    ///<summary>
    /// An HTTP URL which can be used to automatically log in into translation platform and suggest new emoji replacements. The URL will be valid for 30 seconds after generation
    ///</summary>
    string Url { get; set; }
}
