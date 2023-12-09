// ReSharper disable All

namespace MyTelegram.Handlers.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/method/payments.applyGiftCode" />
///</summary>
internal sealed class ApplyGiftCodeHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestApplyGiftCode, MyTelegram.Schema.IUpdates>,
    Payments.IApplyGiftCodeHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestApplyGiftCode obj)
    {
        throw new NotImplementedException();
    }
}
