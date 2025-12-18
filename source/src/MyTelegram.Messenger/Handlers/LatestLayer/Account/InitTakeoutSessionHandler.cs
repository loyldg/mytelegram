namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Initialize a <a href="https://corefork.telegram.org/api/takeout">takeout session, see here » for more info</a>.
/// Possible errors
/// Code Type Description
/// 420 TAKEOUT_INIT_DELAY_%d Sorry, for security reasons, you will be able to begin downloading your data in %d seconds. We have notified all your devices about the export request to make sure it's authorized and to give you time to react if it's not.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.initTakeoutSession"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class InitTakeoutSessionHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestInitTakeoutSession, MyTelegram.Schema.Account.ITakeout>
{
    protected override Task<MyTelegram.Schema.Account.ITakeout> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestInitTakeoutSession obj)
    {
        throw new NotImplementedException();
    }
}