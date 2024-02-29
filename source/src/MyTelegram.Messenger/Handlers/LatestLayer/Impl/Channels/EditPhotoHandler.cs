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
    private readonly ICommandBus _commandBus;
    private readonly IMediaHelper _mediaHelper;
    private readonly IRandomHelper _randomHelper;
    private readonly IAccessHashHelper _accessHashHelper;
    public EditPhotoHandler(IMediaHelper mediaHelper,
        ICommandBus commandBus,
        IRandomHelper randomHelper,
        IAccessHashHelper accessHashHelper)
    {
        _mediaHelper = mediaHelper;
        _commandBus = commandBus;
        _randomHelper = randomHelper;
        _accessHashHelper = accessHashHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestEditPhoto obj)
    {
        long channelId = 0;
        if (obj.Channel is TInputChannel inputChannel)
        {
            channelId = inputChannel.ChannelId;
            await _accessHashHelper.CheckAccessHashAsync(inputChannel.ChannelId, inputChannel.AccessHash);
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
        switch (obj.Photo)
        {
            case Schema.TInputChatUploadedPhoto inputChatUploadedPhoto1:
                {
                    {
                        var file = inputChatUploadedPhoto1.File ?? inputChatUploadedPhoto1.Video;
                        if (file == null)
                        {
                            RpcErrors.RpcErrors400.PhotoInvalid.ThrowRpcError();
                        }

                        fileId = file!.Id;
                        parts = file.Parts;
                        name = file.Name;
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
                }
                break;
            case TInputChatPhoto inputChatPhoto:
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
        if (fileId != 0)
        {
        var r = await _mediaHelper.SavePhotoAsync(input.ReqMsgId,
            fileId,
            hasVideo,
            videoStartTs,
            parts,
            name,
            md5);
            photoId = r.PhotoId;
            photo = r.Photo;
        }
        var command = new EditChannelPhotoCommand(ChannelId.Create(channelId),
            input.ToRequestInfo(),
            photoId,
            //photo.ToBytes(),
            new TMessageActionChatEditPhoto { Photo = photo }.ToBytes().ToHexString(),
            _randomHelper.NextLong());
        await _commandBus.PublishAsync(command, CancellationToken.None);

        return null!;
    }
}
