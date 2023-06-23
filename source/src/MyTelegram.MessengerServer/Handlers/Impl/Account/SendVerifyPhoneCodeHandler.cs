using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;
using MyTelegram.Schema.Auth;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class SendVerifyPhoneCodeHandler : RpcResultObjectHandler<RequestSendVerifyPhoneCode, ISentCode>,
    ISendVerifyPhoneCodeHandler
{
    protected override Task<ISentCode> HandleCoreAsync(IRequestInput input,
        RequestSendVerifyPhoneCode obj)
    {
        throw new NotImplementedException();
    }
}