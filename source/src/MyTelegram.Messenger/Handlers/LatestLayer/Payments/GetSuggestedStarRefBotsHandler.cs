namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/method/payments.getSuggestedStarRefBots" />
///</summary>
internal sealed class GetSuggestedStarRefBotsHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetSuggestedStarRefBots, MyTelegram.Schema.Payments.ISuggestedStarRefBots>
{
    protected override Task<MyTelegram.Schema.Payments.ISuggestedStarRefBots> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestGetSuggestedStarRefBots obj)
    {
        throw new NotImplementedException();
    }
}
