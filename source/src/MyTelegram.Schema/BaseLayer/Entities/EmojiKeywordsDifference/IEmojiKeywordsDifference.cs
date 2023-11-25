// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// New emoji keywords
/// See <a href="https://corefork.telegram.org/constructor/EmojiKeywordsDifference" />
///</summary>
[JsonDerivedType(typeof(TEmojiKeywordsDifference), nameof(TEmojiKeywordsDifference))]
public interface IEmojiKeywordsDifference : IObject
{
    ///<summary>
    /// Language code for keywords
    ///</summary>
    string LangCode { get; set; }

    ///<summary>
    /// Previous emoji keyword list version
    ///</summary>
    int FromVersion { get; set; }

    ///<summary>
    /// Current version of emoji keyword list
    ///</summary>
    int Version { get; set; }

    ///<summary>
    /// Emojis associated to keywords
    /// See <a href="https://corefork.telegram.org/type/EmojiKeyword" />
    ///</summary>
    TVector<MyTelegram.Schema.IEmojiKeyword> Keywords { get; set; }
}
