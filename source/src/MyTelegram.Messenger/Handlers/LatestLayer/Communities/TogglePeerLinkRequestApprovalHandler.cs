
namespace MyTelegram.Messenger.Handlers.LatestLayer.Communities;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class TogglePeerLinkRequestApprovalHandler : RpcResultObjectHandler<MyTelegram.Schema.Communities.RequestTogglePeerLinkRequestApproval, IBool>, IObjectHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Communities.RequestTogglePeerLinkRequestApproval obj)
    {
        throw new NotImplementedException();
    }
}

