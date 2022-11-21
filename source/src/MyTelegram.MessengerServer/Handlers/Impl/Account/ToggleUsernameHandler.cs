// ReSharper disable All

namespace MyTelegram.Handlers.Account;

public class ToggleUsernameHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestToggleUsername, IBool>,
    Account.IToggleUsernameHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestToggleUsername obj)
    {
        throw new NotImplementedException();
    }
}
