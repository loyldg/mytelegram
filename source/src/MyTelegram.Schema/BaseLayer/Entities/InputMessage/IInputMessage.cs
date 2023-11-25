// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A message
/// See <a href="https://corefork.telegram.org/constructor/InputMessage" />
///</summary>
[JsonDerivedType(typeof(TInputMessageID), nameof(TInputMessageID))]
[JsonDerivedType(typeof(TInputMessageReplyTo), nameof(TInputMessageReplyTo))]
[JsonDerivedType(typeof(TInputMessagePinned), nameof(TInputMessagePinned))]
[JsonDerivedType(typeof(TInputMessageCallbackQuery), nameof(TInputMessageCallbackQuery))]
public interface IInputMessage : IObject
{

}
