
namespace MyTelegram.Messenger.Handlers.LatestLayer.Communities;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class ToggleCommunityCollapsedInDialogsHandler : RpcResultObjectHandler<MyTelegram.Schema.Communities.RequestToggleCommunityCollapsedInDialogs, MyTelegram.Schema.IUpdates>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Communities.RequestToggleCommunityCollapsedInDialogs obj)
    {
        throw new NotImplementedException();
    }
}

