
namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class DeleteStarGiftHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestDeleteStarGift, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestDeleteStarGift obj)
    {
        return new TBoolTrue();
    }
}

