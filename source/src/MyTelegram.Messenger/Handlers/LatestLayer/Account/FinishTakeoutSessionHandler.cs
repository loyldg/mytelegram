namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Terminate a <a href="https://corefork.telegram.org/api/takeout">takeout session, see here » for more info</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 403 TAKEOUT_REQUIRED A <a href="https://corefork.telegram.org/api/takeout">takeout</a> session needs to be initialized first, <a href="https://corefork.telegram.org/api/takeout">see here » for more info</a>.
/// See <a href="https://corefork.telegram.org/method/account.finishTakeoutSession" />
///</summary>
internal sealed class FinishTakeoutSessionHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestFinishTakeoutSession, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestFinishTakeoutSession obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
