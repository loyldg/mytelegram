// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

public class GetGroupCallStreamChannelsHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestGetGroupCallStreamChannels, MyTelegram.Schema.Phone.IGroupCallStreamChannels>,
    Phone.IGetGroupCallStreamChannelsHandler
{
    protected override Task<MyTelegram.Schema.Phone.IGroupCallStreamChannels> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestGetGroupCallStreamChannels obj)
    {
        throw new NotImplementedException();
    }
}
