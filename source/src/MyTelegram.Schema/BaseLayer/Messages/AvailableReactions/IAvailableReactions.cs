// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Animations and metadata associated with <a href="https://corefork.telegram.org/api/reactions">message reactions »</a>
/// See <a href="https://corefork.telegram.org/constructor/messages.AvailableReactions" />
///</summary>
[JsonDerivedType(typeof(TAvailableReactionsNotModified), nameof(TAvailableReactionsNotModified))]
[JsonDerivedType(typeof(TAvailableReactions), nameof(TAvailableReactions))]
public interface IAvailableReactions : IObject
{

}
