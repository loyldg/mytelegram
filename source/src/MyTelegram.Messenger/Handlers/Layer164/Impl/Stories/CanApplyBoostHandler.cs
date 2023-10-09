// ReSharper disable All

namespace MyTelegram.Handlers.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.canApplyBoost" />
///</summary>
internal sealed class CanApplyBoostHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestCanApplyBoost, MyTelegram.Schema.Stories.ICanApplyBoostResult>,
    Stories.ICanApplyBoostHandler
{
    protected override Task<MyTelegram.Schema.Stories.ICanApplyBoostResult> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestCanApplyBoost obj)
    {
        throw new NotImplementedException();
    }
}
