// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// Login using a redirected login token, generated in case of DC mismatch during <a href="https://corefork.telegram.org/api/qr-login">QR code login</a>.For more info, see <a href="https://corefork.telegram.org/api/qr-login">login via QR code</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 AUTH_TOKEN_ALREADY_ACCEPTED The specified auth token was already accepted.
/// 400 AUTH_TOKEN_EXPIRED The authorization token has expired.
/// 400 AUTH_TOKEN_INVALID The specified auth token is invalid.
/// 400 AUTH_TOKEN_INVALIDX The specified auth token is invalid.
/// See <a href="https://corefork.telegram.org/method/auth.importLoginToken" />
///</summary>
internal sealed class ImportLoginTokenHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestImportLoginToken, MyTelegram.Schema.Auth.ILoginToken>,
    Auth.IImportLoginTokenHandler
{
    protected override Task<MyTelegram.Schema.Auth.ILoginToken> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Auth.RequestImportLoginToken obj)
    {
        throw new NotImplementedException();
    }
}
