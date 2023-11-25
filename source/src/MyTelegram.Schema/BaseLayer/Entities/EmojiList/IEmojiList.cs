// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a list of <a href="https://corefork.telegram.org/api/custom-emoji">custom emojis</a>.
/// See <a href="https://corefork.telegram.org/constructor/EmojiList" />
///</summary>
[JsonDerivedType(typeof(TEmojiListNotModified), nameof(TEmojiListNotModified))]
[JsonDerivedType(typeof(TEmojiList), nameof(TEmojiList))]
public interface IEmojiList : IObject
{

}
