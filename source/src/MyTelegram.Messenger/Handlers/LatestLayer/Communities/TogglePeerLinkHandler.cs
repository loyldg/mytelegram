
namespace MyTelegram.Messenger.Handlers.LatestLayer.Communities;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class TogglePeerLinkHandler : RpcResultObjectHandler<MyTelegram.Schema.Communities.RequestTogglePeerLink, IBool>, IObjectHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Communities.RequestTogglePeerLink obj)
    {
        throw new NotImplementedException();
    }
}

