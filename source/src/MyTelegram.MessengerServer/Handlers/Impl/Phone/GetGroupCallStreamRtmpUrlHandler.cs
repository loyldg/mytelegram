// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

public class GetGroupCallStreamRtmpUrlHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestGetGroupCallStreamRtmpUrl, MyTelegram.Schema.Phone.IGroupCallStreamRtmpUrl>,
    Phone.IGetGroupCallStreamRtmpUrlHandler
{
    protected override Task<MyTelegram.Schema.Phone.IGroupCallStreamRtmpUrl> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestGetGroupCallStreamRtmpUrl obj)
    {
        throw new NotImplementedException();
    }
}
