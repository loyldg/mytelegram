// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/InputReplyTo" />
///</summary>
[JsonDerivedType(typeof(TInputReplyToMessage), nameof(TInputReplyToMessage))]
[JsonDerivedType(typeof(TInputReplyToStory), nameof(TInputReplyToStory))]
public interface IInputReplyTo : IObject
{

}
