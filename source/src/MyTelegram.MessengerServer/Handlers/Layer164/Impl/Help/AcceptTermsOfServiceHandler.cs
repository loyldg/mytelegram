using MyTelegram.Handlers.Help;

namespace MyTelegram.MessengerServer.Handlers.Impl.Help;

public class AcceptTermsOfServiceHandler : RpcResultObjectHandler<RequestAcceptTermsOfService, IBool>,
    IAcceptTermsOfServiceHandler, IProcessedHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestAcceptTermsOfService obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}