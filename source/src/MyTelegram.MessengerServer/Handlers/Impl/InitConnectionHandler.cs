namespace MyTelegram.MessengerServer.Handlers.Impl;

public class InitConnectionHandler : BaseObjectHandler<RequestInitConnection, IObject>,
    IInitConnectionHandler
{
    protected override Task<IObject> HandleCoreAsync(IRequestInput input,
        RequestInitConnection obj)
    {
        throw new NotImplementedException();
    }
}
