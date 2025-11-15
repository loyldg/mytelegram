namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;
/// <summary>
/// Login by importing an authorization token
/// Possible errors
/// Code Type Description
/// 400 API_ID_INVALID API ID invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/auth.importWebTokenAuthorization"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✔]
/// </remarks>
internal sealed class ImportWebTokenAuthorizationHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestImportWebTokenAuthorization, MyTelegram.Schema.Auth.IAuthorization>
{
    protected override Task<MyTelegram.Schema.Auth.IAuthorization> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Auth.RequestImportWebTokenAuthorization obj)
    {
        throw new NotImplementedException();
    }
}