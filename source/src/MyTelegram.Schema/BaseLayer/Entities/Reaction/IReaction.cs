// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// <a href="https://corefork.telegram.org/api/reactions">Message reaction</a>
/// See <a href="https://corefork.telegram.org/constructor/Reaction" />
///</summary>
[JsonDerivedType(typeof(TReactionEmpty), nameof(TReactionEmpty))]
[JsonDerivedType(typeof(TReactionEmoji), nameof(TReactionEmoji))]
[JsonDerivedType(typeof(TReactionCustomEmoji), nameof(TReactionCustomEmoji))]
public interface IReaction : IObject
{

}
