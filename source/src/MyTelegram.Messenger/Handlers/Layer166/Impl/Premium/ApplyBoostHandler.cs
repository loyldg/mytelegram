// ReSharper disable All

namespace MyTelegram.Handlers.Premium;

///<summary>
/// See <a href="https://corefork.telegram.org/method/premium.applyBoost" />
///</summary>
internal sealed class ApplyBoostHandler : RpcResultObjectHandler<MyTelegram.Schema.Premium.RequestApplyBoost, MyTelegram.Schema.Premium.IMyBoosts>,
    Premium.IApplyBoostHandler
{
    protected override Task<MyTelegram.Schema.Premium.IMyBoosts> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Premium.RequestApplyBoost obj)
    {
        throw new NotImplementedException();
    }
}
