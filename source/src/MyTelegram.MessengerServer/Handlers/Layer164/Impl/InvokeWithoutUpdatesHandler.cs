namespace MyTelegram.MessengerServer.Handlers.Impl;

public class InvokeWithoutUpdatesHandler : BaseObjectHandler<RequestInvokeWithoutUpdates, IObject>,
    IInvokeWithoutUpdatesHandler
{
    protected override Task<IObject> HandleCoreAsync(IRequestInput input,
        RequestInvokeWithoutUpdates obj)
    {
        throw new NotImplementedException();
    }
}