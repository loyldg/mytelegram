namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/method/payments.getConnectedStarRefBot" />
///</summary>
internal sealed class GetConnectedStarRefBotHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetConnectedStarRefBot, MyTelegram.Schema.Payments.IConnectedStarRefBots>
{
    protected override Task<MyTelegram.Schema.Payments.IConnectedStarRefBots> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestGetConnectedStarRefBot obj)
    {
        throw new NotImplementedException();
    }
}
