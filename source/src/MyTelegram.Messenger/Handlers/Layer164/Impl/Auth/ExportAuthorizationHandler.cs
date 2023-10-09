// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// Returns data for copying authorization to another data-center.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 DC_ID_INVALID The provided DC ID is invalid.
/// See <a href="https://corefork.telegram.org/method/auth.exportAuthorization" />
///</summary>
internal sealed class ExportAuthorizationHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestExportAuthorization, MyTelegram.Schema.Auth.IExportedAuthorization>,
    Auth.IExportAuthorizationHandler
{
    protected override Task<MyTelegram.Schema.Auth.IExportedAuthorization> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Auth.RequestExportAuthorization obj)
    {
        throw new NotImplementedException();
    }
}
