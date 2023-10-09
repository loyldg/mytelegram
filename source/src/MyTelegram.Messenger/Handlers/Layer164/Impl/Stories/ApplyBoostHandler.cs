// ReSharper disable All

namespace MyTelegram.Handlers.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.applyBoost" />
///</summary>
internal sealed class ApplyBoostHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestApplyBoost, IBool>,
    Stories.IApplyBoostHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestApplyBoost obj)
    {
        throw new NotImplementedException();
    }
}
