namespace MyTelegram.MessengerServer.Handlers.Impl;

public class InvokeWithMessagesRangeHandler : BaseObjectHandler<RequestInvokeWithMessagesRange, IObject>,
    IInvokeWithMessagesRangeHandler
{
    protected override Task<IObject> HandleCoreAsync(IRequestInput input,
        RequestInvokeWithMessagesRange obj)
    {
        throw new NotImplementedException();
    }
}
