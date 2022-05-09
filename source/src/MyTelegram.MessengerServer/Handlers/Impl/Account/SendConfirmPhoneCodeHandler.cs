using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;
using MyTelegram.Schema.Auth;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class SendConfirmPhoneCodeHandler : RpcResultObjectHandler<RequestSendConfirmPhoneCode, ISentCode>,
    ISendConfirmPhoneCodeHandler
{
    protected override Task<ISentCode> HandleCoreAsync(IRequestInput input,
        RequestSendConfirmPhoneCode obj)
    {
        throw new NotImplementedException();
    }
}
