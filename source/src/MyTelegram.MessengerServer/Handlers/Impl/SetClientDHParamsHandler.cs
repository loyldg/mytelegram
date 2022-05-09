namespace MyTelegram.MessengerServer.Handlers.Impl;

public class SetClientDhParamsHandler : RpcResultObjectHandler<RequestSetClientDHParams, ISetClientDHParamsAnswer>,
    ISetClientDHParamsHandler
{
    protected override Task<ISetClientDHParamsAnswer> HandleCoreAsync(IRequestInput input,
        RequestSetClientDHParams obj)
    {
        throw new NotImplementedException();
    }
}
