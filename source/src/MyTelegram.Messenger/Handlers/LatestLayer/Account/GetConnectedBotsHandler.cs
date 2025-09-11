namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// List all currently connected <a href="https://corefork.telegram.org/api/business#connected-bots">business bots »</a>
/// See <a href="https://corefork.telegram.org/method/account.getConnectedBots" />
///</summary>
internal sealed class GetConnectedBotsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetConnectedBots, MyTelegram.Schema.Account.IConnectedBots>
{
    protected override Task<MyTelegram.Schema.Account.IConnectedBots> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetConnectedBots obj)
    {
        return Task.FromResult<MyTelegram.Schema.Account.IConnectedBots>(new TConnectedBots
        {
            ConnectedBots = [],
            Users = []
        });
    }
}
