using MyTelegram.Domain.Commands.Channel;
using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class EditPhotoHandler : RpcResultObjectHandler<RequestEditPhoto, IUpdates>,
    IEditPhotoHandler, IProcessedHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IMediaHelper _mediaHelper;
    private readonly IRandomHelper _randomHelper;

    public EditPhotoHandler(IMediaHelper mediaHelper,
        ICommandBus commandBus,
        IRandomHelper randomHelper)
    {
        _mediaHelper = mediaHelper;
        _commandBus = commandBus;
        _randomHelper = randomHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestEditPhoto obj)
    {
        var channelId = obj.Channel switch
        {
            TInputChannel inputChannel => inputChannel.ChannelId,
            _ => throw new UserFriendlyException(RpcErrorMessages.ChannelInvalid)
        };

        long fileId = 0;
        var parts = 0;
        var md5 = string.Empty;
        var name = string.Empty;
        var hasVideo = false;
        double? videoStartTs = 0;
        switch (obj.Photo)
        {
            case TInputChatPhoto inputChatPhoto:
                switch (inputChatPhoto.Id)
                {
                    case TInputPhoto inputPhoto:
                        fileId = inputPhoto.Id;
                        break;
                    case TInputPhotoEmpty:
                        break;
                    default:
                        //throw new ArgumentOutOfRangeException(nameof(inputChatPhoto.Id));
                        throw new UserFriendlyException(RpcErrorMessages.PhotoInvalid);
                }

                break;
            case TInputChatPhotoEmpty:
                break;
            case TInputChatUploadedPhoto inputChatUploadedPhoto:
                var file = inputChatUploadedPhoto.File ?? inputChatUploadedPhoto.Video;
                if (file == null)
                {
                    ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.PhotoInvalid);
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
                //throw new ArgumentOutOfRangeException();
                throw new UserFriendlyException(RpcErrorMessages.InvalidOperation);
        }

        var photo = await _mediaHelper.SavePhotoAsync(input.ReqMsgId,
            fileId,
            hasVideo,
            videoStartTs,
            parts,
            name,
            md5);
        var command = new EditChannelPhotoCommand(ChannelId.Create(channelId),
            input.ToRequestInfo(),
            fileId,
            photo.ToBytes(),
            new TMessageActionChatEditPhoto { Photo = photo }.ToBytes().ToHexString(),
            _randomHelper.NextLong(),
            Guid.NewGuid());
        await _commandBus.PublishAsync(command, CancellationToken.None);

        return null!;
    }
}
