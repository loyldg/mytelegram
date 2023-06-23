using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class GetAllSecureValuesHandler : RpcResultObjectHandler<RequestGetAllSecureValues, TVector<ISecureValue>>,
    IGetAllSecureValuesHandler
{
    protected override Task<TVector<ISecureValue>> HandleCoreAsync(IRequestInput input,
        RequestGetAllSecureValues obj)
    {
        throw new NotImplementedException();
    }
}