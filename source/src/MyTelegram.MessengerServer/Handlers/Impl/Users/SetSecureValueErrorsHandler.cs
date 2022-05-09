using MyTelegram.Handlers.Users;
using MyTelegram.Schema.Users;

namespace MyTelegram.MessengerServer.Handlers.Impl.Users;

public class SetSecureValueErrorsHandler : RpcResultObjectHandler<RequestSetSecureValueErrors, IBool>,
    ISetSecureValueErrorsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSetSecureValueErrors obj)
    {
        throw new NotImplementedException();
    }
}
