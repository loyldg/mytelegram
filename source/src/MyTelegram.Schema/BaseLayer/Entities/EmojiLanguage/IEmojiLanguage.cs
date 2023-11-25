// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Emoji language
/// See <a href="https://corefork.telegram.org/constructor/EmojiLanguage" />
///</summary>
[JsonDerivedType(typeof(TEmojiLanguage), nameof(TEmojiLanguage))]
public interface IEmojiLanguage : IObject
{
    ///<summary>
    /// Language code
    ///</summary>
    string LangCode { get; set; }
}
