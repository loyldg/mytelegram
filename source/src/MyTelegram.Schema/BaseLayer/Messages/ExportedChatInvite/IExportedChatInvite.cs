// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Contains info about a chat invite, and eventually a pointer to the newest chat invite.
/// See <a href="https://corefork.telegram.org/constructor/messages.ExportedChatInvite" />
///</summary>
[JsonDerivedType(typeof(TExportedChatInvite), nameof(TExportedChatInvite))]
[JsonDerivedType(typeof(TExportedChatInviteReplaced), nameof(TExportedChatInviteReplaced))]
public interface IExportedChatInvite : IObject
{
    ///<summary>
    /// The replaced chat invite
    /// See <a href="https://corefork.telegram.org/type/ExportedChatInvite" />
    ///</summary>
    MyTelegram.Schema.IExportedChatInvite Invite { get; set; }

    ///<summary>
    /// Mentioned users
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
