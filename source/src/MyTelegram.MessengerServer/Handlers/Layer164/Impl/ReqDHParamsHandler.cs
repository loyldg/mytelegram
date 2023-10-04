namespace MyTelegram.MessengerServer.Handlers.Impl;

public class ReqDhParamsHandler : BaseObjectHandler<RequestReqDHParams, IServerDHParams>,
    IReqDHParamsHandler
{
    protected override Task<IServerDHParams> HandleCoreAsync(IRequestInput input,
        RequestReqDHParams obj)
    {
        throw new NotImplementedException();
    }
}