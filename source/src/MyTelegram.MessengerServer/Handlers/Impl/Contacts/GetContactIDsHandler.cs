using MyTelegram.Handlers.Contacts;
using MyTelegram.Schema.Contacts;

namespace MyTelegram.MessengerServer.Handlers.Impl.Contacts;

public class GetContactIDsHandler : RpcResultObjectHandler<RequestGetContactIDs, TVector<int>>,
    IGetContactIDsHandler
{
    protected override Task<TVector<int>> HandleCoreAsync(IRequestInput input,
        RequestGetContactIDs obj)
    {
        throw new NotImplementedException();
    }
}
