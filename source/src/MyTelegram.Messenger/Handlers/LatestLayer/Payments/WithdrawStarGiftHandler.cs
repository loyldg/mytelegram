
namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class WithdrawStarGiftHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestWithdrawStarGift, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestWithdrawStarGift obj)
    {
        throw new NotImplementedException();
    }
}

