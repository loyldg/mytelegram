using MyTelegram.Domain.Commands.Chat;
using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class EditChatPhotoHandler : RpcResultObjectHandler<RequestEditChatPhoto, IUpdates>,
    IEditChatPhotoHandler, IProcessedHandler //, IShouldCacheRequest
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
        //var photo=await _mediaHelper.SavePhotoAsync(input.ReqMsgId,obj)
        var chatId = obj.ChatId;
        long fileId = 0;
        var parts = 0;
        var md5 = string.Empty;
        var name = string.Empty;
        var hasVideo = false;
        double? videoStartTs = 0;
        switch (obj.Photo)
        {
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
                        //throw new ArgumentOutOfRangeException();
                        throw new UserFriendlyException(RpcErrorMessages.InvalidOperation);
                }

                break;
            case TInputChatPhotoEmpty:
                break;
            case TInputChatUploadedPhoto inputChatUploadedPhoto:
                var file = inputChatUploadedPhoto.File ?? inputChatUploadedPhoto.Video;
                if (file == null)
                {
                    ThrowHelper.ThrowUserFriendlyException("PHOTO_INVALID");
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
                        //throw new ArgumentOutOfRangeException(nameof(file));
                        throw new UserFriendlyException(RpcErrorMessages.InvalidOperation);
                }

                break;
            default:
                throw new UserFriendlyException(RpcErrorMessages.InvalidOperation);
        }

        var photo = await _mediaHelper.SavePhotoAsync(input.ReqMsgId,
            fileId,
            hasVideo,
            videoStartTs,
            parts,
            name,
            md5).ConfigureAwait(false);
        var command = new EditChatPhotoCommand(ChatId.Create(chatId),
            input.ToRequestInfo(),
            fileId,
            photo.ToBytes(),
            new TMessageActionChatEditPhoto { Photo = photo }.ToBytes().ToHexString(),
            _randomHelper.NextLong(),
            Guid.NewGuid());
        await _commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);

        return null!;
    }
}
