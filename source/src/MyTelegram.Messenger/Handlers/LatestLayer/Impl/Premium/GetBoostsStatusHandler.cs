// ReSharper disable All

using MyTelegram.Schema.Premium;

namespace MyTelegram.Handlers.Premium;

///<summary>
/// See <a href="https://corefork.telegram.org/method/premium.getBoostsStatus" />
///</summary>
internal sealed class GetBoostsStatusHandler : RpcResultObjectHandler<MyTelegram.Schema.Premium.RequestGetBoostsStatus, MyTelegram.Schema.Premium.IBoostsStatus>,
    Premium.IGetBoostsStatusHandler
{
    protected override Task<MyTelegram.Schema.Premium.IBoostsStatus> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Premium.RequestGetBoostsStatus obj)
    {
        var boostsStatus = new TBoostsStatus
        {
            MyBoost = false,
            Level = 5,
            CurrentLevelBoosts = 5,
            NextLevelBoosts = 6,
            BoostUrl = "https://t.me/"
        };

        return Task.FromResult<IBoostsStatus>(boostsStatus);
    }
}
