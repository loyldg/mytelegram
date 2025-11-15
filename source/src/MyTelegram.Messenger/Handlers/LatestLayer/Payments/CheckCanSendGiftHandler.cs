using MyTelegram.Schema.Payments;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Check if the specified <a href="https://corefork.telegram.org/api/gifts">gift »</a> can be sent.
/// Possible errors
/// Code Type Description
/// 400 STARGIFT_INVALID The passed gift is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.checkCanSendGift"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class CheckCanSendGiftHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestCheckCanSendGift, MyTelegram.Schema.Payments.ICheckCanSendGiftResult>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Payments.ICheckCanSendGiftResult> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestCheckCanSendGift obj)
    {
        return Task.FromResult<MyTelegram.Schema.Payments.ICheckCanSendGiftResult>(new TCheckCanSendGiftResultOk());
    }
}