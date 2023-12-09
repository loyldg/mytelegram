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
    private readonly ICommandBus _commandBus;
    private readonly IMediaHelper _mediaHelper;
    private readonly IRandomHelper _randomHelper;

    public EditChatPhotoHandler(IMediaHelper mediaHelper,
        ICommandBus commandBus,
        IRandomHelper randomHelper)
    {
        _mediaHelper = mediaHelper;
        _commandBus = commandBus;
        _randomHelper = randomHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestEditChatPhoto obj)
    {
        var chatId = obj.ChatId;
        long fileId = 0;
        var parts = 0;
        var md5 = string.Empty;
        var name = string.Empty;
        var hasVideo = false;
        double? videoStartTs = 0;
        switch (obj.Photo)
        {
            case Schema.TInputChatUploadedPhoto inputChatUploadedPhoto:
                {
                    {
                        var file = inputChatUploadedPhoto.File ?? inputChatUploadedPhoto.Video;
                        if (file == null)
                        {
                            RpcErrors.RpcErrors400.PhotoInvalid.ThrowRpcError();
                        }

                        fileId = file!.Id;
                        parts = file.Parts;
                        name = file.Name;
                        hasVideo = inputChatUploadedPhoto.Video != null;
                        videoStartTs = inputChatUploadedPhoto.VideoStartTs;
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
                        throw new ArgumentOutOfRangeException();
                }
                break;
            case TInputChatPhotoEmpty:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        var r = await _mediaHelper.SavePhotoAsync(input.ReqMsgId,
            fileId,
            hasVideo,
            videoStartTs,
            parts,
            name,
            md5);
        var command = new EditChatPhotoCommand(ChatId.Create(chatId),
            input.ToRequestInfo(),
            fileId,
            r.PhotoId,
            //photo.ToBytes(),
            new TMessageActionChatEditPhoto { Photo = r.Photo }.ToBytes().ToHexString(),
            _randomHelper.NextLong());
        await _commandBus.PublishAsync(command, CancellationToken.None);

        return null!;
    }
}
