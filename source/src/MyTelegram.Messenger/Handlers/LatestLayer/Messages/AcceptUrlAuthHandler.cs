namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Use this to accept a Seamless Telegram Login authorization request, for more info <a href="https://corefork.telegram.org/api/url-authorization">click here »</a>
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.acceptUrlAuth"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class AcceptUrlAuthHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestAcceptUrlAuth, MyTelegram.Schema.IUrlAuthResult>
{
    protected override Task<MyTelegram.Schema.IUrlAuthResult> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestAcceptUrlAuth obj)
    {
        throw new NotImplementedException();
    }
}