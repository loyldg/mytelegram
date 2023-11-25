// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// A list of <a href="https://corefork.telegram.org/api/emoji-status">emoji statuses</a>
/// See <a href="https://corefork.telegram.org/constructor/account.EmojiStatuses" />
///</summary>
[JsonDerivedType(typeof(TEmojiStatusesNotModified), nameof(TEmojiStatusesNotModified))]
[JsonDerivedType(typeof(TEmojiStatuses), nameof(TEmojiStatuses))]
public interface IEmojiStatuses : IObject
{

}
