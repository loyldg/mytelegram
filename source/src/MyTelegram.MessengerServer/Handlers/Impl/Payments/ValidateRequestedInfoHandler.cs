using MyTelegram.Handlers.Payments;
using MyTelegram.Schema.Payments;

namespace MyTelegram.MessengerServer.Handlers.Impl.Payments;

public class ValidateRequestedInfoHandler :
    RpcResultObjectHandler<RequestValidateRequestedInfo, IValidatedRequestedInfo>,
    IValidateRequestedInfoHandler
{
    protected override Task<IValidatedRequestedInfo> HandleCoreAsync(IRequestInput input,
        RequestValidateRequestedInfo obj)
    {
        throw new NotImplementedException();
    }
}