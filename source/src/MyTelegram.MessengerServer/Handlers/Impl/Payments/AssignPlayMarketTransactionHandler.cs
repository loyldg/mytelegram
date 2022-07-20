// ReSharper disable All

namespace MyTelegram.Handlers.Payments;

public class AssignPlayMarketTransactionHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestAssignPlayMarketTransaction, MyTelegram.Schema.IUpdates>,
    Payments.IAssignPlayMarketTransactionHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestAssignPlayMarketTransaction obj)
    {
        throw new NotImplementedException();
    }
}
