// ReSharper disable All

using MyTelegram.Schema.Auth;
using IAuthorization = MyTelegram.Schema.Auth.IAuthorization;

namespace MyTelegram.Handlers.Auth;

internal sealed class ImportWebTokenAuthorizationHandler :
    RpcResultObjectHandler<RequestImportWebTokenAuthorization, IAuthorization>,
    Auth.IImportWebTokenAuthorizationHandler
{
    protected override Task<IAuthorization> HandleCoreAsync(IRequestInput input,
        RequestImportWebTokenAuthorization obj)
    {
        throw new NotImplementedException();
    }
}