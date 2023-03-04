// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

internal sealed class ImportWebTokenAuthorizationHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestImportWebTokenAuthorization, MyTelegram.Schema.Auth.IAuthorization>,
    Auth.IImportWebTokenAuthorizationHandler
{
    protected override Task<MyTelegram.Schema.Auth.IAuthorization> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Auth.RequestImportWebTokenAuthorization obj)
    {
        throw new NotImplementedException();
    }
}
