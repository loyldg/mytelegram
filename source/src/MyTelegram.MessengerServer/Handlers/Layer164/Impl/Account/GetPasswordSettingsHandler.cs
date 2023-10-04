using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class GetPasswordSettingsHandler : RpcResultObjectHandler<RequestGetPasswordSettings, IPasswordSettings>,
    IGetPasswordSettingsHandler
{
    protected override Task<IPasswordSettings> HandleCoreAsync(IRequestInput input,
        RequestGetPasswordSettings obj)
    {
        throw new NotImplementedException();
    }
}