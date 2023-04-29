// ReSharper disable All

using MyTelegram.Schema.Phone;

namespace MyTelegram.Handlers.Phone;

public class GetGroupCallStreamRtmpUrlHandler :
    RpcResultObjectHandler<RequestGetGroupCallStreamRtmpUrl, IGroupCallStreamRtmpUrl>,
    Phone.IGetGroupCallStreamRtmpUrlHandler
{
    protected override Task<IGroupCallStreamRtmpUrl> HandleCoreAsync(IRequestInput input,
        RequestGetGroupCallStreamRtmpUrl obj)
    {
        throw new NotImplementedException();
    }
}
