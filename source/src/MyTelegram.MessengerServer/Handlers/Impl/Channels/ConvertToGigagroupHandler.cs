using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class ConvertToGigagroupHandler : RpcResultObjectHandler<RequestConvertToGigagroup, IUpdates>,
    IConvertToGigagroupHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestConvertToGigagroup obj)
    {
        throw new NotImplementedException();
    }
}