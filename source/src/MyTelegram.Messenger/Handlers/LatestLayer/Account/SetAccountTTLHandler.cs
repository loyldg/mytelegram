namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Set account self-destruction period
/// Possible errors
/// Code Type Description
/// 400 TTL_DAYS_INVALID The provided TTL is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.setAccountTTL"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SetAccountTTLHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSetAccountTTL, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestSetAccountTTL obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}