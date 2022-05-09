namespace MyTelegram.MessengerServer.Handlers.Impl;

public class InvokeAfterMsgsHandler : BaseObjectHandler<RequestInvokeAfterMsgs, IObject>,
    IInvokeAfterMsgsHandler
{
    protected override Task<IObject> HandleCoreAsync(IRequestInput input,
        RequestInvokeAfterMsgs obj)
    {
        throw new NotImplementedException();
    }
}
