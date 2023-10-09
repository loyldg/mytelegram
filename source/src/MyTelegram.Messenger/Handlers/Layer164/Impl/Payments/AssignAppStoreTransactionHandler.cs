// ReSharper disable All

namespace MyTelegram.Handlers.Payments;

///<summary>
/// Informs server about a purchase made through the App Store: for official applications only.
/// See <a href="https://corefork.telegram.org/method/payments.assignAppStoreTransaction" />
///</summary>
internal sealed class AssignAppStoreTransactionHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestAssignAppStoreTransaction, MyTelegram.Schema.IUpdates>,
    Payments.IAssignAppStoreTransactionHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestAssignAppStoreTransaction obj)
    {
        throw new NotImplementedException();
    }
}
