// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Emoji keyword
/// See <a href="https://corefork.telegram.org/constructor/EmojiKeyword" />
///</summary>
[JsonDerivedType(typeof(TEmojiKeyword), nameof(TEmojiKeyword))]
[JsonDerivedType(typeof(TEmojiKeywordDeleted), nameof(TEmojiKeywordDeleted))]
public interface IEmojiKeyword : IObject
{
    ///<summary>
    /// Keyword
    ///</summary>
    string Keyword { get; set; }

    ///<summary>
    /// Emojis that were associated to keyword
    ///</summary>
    TVector<string> Emoticons { get; set; }
}
