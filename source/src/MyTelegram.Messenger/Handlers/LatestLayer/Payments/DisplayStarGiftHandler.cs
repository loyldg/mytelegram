
namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class DisplayStarGiftHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestDisplayStarGift, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestDisplayStarGift obj)
    {
        throw new NotImplementedException();
    }
}

