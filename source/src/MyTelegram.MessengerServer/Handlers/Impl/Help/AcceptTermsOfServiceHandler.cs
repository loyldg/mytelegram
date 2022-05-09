using MyTelegram.Handlers.Help;
using MyTelegram.Schema.Help;

namespace MyTelegram.MessengerServer.Handlers.Impl.Help;

public class AcceptTermsOfServiceHandler : RpcResultObjectHandler<RequestAcceptTermsOfService, IBool>,
    IAcceptTermsOfServiceHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestAcceptTermsOfService obj)
    {
        throw new NotImplementedException();
    }
}
