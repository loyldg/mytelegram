using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;
using MyTelegram.Schema.Auth;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class SendChangePhoneCodeHandler : RpcResultObjectHandler<RequestSendChangePhoneCode, ISentCode>,
    ISendChangePhoneCodeHandler
{
    protected override Task<ISentCode> HandleCoreAsync(IRequestInput input,
        RequestSendChangePhoneCode obj)
    {
        throw new NotImplementedException();
    }
}
