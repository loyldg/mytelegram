namespace MyTelegram.MessengerServer.Handlers.Impl;

public class InvokeWithTakeoutHandler : BaseObjectHandler<RequestInvokeWithTakeout, IObject>,
    IInvokeWithTakeoutHandler
{
    protected override Task<IObject> HandleCoreAsync(IRequestInput input,
        RequestInvokeWithTakeout obj)
    {
        throw new NotImplementedException();
    }
}