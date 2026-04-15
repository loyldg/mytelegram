
namespace MyTelegram.Messenger.Handlers.Bots;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class RequestWebViewButtonHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestRequestWebViewButton, MyTelegram.Schema.Bots.IRequestedButton>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Bots.IRequestedButton> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Bots.RequestRequestWebViewButton obj)
    {
        throw new NotImplementedException();
    }
}

