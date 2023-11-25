// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Inline query peer type.
/// See <a href="https://corefork.telegram.org/constructor/InlineQueryPeerType" />
///</summary>
[JsonDerivedType(typeof(TInlineQueryPeerTypeSameBotPM), nameof(TInlineQueryPeerTypeSameBotPM))]
[JsonDerivedType(typeof(TInlineQueryPeerTypePM), nameof(TInlineQueryPeerTypePM))]
[JsonDerivedType(typeof(TInlineQueryPeerTypeChat), nameof(TInlineQueryPeerTypeChat))]
[JsonDerivedType(typeof(TInlineQueryPeerTypeMegagroup), nameof(TInlineQueryPeerTypeMegagroup))]
[JsonDerivedType(typeof(TInlineQueryPeerTypeBroadcast), nameof(TInlineQueryPeerTypeBroadcast))]
[JsonDerivedType(typeof(TInlineQueryPeerTypeBotPM), nameof(TInlineQueryPeerTypeBotPM))]
public interface IInlineQueryPeerType : IObject
{

}
