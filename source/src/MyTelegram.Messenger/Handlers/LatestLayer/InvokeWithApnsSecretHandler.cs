namespace MyTelegram.Messenger.Handlers;
/// <summary>
/// Official clients only, invoke with Apple push verification.
/// <para><c>See <a href="https://corefork.telegram.org/method/invokeWithApnsSecret"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class InvokeWithApnsSecretHandler : RpcResultObjectHandler<MyTelegram.Schema.RequestInvokeWithApnsSecret, IObject>
{
    protected override Task<IObject> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.RequestInvokeWithApnsSecret obj)
    {
        throw new NotImplementedException();
    }
}