namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// Informs server about a purchase made through the Play Store: for official applications only.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 DATA_JSON_INVALID The provided JSON data is invalid.
/// See <a href="https://corefork.telegram.org/method/payments.assignPlayMarketTransaction" />
///</summary>
internal sealed class AssignPlayMarketTransactionHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestAssignPlayMarketTransaction, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestAssignPlayMarketTransaction obj)
    {
        throw new NotImplementedException();
    }
}
