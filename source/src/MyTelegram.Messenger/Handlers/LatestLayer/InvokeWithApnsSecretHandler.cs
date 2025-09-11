namespace MyTelegram.Messenger.Handlers;

///<summary>
/// Official clients only, invoke with Apple push verification.
/// See <a href="https://corefork.telegram.org/method/invokeWithApnsSecret" />
///</summary>
internal sealed class InvokeWithApnsSecretHandler : RpcResultObjectHandler<MyTelegram.Schema.RequestInvokeWithApnsSecret, IObject>
{
    protected override Task<IObject> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.RequestInvokeWithApnsSecret obj)
    {
        throw new NotImplementedException();
    }
}
