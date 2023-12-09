// ReSharper disable All

namespace MyTelegram.Handlers.Payments;

///<summary>
/// Get saved payment information
/// See <a href="https://corefork.telegram.org/method/payments.getSavedInfo" />
///</summary>
internal sealed class GetSavedInfoHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetSavedInfo, MyTelegram.Schema.Payments.ISavedInfo>,
    Payments.IGetSavedInfoHandler
{
    protected override Task<MyTelegram.Schema.Payments.ISavedInfo> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestGetSavedInfo obj)
    {
        throw new NotImplementedException();
    }
}
