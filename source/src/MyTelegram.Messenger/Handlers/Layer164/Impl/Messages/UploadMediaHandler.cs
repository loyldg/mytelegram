// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Upload a file and associate it to a chat (without actually sending it to the chat)
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 CHAT_RESTRICTED You can't send messages in this chat, you were restricted.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 FILE_PARTS_INVALID The number of file parts is invalid.
/// 500 FILE_WRITE_EMPTY &nbsp;
/// 400 IMAGE_PROCESS_FAILED Failure while processing image.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 MEDIA_INVALID Media invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 PHOTO_EXT_INVALID The extension of the photo is invalid.
/// 400 PHOTO_INVALID_DIMENSIONS The photo dimensions are invalid.
/// 400 PHOTO_SAVE_FILE_INVALID Internal issues, try again later.
/// 400 USER_BANNED_IN_CHANNEL You're banned from sending messages in supergroups/channels.
/// 400 WEBPAGE_CURL_FAILED Failure while fetching the webpage with cURL.
/// See <a href="https://corefork.telegram.org/method/messages.uploadMedia" />
///</summary>
internal sealed class UploadMediaHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestUploadMedia, MyTelegram.Schema.IMessageMedia>,
    Messages.IUploadMediaHandler
{
    protected override Task<MyTelegram.Schema.IMessageMedia> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestUploadMedia obj)
    {
        throw new NotImplementedException();
    }
}
