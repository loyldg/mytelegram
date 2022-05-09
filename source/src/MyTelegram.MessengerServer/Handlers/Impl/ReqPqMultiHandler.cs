namespace MyTelegram.MessengerServer.Handlers.Impl;

public class ReqPqMultiHandler : BaseObjectHandler<RequestReqPqMulti, IResPQ>,
    IReqPqMultiHandler
{
    protected override Task<IResPQ> HandleCoreAsync(IRequestInput input,
        RequestReqPqMulti obj)
    {
        throw new NotImplementedException();
    }
}
