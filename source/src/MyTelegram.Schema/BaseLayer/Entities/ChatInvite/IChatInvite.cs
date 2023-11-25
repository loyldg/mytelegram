// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Chat invite
/// See <a href="https://corefork.telegram.org/constructor/ChatInvite" />
///</summary>
[JsonDerivedType(typeof(TChatInviteAlready), nameof(TChatInviteAlready))]
[JsonDerivedType(typeof(TChatInvite), nameof(TChatInvite))]
[JsonDerivedType(typeof(TChatInvitePeek), nameof(TChatInvitePeek))]
public interface IChatInvite : IObject
{

}
