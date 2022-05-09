using MyTelegram.Handlers.Auth;
using MyTelegram.Schema.Auth;
using IAuthorization = MyTelegram.Schema.Auth.IAuthorization;

namespace MyTelegram.MessengerServer.Handlers.Impl.Auth;

public class ImportBotAuthorizationHandler : RpcResultObjectHandler<RequestImportBotAuthorization, IAuthorization>,
    IImportBotAuthorizationHandler
{
    protected override Task<IAuthorization> HandleCoreAsync(IRequestInput input,
        RequestImportBotAuthorization obj)
    {
        throw new NotImplementedException();
    }
}
