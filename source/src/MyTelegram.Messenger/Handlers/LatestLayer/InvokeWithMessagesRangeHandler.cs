namespace MyTelegram.Messenger.Handlers;

///<summary>
/// Invoke with the given message range
/// See <a href="https://corefork.telegram.org/method/invokeWithMessagesRange" />
///</summary>
internal sealed class InvokeWithMessagesRangeHandler : BaseObjectHandler<MyTelegram.Schema.RequestInvokeWithMessagesRange, IObject>
{
    protected override Task<IObject> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.RequestInvokeWithMessagesRange obj)
    {
        throw new NotImplementedException();
    }
}
