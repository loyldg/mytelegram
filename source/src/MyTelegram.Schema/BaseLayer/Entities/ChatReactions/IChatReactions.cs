// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Available chat reactions
/// See <a href="https://corefork.telegram.org/constructor/ChatReactions" />
///</summary>
[JsonDerivedType(typeof(TChatReactionsNone), nameof(TChatReactionsNone))]
[JsonDerivedType(typeof(TChatReactionsAll), nameof(TChatReactionsAll))]
[JsonDerivedType(typeof(TChatReactionsSome), nameof(TChatReactionsSome))]
public interface IChatReactions : IObject
{

}
