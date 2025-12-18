namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Sends a Telegram Passport authorization form, effectively sharing data with the service
/// Possible errors
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// 400 PUBLIC_KEY_REQUIRED A public key is required.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.acceptAuthorization"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class AcceptAuthorizationHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestAcceptAuthorization, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestAcceptAuthorization obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}