// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Use this to accept a Seamless Telegram Login authorization request, for more info <a href="https://corefork.telegram.org/api/url-authorization">click here »</a>
/// See <a href="https://corefork.telegram.org/method/messages.acceptUrlAuth" />
///</summary>
internal sealed class AcceptUrlAuthHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestAcceptUrlAuth, MyTelegram.Schema.IUrlAuthResult>,
    Messages.IAcceptUrlAuthHandler
{
    protected override Task<MyTelegram.Schema.IUrlAuthResult> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestAcceptUrlAuth obj)
    {
        throw new NotImplementedException();
    }
}
