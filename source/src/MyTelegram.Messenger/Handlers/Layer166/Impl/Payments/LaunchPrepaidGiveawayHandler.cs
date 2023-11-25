// ReSharper disable All

namespace MyTelegram.Handlers.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/method/payments.launchPrepaidGiveaway" />
///</summary>
internal sealed class LaunchPrepaidGiveawayHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestLaunchPrepaidGiveaway, MyTelegram.Schema.IUpdates>,
    Payments.ILaunchPrepaidGiveawayHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestLaunchPrepaidGiveaway obj)
    {
        throw new NotImplementedException();
    }
}
