// ReSharper disable All

namespace MyTelegram.Handlers.Account;

internal sealed class DeleteAutoSaveExceptionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestDeleteAutoSaveExceptions, IBool>,
    Account.IDeleteAutoSaveExceptionsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestDeleteAutoSaveExceptions obj)
    {
        throw new NotImplementedException();
    }
}
