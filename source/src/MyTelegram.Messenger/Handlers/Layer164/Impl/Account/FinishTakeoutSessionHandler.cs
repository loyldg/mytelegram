// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Finish account takeout session
/// <para>Possible errors</para>
/// Code Type Description
/// 403 TAKEOUT_REQUIRED A takeout session has to be initialized, first.
/// See <a href="https://corefork.telegram.org/method/account.finishTakeoutSession" />
///</summary>
internal sealed class FinishTakeoutSessionHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestFinishTakeoutSession, IBool>,
    Account.IFinishTakeoutSessionHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestFinishTakeoutSession obj)
    {
        throw new NotImplementedException();
    }
}
