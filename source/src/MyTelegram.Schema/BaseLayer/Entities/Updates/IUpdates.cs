// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object which is perceived by the client without a call on its part when an event occurs.
/// See <a href="https://corefork.telegram.org/constructor/Updates" />
///</summary>
[JsonDerivedType(typeof(TUpdatesTooLong), nameof(TUpdatesTooLong))]
[JsonDerivedType(typeof(TUpdateShortMessage), nameof(TUpdateShortMessage))]
[JsonDerivedType(typeof(TUpdateShortChatMessage), nameof(TUpdateShortChatMessage))]
[JsonDerivedType(typeof(TUpdateShort), nameof(TUpdateShort))]
[JsonDerivedType(typeof(TUpdatesCombined), nameof(TUpdatesCombined))]
[JsonDerivedType(typeof(TUpdates), nameof(TUpdates))]
[JsonDerivedType(typeof(TUpdateShortSentMessage), nameof(TUpdateShortSentMessage))]
public interface IUpdates : IObject
{

}
