namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Returns a Telegram Passport authorization form for sharing data with a service
/// Possible errors
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// 400 PUBLIC_KEY_REQUIRED A public key is required.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.getAuthorizationForm"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetAuthorizationFormHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetAuthorizationForm, MyTelegram.Schema.Account.IAuthorizationForm>
{
    protected override Task<MyTelegram.Schema.Account.IAuthorizationForm> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestGetAuthorizationForm obj)
    {
        throw new NotImplementedException();
    }
}