// ReSharper disable All

namespace MyTelegram.Handlers.Folders;

///<summary>
/// Edit peers in <a href="https://corefork.telegram.org/api/folders#peer-folders">peer folder</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 FOLDER_ID_INVALID Invalid folder ID.
/// See <a href="https://corefork.telegram.org/method/folders.editPeerFolders" />
///</summary>
internal sealed class EditPeerFoldersHandler : RpcResultObjectHandler<MyTelegram.Schema.Folders.RequestEditPeerFolders, MyTelegram.Schema.IUpdates>,
    Folders.IEditPeerFoldersHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Folders.RequestEditPeerFolders obj)
    {
        throw new NotImplementedException();
    }
}
