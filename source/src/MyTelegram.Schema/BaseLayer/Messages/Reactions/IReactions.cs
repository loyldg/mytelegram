// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// A set of <a href="https://corefork.telegram.org/api/reactions">message reactions</a>
/// See <a href="https://corefork.telegram.org/constructor/messages.Reactions" />
///</summary>
[JsonDerivedType(typeof(TReactionsNotModified), nameof(TReactionsNotModified))]
[JsonDerivedType(typeof(TReactions), nameof(TReactions))]
public interface IReactions : IObject
{

}
