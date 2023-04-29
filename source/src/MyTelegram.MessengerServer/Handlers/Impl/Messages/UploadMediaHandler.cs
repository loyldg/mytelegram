using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class UploadMediaHandler : RpcResultObjectHandler<RequestUploadMedia, IMessageMedia>,
    IUploadMediaHandler, IProcessedHandler
{
    private readonly IMediaHelper _mediaHelper;

    public UploadMediaHandler(IMediaHelper mediaHelper)
    {
        _mediaHelper = mediaHelper;
    }

    protected override async Task<IMessageMedia> HandleCoreAsync(IRequestInput input,
        RequestUploadMedia obj)
    {
        var media = await _mediaHelper.SaveMediaAsync(obj.Media);
        return media;
    }
}
