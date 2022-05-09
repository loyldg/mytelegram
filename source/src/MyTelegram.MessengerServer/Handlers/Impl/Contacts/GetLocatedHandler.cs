using MyTelegram.Handlers.Contacts;
using MyTelegram.Schema.Contacts;

namespace MyTelegram.MessengerServer.Handlers.Impl.Contacts;

public class GetLocatedHandler : RpcResultObjectHandler<RequestGetLocated, IUpdates>,
    IGetLocatedHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestGetLocated obj)
    {
        throw new NotImplementedException();
    }
}
