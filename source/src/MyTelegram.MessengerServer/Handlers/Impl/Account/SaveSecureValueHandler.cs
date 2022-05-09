using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class SaveSecureValueHandler : RpcResultObjectHandler<RequestSaveSecureValue, ISecureValue>,
    ISaveSecureValueHandler
{
    protected override Task<ISecureValue> HandleCoreAsync(IRequestInput input,
        RequestSaveSecureValue obj)
    {
        throw new NotImplementedException();
    }
}
