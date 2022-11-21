// ReSharper disable All

namespace MyTelegram.Handlers.Account;

public class ReorderUsernamesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestReorderUsernames, IBool>,
    Account.IReorderUsernamesHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestReorderUsernames obj)
    {
        throw new NotImplementedException();
    }
}
