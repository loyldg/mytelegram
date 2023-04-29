// ReSharper disable All

using MyTelegram.Schema.Payments;

namespace MyTelegram.Handlers.Payments;

public class AssignAppStoreTransactionHandler :
    RpcResultObjectHandler<RequestAssignAppStoreTransaction, Schema.IUpdates>,
    Payments.IAssignAppStoreTransactionHandler
{
    protected override Task<Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        RequestAssignAppStoreTransaction obj)
    {
        throw new NotImplementedException();
    }
}
