// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Change the photo of a <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 FILE_PARTS_INVALID The number of file parts is invalid.
/// 400 FILE_REFERENCE_INVALID The specified <a href="https://corefork.telegram.org/api/file_reference">file reference</a> is invalid.
/// 400 PHOTO_CROP_SIZE_SMALL Photo is too small.
/// 400 PHOTO_EXT_INVALID The extension of the photo is invalid.
/// 400 PHOTO_INVALID Photo invalid.
/// 400 STICKER_MIME_INVALID The specified sticker MIME type is invalid.
/// See <a href="https://corefork.telegram.org/method/channels.editPhoto" />
///</summary>
internal sealed class EditPhotoHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestEditPhoto, MyTelegram.Schema.IUpdates>,
    Channels.IEditPhotoHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestEditPhoto obj)
    {
        throw new NotImplementedException();
    }
}
