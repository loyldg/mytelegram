
namespace MyTelegram.Messenger.Handlers.Bots;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetRequestedWebViewButtonHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestGetRequestedWebViewButton, MyTelegram.Schema.IKeyboardButton>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.IKeyboardButton> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Bots.RequestGetRequestedWebViewButton obj)
    {
        throw new NotImplementedException();
    }
}

