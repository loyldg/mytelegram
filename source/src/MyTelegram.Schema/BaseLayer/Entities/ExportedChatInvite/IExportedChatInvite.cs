// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Exported chat invite
/// See <a href="https://corefork.telegram.org/constructor/ExportedChatInvite" />
///</summary>
[JsonDerivedType(typeof(TChatInviteExported), nameof(TChatInviteExported))]
[JsonDerivedType(typeof(TChatInvitePublicJoinRequests), nameof(TChatInvitePublicJoinRequests))]
public interface IExportedChatInvite : IObject
{

}
