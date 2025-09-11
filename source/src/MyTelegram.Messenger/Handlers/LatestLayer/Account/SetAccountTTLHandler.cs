namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Set account self-destruction period
/// <para>Possible errors</para>
/// Code Type Description
/// 400 TTL_DAYS_INVALID The provided TTL is invalid.
/// See <a href="https://corefork.telegram.org/method/account.setAccountTTL" />
///</summary>
internal sealed class SetAccountTTLHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSetAccountTTL, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestSetAccountTTL obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
