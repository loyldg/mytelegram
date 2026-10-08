
namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class RequestChatJoinWebViewHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestRequestChatJoinWebView, MyTelegram.Schema.IWebViewResult>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.IWebViewResult> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestRequestChatJoinWebView obj)
    {
        throw new NotImplementedException();
    }
}

