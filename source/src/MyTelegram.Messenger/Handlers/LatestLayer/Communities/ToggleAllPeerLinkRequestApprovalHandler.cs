
namespace MyTelegram.Messenger.Handlers.LatestLayer.Communities;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class ToggleAllPeerLinkRequestApprovalHandler : RpcResultObjectHandler<MyTelegram.Schema.Communities.RequestToggleAllPeerLinkRequestApproval, IBool>, IObjectHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Communities.RequestToggleAllPeerLinkRequestApproval obj)
    {
        throw new NotImplementedException();
    }
}

