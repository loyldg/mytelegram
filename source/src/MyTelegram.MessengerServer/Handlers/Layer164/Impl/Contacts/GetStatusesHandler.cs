using MyTelegram.Handlers.Contacts;
using MyTelegram.Schema.Contacts;

namespace MyTelegram.MessengerServer.Handlers.Impl.Contacts;

public class GetStatusesHandler : RpcResultObjectHandler<RequestGetStatuses, TVector<IContactStatus>>,
    IGetStatusesHandler
{
    protected override Task<TVector<IContactStatus>> HandleCoreAsync(IRequestInput input,
        RequestGetStatuses obj)
    {
        throw new NotImplementedException();
    }
}