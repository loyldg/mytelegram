// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// Generate a login token, for <a href="https://corefork.telegram.org/api/qr-login">login via QR code</a>.<br>
/// The generated login token should be encoded using base64url, then shown as a <code>tg://login?token=base64encodedtoken</code> <a href="https://corefork.telegram.org/api/links#qr-code-login-links">deep link »</a> in the QR code.For more info, see <a href="https://corefork.telegram.org/api/qr-login">login via QR code</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 API_ID_INVALID API ID invalid.
/// 400 API_ID_PUBLISHED_FLOOD This API id was published somewhere, you can't use it now.
/// See <a href="https://corefork.telegram.org/method/auth.exportLoginToken" />
///</summary>
internal sealed class ExportLoginTokenHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestExportLoginToken, MyTelegram.Schema.Auth.ILoginToken>,
    Auth.IExportLoginTokenHandler
{
    protected override Task<MyTelegram.Schema.Auth.ILoginToken> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Auth.RequestExportLoginToken obj)
    {
        throw new NotImplementedException();
    }
}
