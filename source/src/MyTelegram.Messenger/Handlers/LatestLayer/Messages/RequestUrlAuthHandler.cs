namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Get more info about a Seamless Telegram Login authorization request, for more info <a href="https://corefork.telegram.org/api/url-authorization">click here »</a>
/// See <a href="https://corefork.telegram.org/method/messages.requestUrlAuth" />
///</summary>
internal sealed class RequestUrlAuthHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestRequestUrlAuth, MyTelegram.Schema.IUrlAuthResult>
{
    protected override Task<MyTelegram.Schema.IUrlAuthResult> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestRequestUrlAuth obj)
    {
        throw new NotImplementedException();
    }
}
