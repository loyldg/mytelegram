// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Changes chat photo and sends a service message on it
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 PHOTO_CROP_SIZE_SMALL Photo is too small.
/// 400 PHOTO_EXT_INVALID The extension of the photo is invalid.
/// 400 PHOTO_INVALID Photo invalid.
/// See <a href="https://corefork.telegram.org/method/messages.editChatPhoto" />
///</summary>
internal sealed class EditChatPhotoHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestEditChatPhoto, MyTelegram.Schema.IUpdates>,
    Messages.IEditChatPhotoHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestEditChatPhoto obj)
    {
        throw new NotImplementedException();
    }
}
