namespace MyTelegram.Messenger.Handlers;

///<summary>
/// See <a href="https://corefork.telegram.org/method/invokeWithReCaptcha" />
///</summary>
internal sealed class InvokeWithReCaptchaHandler : RpcResultObjectHandler<MyTelegram.Schema.RequestInvokeWithReCaptcha, IObject>
{
    protected override Task<IObject> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.RequestInvokeWithReCaptcha obj)
    {
        throw new NotImplementedException();
    }
}
