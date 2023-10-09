// ReSharper disable All

namespace MyTelegram.Handlers.Payments;

///<summary>
/// Clear saved payment information
/// See <a href="https://corefork.telegram.org/method/payments.clearSavedInfo" />
///</summary>
internal sealed class ClearSavedInfoHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestClearSavedInfo, IBool>,
    Payments.IClearSavedInfoHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestClearSavedInfo obj)
    {
        throw new NotImplementedException();
    }
}
