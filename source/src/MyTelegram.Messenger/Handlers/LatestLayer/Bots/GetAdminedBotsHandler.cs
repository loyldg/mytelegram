namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;

///<summary>
/// See <a href="https://corefork.telegram.org/method/bots.getAdminedBots" />
///</summary>
internal sealed class GetAdminedBotsHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestGetAdminedBots, TVector<MyTelegram.Schema.IUser>>
{
    protected override Task<TVector<MyTelegram.Schema.IUser>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Bots.RequestGetAdminedBots obj)
    {
        throw new NotImplementedException();
    }
}
