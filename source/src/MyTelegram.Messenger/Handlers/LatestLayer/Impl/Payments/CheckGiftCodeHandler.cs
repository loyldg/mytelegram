// ReSharper disable All

namespace MyTelegram.Handlers.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/method/payments.checkGiftCode" />
///</summary>
internal sealed class CheckGiftCodeHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestCheckGiftCode, MyTelegram.Schema.Payments.ICheckedGiftCode>,
    Payments.ICheckGiftCodeHandler
{
    protected override Task<MyTelegram.Schema.Payments.ICheckedGiftCode> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestCheckGiftCode obj)
    {
        throw new NotImplementedException();
    }
}
