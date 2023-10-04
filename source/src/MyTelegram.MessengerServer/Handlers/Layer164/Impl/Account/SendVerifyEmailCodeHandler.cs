using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class SendVerifyEmailCodeHandler : RpcResultObjectHandler<RequestSendVerifyEmailCode, ISentEmailCode>,
    ISendVerifyEmailCodeHandler
{
    protected override Task<ISentEmailCode> HandleCoreAsync(IRequestInput input,
        RequestSendVerifyEmailCode obj)
    {
        throw new NotImplementedException();
    }
}