// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents an <a href="https://corefork.telegram.org/api/custom-emoji#emoji-categories">emoji category</a>.
/// See <a href="https://corefork.telegram.org/constructor/EmojiGroup" />
///</summary>
public interface IEmojiGroup : IObject
{
    ///<summary>
    /// Category name, i.e. "Animals", "Flags", "Faces" and so on...
    ///</summary>
    string Title { get; set; }

    ///<summary>
    /// A single custom emoji used as preview for the category.
    ///</summary>
    long IconEmojiId { get; set; }

    ///<summary>
    /// A list of UTF-8 emojis, matching the category.
    ///</summary>
    TVector<string> Emoticons { get; set; }
}
