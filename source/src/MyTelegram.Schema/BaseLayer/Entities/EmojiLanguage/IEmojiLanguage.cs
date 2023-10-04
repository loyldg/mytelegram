// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Emoji language
/// See <a href="https://corefork.telegram.org/constructor/EmojiLanguage" />
///</summary>
public interface IEmojiLanguage : IObject
{
    ///<summary>
    /// Language code
    ///</summary>
    string LangCode { get; set; }
}
