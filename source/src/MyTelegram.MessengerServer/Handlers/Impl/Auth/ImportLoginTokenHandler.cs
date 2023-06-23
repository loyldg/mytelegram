using MyTelegram.Handlers.Auth;
using MyTelegram.Schema.Auth;

namespace MyTelegram.MessengerServer.Handlers.Impl.Auth;

public class ImportLoginTokenHandler : RpcResultObjectHandler<RequestImportLoginToken, ILoginToken>,
    IImportLoginTokenHandler
{
    protected override Task<ILoginToken> HandleCoreAsync(IRequestInput input,
        RequestImportLoginToken obj)
    {
        throw new NotImplementedException();
    }
}