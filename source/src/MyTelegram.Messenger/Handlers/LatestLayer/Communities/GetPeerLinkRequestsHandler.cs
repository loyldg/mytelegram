
namespace MyTelegram.Messenger.Handlers.LatestLayer.Communities;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetPeerLinkRequestsHandler : RpcResultObjectHandler<MyTelegram.Schema.Communities.RequestGetPeerLinkRequests, MyTelegram.Schema.Communities.IPeerLinkRequests>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Communities.IPeerLinkRequests> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Communities.RequestGetPeerLinkRequests obj)
    {
        throw new NotImplementedException();
    }
}

