// ReSharper disable All

using MyTelegram.Schema.Phone;

namespace MyTelegram.Handlers.Phone;

public class GetGroupCallStreamChannelsHandler :
    RpcResultObjectHandler<RequestGetGroupCallStreamChannels, IGroupCallStreamChannels>,
    Phone.IGetGroupCallStreamChannelsHandler
{
    protected override Task<IGroupCallStreamChannels> HandleCoreAsync(IRequestInput input,
        RequestGetGroupCallStreamChannels obj)
    {
        throw new NotImplementedException();
    }
}