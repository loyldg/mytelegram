
namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class DepositStarGiftHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestDepositStarGift, MyTelegram.Schema.Payments.IStarGiftDepositPayload>
{
    protected override Task<MyTelegram.Schema.Payments.IStarGiftDepositPayload> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestDepositStarGift obj)
    {
        throw new NotImplementedException();
    }
}

