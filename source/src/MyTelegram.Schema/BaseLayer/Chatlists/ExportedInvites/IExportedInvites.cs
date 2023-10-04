// ReSharper disable All

namespace MyTelegram.Schema.Chatlists;

///<summary>
/// A list of exported <a href="https://corefork.telegram.org/api/links#chat-folder-links">chat folder deep links »</a>.
/// See <a href="https://corefork.telegram.org/constructor/chatlists.ExportedInvites" />
///</summary>
public interface IExportedInvites : IObject
{
    ///<summary>
    /// The <a href="https://corefork.telegram.org/api/links#chat-folder-links">chat folder deep links »</a>.
    /// See <a href="https://corefork.telegram.org/type/ExportedChatlistInvite" />
    ///</summary>
    TVector<MyTelegram.Schema.IExportedChatlistInvite> Invites { get; set; }

    ///<summary>
    /// Related chat information
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Related user information
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
