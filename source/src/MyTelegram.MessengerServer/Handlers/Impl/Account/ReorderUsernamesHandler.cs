// ReSharper disable All

using MyTelegram.Schema.Account;

namespace MyTelegram.Handlers.Account;

public class ReorderUsernamesHandler : RpcResultObjectHandler<RequestReorderUsernames, IBool>,
    Account.IReorderUsernamesHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestReorderUsernames obj)
    {
        throw new NotImplementedException();
    }
}
