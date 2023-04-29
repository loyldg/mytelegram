// ReSharper disable All

using MyTelegram.Schema.Payments;

namespace MyTelegram.Handlers.Payments;

public class AssignPlayMarketTransactionHandler :
    RpcResultObjectHandler<RequestAssignPlayMarketTransaction, Schema.IUpdates>,
    Payments.IAssignPlayMarketTransactionHandler
{
    protected override Task<Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        RequestAssignPlayMarketTransaction obj)
    {
        throw new NotImplementedException();
    }
}
