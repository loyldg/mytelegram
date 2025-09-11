namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;

///<summary>
/// Fetch popular <a href="https://corefork.telegram.org/api/bots/webapps#main-mini-apps">Main Mini Apps</a>, to be used in the <a href="https://corefork.telegram.org/api/search#apps-tab">apps tab of global search »</a>.
/// See <a href="https://corefork.telegram.org/method/bots.getPopularAppBots" />
///</summary>
internal sealed class GetPopularAppBotsHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestGetPopularAppBots, MyTelegram.Schema.Bots.IPopularAppBots>
{
    protected override Task<MyTelegram.Schema.Bots.IPopularAppBots> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Bots.RequestGetPopularAppBots obj)
    {
        throw new NotImplementedException();
    }
}
