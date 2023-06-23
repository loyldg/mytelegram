using MyTelegram.Handlers.Contacts;
using MyTelegram.Schema.Contacts;

namespace MyTelegram.MessengerServer.Handlers.Impl.Contacts;

public class GetSavedHandler : RpcResultObjectHandler<RequestGetSaved, TVector<ISavedContact>>,
    IGetSavedHandler
{
    protected override Task<TVector<ISavedContact>> HandleCoreAsync(IRequestInput input,
        RequestGetSaved obj)
    {
        throw new NotImplementedException();
    }
}