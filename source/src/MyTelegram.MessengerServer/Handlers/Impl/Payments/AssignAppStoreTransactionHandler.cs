// ReSharper disable All

namespace MyTelegram.Handlers.Payments;

public class AssignAppStoreTransactionHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestAssignAppStoreTransaction, MyTelegram.Schema.IUpdates>,
    Payments.IAssignAppStoreTransactionHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestAssignAppStoreTransaction obj)
    {
        throw new NotImplementedException();
    }
}
