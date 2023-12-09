// ReSharper disable All

namespace MyTelegram.Handlers.Payments;

///<summary>
/// Informs server about a purchase made through the Play Store: for official applications only.
/// See <a href="https://corefork.telegram.org/method/payments.assignPlayMarketTransaction" />
///</summary>
internal sealed class AssignPlayMarketTransactionHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestAssignPlayMarketTransaction, MyTelegram.Schema.IUpdates>,
    Payments.IAssignPlayMarketTransactionHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestAssignPlayMarketTransaction obj)
    {
        throw new NotImplementedException();
    }
}
