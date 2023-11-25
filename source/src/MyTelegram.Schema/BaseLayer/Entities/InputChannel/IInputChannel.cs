// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a channel
/// See <a href="https://corefork.telegram.org/constructor/InputChannel" />
///</summary>
[JsonDerivedType(typeof(TInputChannelEmpty), nameof(TInputChannelEmpty))]
[JsonDerivedType(typeof(TInputChannel), nameof(TInputChannel))]
[JsonDerivedType(typeof(TInputChannelFromMessage), nameof(TInputChannelFromMessage))]
public interface IInputChannel : IObject
{

}
