// ReSharper disable All

namespace MyTelegram.Handlers.Payments;

///<summary>
/// Get info about a credit card
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BANK_CARD_NUMBER_INVALID The specified card number is invalid.
/// See <a href="https://corefork.telegram.org/method/payments.getBankCardData" />
///</summary>
internal sealed class GetBankCardDataHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetBankCardData, MyTelegram.Schema.Payments.IBankCardData>,
    Payments.IGetBankCardDataHandler
{
    protected override Task<MyTelegram.Schema.Payments.IBankCardData> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestGetBankCardData obj)
    {
        throw new NotImplementedException();
    }
}
