// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Initialize account takeout session
/// <para>Possible errors</para>
/// Code Type Description
/// 420 TAKEOUT_INIT_DELAY_%d Wait %d seconds before initializing takeout.
/// See <a href="https://corefork.telegram.org/method/account.initTakeoutSession" />
///</summary>
internal sealed class InitTakeoutSessionHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestInitTakeoutSession, MyTelegram.Schema.Account.ITakeout>,
    Account.IInitTakeoutSessionHandler
{
    protected override Task<MyTelegram.Schema.Account.ITakeout> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestInitTakeoutSession obj)
    {
        throw new NotImplementedException();
    }
}
