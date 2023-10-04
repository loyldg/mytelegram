// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Info about chat invites exported by a certain admin.
/// See <a href="https://corefork.telegram.org/constructor/messages.ExportedChatInvites" />
///</summary>
public interface IExportedChatInvites : IObject
{
    ///<summary>
    /// Number of invites exported by the admin
    ///</summary>
    int Count { get; set; }

    ///<summary>
    /// Exported invites
    /// See <a href="https://corefork.telegram.org/type/ExportedChatInvite" />
    ///</summary>
    TVector<MyTelegram.Schema.IExportedChatInvite> Invites { get; set; }

    ///<summary>
    /// Info about the admin
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
