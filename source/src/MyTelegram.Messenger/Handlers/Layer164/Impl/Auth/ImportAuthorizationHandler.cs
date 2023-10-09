// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// Logs in a user using a key transmitted from his native data-center.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 AUTH_BYTES_INVALID The provided authorization is invalid.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// See <a href="https://corefork.telegram.org/method/auth.importAuthorization" />
///</summary>
internal sealed class ImportAuthorizationHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestImportAuthorization, MyTelegram.Schema.Auth.IAuthorization>,
    Auth.IImportAuthorizationHandler
{
    protected override Task<MyTelegram.Schema.Auth.IAuthorization> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Auth.RequestImportAuthorization obj)
    {
        throw new NotImplementedException();
    }
}
