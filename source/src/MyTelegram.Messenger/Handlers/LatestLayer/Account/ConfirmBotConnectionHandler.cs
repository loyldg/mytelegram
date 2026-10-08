
namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class ConfirmBotConnectionHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestConfirmBotConnection, IBool>, IObjectHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestConfirmBotConnection obj)
    {
        throw new NotImplementedException();
    }
}

