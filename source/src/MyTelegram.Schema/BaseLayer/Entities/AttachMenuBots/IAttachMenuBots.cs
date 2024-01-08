// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a list of <a href="https://corefork.telegram.org/bots/webapps#launching-mini-apps-from-the-attachment-menu">bot mini apps that can be launched from the attachment menu »</a>
/// See <a href="https://corefork.telegram.org/constructor/AttachMenuBots" />
///</summary>
[JsonDerivedType(typeof(TAttachMenuBotsNotModified), nameof(TAttachMenuBotsNotModified))]
[JsonDerivedType(typeof(TAttachMenuBots), nameof(TAttachMenuBots))]
public interface IAttachMenuBots : IObject
{

}
