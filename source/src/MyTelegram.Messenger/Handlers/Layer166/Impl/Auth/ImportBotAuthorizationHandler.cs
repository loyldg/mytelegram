// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// Login as a bot
/// <para>Possible errors</para>
/// Code Type Description
/// 400 ACCESS_TOKEN_EXPIRED Access token expired.
/// 400 ACCESS_TOKEN_INVALID Access token invalid.
/// 400 API_ID_INVALID API ID invalid.
/// 400 API_ID_PUBLISHED_FLOOD This API id was published somewhere, you can't use it now.
/// See <a href="https://corefork.telegram.org/method/auth.importBotAuthorization" />
///</summary>
internal sealed class ImportBotAuthorizationHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestImportBotAuthorization, MyTelegram.Schema.Auth.IAuthorization>,
    Auth.IImportBotAuthorizationHandler
{
    protected override Task<MyTelegram.Schema.Auth.IAuthorization> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Auth.RequestImportBotAuthorization obj)
    {
        throw new NotImplementedException();
    }
}
