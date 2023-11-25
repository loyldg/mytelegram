// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Represents a list of <a href="https://corefork.telegram.org/api/custom-emoji#emoji-categories">emoji categories</a>.
/// See <a href="https://corefork.telegram.org/constructor/messages.EmojiGroups" />
///</summary>
[JsonDerivedType(typeof(TEmojiGroupsNotModified), nameof(TEmojiGroupsNotModified))]
[JsonDerivedType(typeof(TEmojiGroups), nameof(TEmojiGroups))]
public interface IEmojiGroups : IObject
{

}
