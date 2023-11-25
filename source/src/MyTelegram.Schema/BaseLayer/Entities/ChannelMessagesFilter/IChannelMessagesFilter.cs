// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Filter for fetching only certain types of channel messages
/// See <a href="https://corefork.telegram.org/constructor/ChannelMessagesFilter" />
///</summary>
[JsonDerivedType(typeof(TChannelMessagesFilterEmpty), nameof(TChannelMessagesFilterEmpty))]
[JsonDerivedType(typeof(TChannelMessagesFilter), nameof(TChannelMessagesFilter))]
public interface IChannelMessagesFilter : IObject
{

}
