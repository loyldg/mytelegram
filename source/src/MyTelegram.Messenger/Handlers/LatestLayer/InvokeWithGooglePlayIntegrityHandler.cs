namespace MyTelegram.Messenger.Handlers;
/// <summary>
/// Official clients only, invoke with Google Play Integrity token.
/// <para><c>See <a href="https://corefork.telegram.org/method/invokeWithGooglePlayIntegrity"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class InvokeWithGooglePlayIntegrityHandler : RpcResultObjectHandler<MyTelegram.Schema.RequestInvokeWithGooglePlayIntegrity, IObject>
{
    protected override Task<IObject> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.RequestInvokeWithGooglePlayIntegrity obj)
    {
        throw new NotImplementedException();
    }
}