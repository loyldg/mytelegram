// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Reply information
/// See <a href="https://corefork.telegram.org/constructor/MessageReplyHeader" />
///</summary>
[JsonDerivedType(typeof(TMessageReplyHeader), nameof(TMessageReplyHeader))]
[JsonDerivedType(typeof(TMessageReplyStoryHeader), nameof(TMessageReplyStoryHeader))]
public interface IMessageReplyHeader : IObject
{

}
