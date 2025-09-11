namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

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
/// 400 IMAGE_PROCESS_FAILED Failure while processing image.
/// 400 PHOTO_CROP_SIZE_SMALL Photo is too small.
/// 400 PHOTO_EXT_INVALID The extension of the photo is invalid.
/// 400 PHOTO_INVALID Photo invalid.
/// 400 STICKER_MIME_INVALID The specified sticker MIME type is invalid.
/// See <a href="https://corefork.telegram.org/method/channels.editPhoto" />
///</summary>
internal sealed class EditPhotoHandler(
    IMediaHelper mediaHelper,
    ICommandBus commandBus,
    IRandomHelper randomHelper,
    IAccessHashHelper accessHashHelper)
    : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestEditPhoto, MyTelegram.Schema.IUpdates>
{
    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestEditPhoto obj)
    {
        long channelId = 0;
        if (obj.Channel is TInputChannel inputChannel)
        {
            channelId = inputChannel.ChannelId;
            await accessHashHelper.CheckAccessHashAsync(input, inputChannel.ChannelId, inputChannel.AccessHash, AccessHashType.Channel);
        }
        else
        {
            throw new NotImplementedException();
        }

        long fileId = 0;
        var parts = 0;
        var md5 = string.Empty;
        var name = string.Empty;
        var hasVideo = false;
        double? videoStartTs = 0;
        IVideoSize? videoSize = null;
        switch (obj.Photo)
        {
            case Schema.TInputChatUploadedPhoto inputChatUploadedPhoto1:
                {
                    var file = inputChatUploadedPhoto1.File ?? inputChatUploadedPhoto1.Video;
                    if (file != null && file is TInputFile tInputFile)
                    {
                        fileId = tInputFile!.Id;
                        parts = tInputFile.Parts;
                        name = tInputFile.Name;
                        hasVideo = inputChatUploadedPhoto1.Video != null;
                        videoStartTs = inputChatUploadedPhoto1.VideoStartTs;
                        switch (file)
                        {
                            case TInputFile inputFile:
                                md5 = inputFile.Md5Checksum;
                                break;
                            case TInputFileBig:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(file));
                        }
                    }

                    videoSize = inputChatUploadedPhoto1.VideoEmojiMarkup;
                }
                break;
            case TInputChatPhoto inputChatPhoto:
                //photo=await _mediaHelper.SavePhotoAsync(input.ReqMsgId,inputChatPhoto.)
                switch (inputChatPhoto.Id)
                {
                    case TInputPhoto inputPhoto:
                        fileId = inputPhoto.Id;
                        break;
                    case TInputPhotoEmpty:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(inputChatPhoto.Id));
                }

                break;
            case TInputChatPhotoEmpty:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        IPhoto photo = new TPhotoEmpty();

        long? photoId = null;
        var r = await mediaHelper.SavePhotoAsync(input.ReqMsgId,
            input.UserId,
            fileId,
            hasVideo,
            videoStartTs,
            parts,
            name,
            md5,
            videoSize
        );
        photoId = r.PhotoId;
        photo = r.Photo;

        var command = new EditChannelPhotoCommand(ChannelId.Create(channelId),
            input.ToRequestInfo(),
            photoId,
            new TMessageActionChatEditPhoto { Photo = photo },
            randomHelper.NextInt64());
        await commandBus.PublishAsync(command);

        return null!;
    }
}
