// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Object contains information on list of messages with auxiliary data.
/// See <a href="https://corefork.telegram.org/constructor/messages.Messages" />
///</summary>
[JsonDerivedType(typeof(TMessages), nameof(TMessages))]
[JsonDerivedType(typeof(TMessagesSlice), nameof(TMessagesSlice))]
[JsonDerivedType(typeof(TChannelMessages), nameof(TChannelMessages))]
[JsonDerivedType(typeof(TMessagesNotModified), nameof(TMessagesNotModified))]
public interface IMessages : IObject
{

}
