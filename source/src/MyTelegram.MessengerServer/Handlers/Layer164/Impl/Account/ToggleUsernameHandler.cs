// ReSharper disable All

using MyTelegram.Schema.Account;
using RequestToggleUsername = MyTelegram.Schema.Account.RequestToggleUsername;

namespace MyTelegram.Handlers.Account;

public class ToggleUsernameHandler : RpcResultObjectHandler<RequestToggleUsername, IBool>,
    Account.IToggleUsernameHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestToggleUsername obj)
    {
        throw new NotImplementedException();
    }
}