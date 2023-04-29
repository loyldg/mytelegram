using MyTelegram.Domain.Commands.User;
using MyTelegram.Handlers.Photos;
using MyTelegram.Schema.Photos;
using IPhoto = MyTelegram.Schema.Photos.IPhoto;

namespace MyTelegram.MessengerServer.Handlers.Impl.Photos;

public class UploadProfilePhotoHandler : RpcResultObjectHandler<RequestUploadProfilePhoto, IPhoto>,
    IUploadProfilePhotoHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;
    private readonly IMediaHelper _mediaHelper;

    public UploadProfilePhotoHandler(IMediaHelper mediaHelper,
        ICommandBus commandBus)
    {
        _mediaHelper = mediaHelper;
        _commandBus = commandBus;
    }

    protected override async Task<IPhoto> HandleCoreAsync(IRequestInput input,
        RequestUploadProfilePhoto obj)
    {
        var file = obj.File ?? obj.Video;
        string? md5 = null;
        //Console.WriteLine();
        //if (obj.File != null && obj.Video != null)
        //{
        //    Console.WriteLine("###################################################");
        //    Console.WriteLine("###################################################");
        //}

        //if (obj.Video != null)
        //{
        //    throw new InternalException("Internal_Server_Error_Set video as avatar is not supported");
        //    //Console.WriteLine($"### Video:{obj.Video.Name} VideoStartTs={obj.VideoStartTs}");
        //}

        switch (file)
        {
            case TInputFile inputFile:
                md5 = inputFile.Md5Checksum;
                break;
            case TInputFileBig:
                break;
            default:
                throw new NotSupportedException($"Not supported file type:{file}");
        }

        var photo = await _mediaHelper.SavePhotoAsync(input.ReqMsgId,
            file.Id,
            obj.Video != null,
            obj.VideoStartTs,
            file.Parts,
            file.Name,
            md5 ?? string.Empty);
        var command = new UpdateProfilePhotoCommand(UserId.Create(input.UserId),
            input.ReqMsgId,
            photo.Id,
            photo.ToBytes()
        );
        await _commandBus.PublishAsync(command, default);

        return null!;
        //return photo;
    }
}
