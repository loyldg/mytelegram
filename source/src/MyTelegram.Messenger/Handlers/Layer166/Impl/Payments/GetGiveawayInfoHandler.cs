// ReSharper disable All

namespace MyTelegram.Handlers.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/method/payments.getGiveawayInfo" />
///</summary>
internal sealed class GetGiveawayInfoHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetGiveawayInfo, MyTelegram.Schema.Payments.IGiveawayInfo>,
    Payments.IGetGiveawayInfoHandler
{
    protected override Task<MyTelegram.Schema.Payments.IGiveawayInfo> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestGetGiveawayInfo obj)
    {
        throw new NotImplementedException();
    }
}
