namespace MyTelegram.MessengerServer.Handlers.Impl;

public class ReqPqHandler : BaseObjectHandler<RequestReqPq, IResPQ>,
    IReqPqHandler
{
    protected override Task<IResPQ> HandleCoreAsync(IRequestInput input,
        RequestReqPq obj)
    {
        throw new NotImplementedException();
    }
}
