namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/method/payments.editConnectedStarRefBot" />
///</summary>
internal sealed class EditConnectedStarRefBotHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestEditConnectedStarRefBot, MyTelegram.Schema.Payments.IConnectedStarRefBots>
{
    protected override Task<MyTelegram.Schema.Payments.IConnectedStarRefBots> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestEditConnectedStarRefBot obj)
    {
        throw new NotImplementedException();
    }
}
