namespace MyTelegram.Messenger.Handlers;
/// <summary>
/// Official clients only: re-execute a method call that required reCAPTCHA verification via a <code>RECAPTCHA_CHECK_%s__%s</code>, where the first placeholder is the <code>action</code>, and the second one is the reCAPTCHA key ID.
/// <para><c>See <a href="https://corefork.telegram.org/method/invokeWithReCaptcha"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class InvokeWithReCaptchaHandler : RpcResultObjectHandler<MyTelegram.Schema.RequestInvokeWithReCaptcha, IObject>
{
    protected override Task<IObject> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.RequestInvokeWithReCaptcha obj)
    {
        throw new NotImplementedException();
    }
}