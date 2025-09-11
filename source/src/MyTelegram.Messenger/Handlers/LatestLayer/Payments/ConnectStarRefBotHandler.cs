namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/method/payments.connectStarRefBot" />
///</summary>
internal sealed class ConnectStarRefBotHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestConnectStarRefBot, MyTelegram.Schema.Payments.IConnectedStarRefBots>
{
    protected override Task<MyTelegram.Schema.Payments.IConnectedStarRefBots> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestConnectStarRefBot obj)
    {
        throw new NotImplementedException();
    }
}
