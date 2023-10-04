using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class GetSecureValueHandler : RpcResultObjectHandler<RequestGetSecureValue, TVector<ISecureValue>>,
    IGetSecureValueHandler
{
    protected override Task<TVector<ISecureValue>> HandleCoreAsync(IRequestInput input,
        RequestGetSecureValue obj)
    {
        throw new NotImplementedException();
    }
}