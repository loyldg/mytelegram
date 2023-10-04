// ReSharper disable All

namespace MyTelegram.Schema.Chatlists;

///<summary>
/// Exported <a href="https://corefork.telegram.org/api/links#chat-folder-links">chat folder deep link »</a>.
/// See <a href="https://corefork.telegram.org/constructor/chatlists.ExportedChatlistInvite" />
///</summary>
public interface IExportedChatlistInvite : IObject
{
    ///<summary>
    /// Folder ID
    /// See <a href="https://corefork.telegram.org/type/DialogFilter" />
    ///</summary>
    MyTelegram.Schema.IDialogFilter Filter { get; set; }

    ///<summary>
    /// The exported <a href="https://corefork.telegram.org/api/links#chat-folder-links">chat folder deep link »</a>.
    /// See <a href="https://corefork.telegram.org/type/ExportedChatlistInvite" />
    ///</summary>
    MyTelegram.Schema.IExportedChatlistInvite Invite { get; set; }
}
