// ReSharper disable All

using MyTelegram.Schema.Account;

namespace MyTelegram.Handlers.Account;

internal sealed class DeleteAutoSaveExceptionsHandler : RpcResultObjectHandler<RequestDeleteAutoSaveExceptions, IBool>,
    Account.IDeleteAutoSaveExceptionsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestDeleteAutoSaveExceptions obj)
    {
        throw new NotImplementedException();
    }
}
