namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;
/// <summary>
/// Login as a bot
/// Possible errors
/// Code Type Description
/// 400 ACCESS_TOKEN_EXPIRED Access token expired.
/// 400 ACCESS_TOKEN_INVALID Access token invalid.
/// 400 API_ID_INVALID API ID invalid.
/// 400 API_ID_PUBLISHED_FLOOD This API id was published somewhere, you can't use it now.
/// <para><c>See <a href="https://corefork.telegram.org/method/auth.importBotAuthorization"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✔]
/// </remarks>
internal sealed class ImportBotAuthorizationHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestImportBotAuthorization, MyTelegram.Schema.Auth.IAuthorization>
{
    protected override Task<MyTelegram.Schema.Auth.IAuthorization> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Auth.RequestImportBotAuthorization obj)
    {
        throw new NotImplementedException();
    }
}