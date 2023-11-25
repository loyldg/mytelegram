// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Indicates a supported peer type for a <a href="https://corefork.telegram.org/bots/webapps#launching-web-apps-from-the-attachment-menu">bot web app attachment menu</a>
/// See <a href="https://corefork.telegram.org/constructor/AttachMenuPeerType" />
///</summary>
[JsonDerivedType(typeof(TAttachMenuPeerTypeSameBotPM), nameof(TAttachMenuPeerTypeSameBotPM))]
[JsonDerivedType(typeof(TAttachMenuPeerTypeBotPM), nameof(TAttachMenuPeerTypeBotPM))]
[JsonDerivedType(typeof(TAttachMenuPeerTypePM), nameof(TAttachMenuPeerTypePM))]
[JsonDerivedType(typeof(TAttachMenuPeerTypeChat), nameof(TAttachMenuPeerTypeChat))]
[JsonDerivedType(typeof(TAttachMenuPeerTypeBroadcast), nameof(TAttachMenuPeerTypeBroadcast))]
public interface IAttachMenuPeerType : IObject
{

}
