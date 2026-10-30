
namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class RemoveUsernameHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestRemoveUsername, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Bots.RequestRemoveUsername obj)
    {
        throw new NotImplementedException();
    }
}

