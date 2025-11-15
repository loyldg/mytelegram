namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// List all currently connected <a href="https://corefork.telegram.org/api/bots/connected-business-bots">business bots »</a>
/// <para><c>See <a href="https://corefork.telegram.org/method/account.getConnectedBots"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetConnectedBotsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetConnectedBots, MyTelegram.Schema.Account.IConnectedBots>
{
    protected override Task<MyTelegram.Schema.Account.IConnectedBots> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestGetConnectedBots obj)
    {
        return Task.FromResult<MyTelegram.Schema.Account.IConnectedBots>(new TConnectedBots { ConnectedBots = [], Users = [] });
    }
}