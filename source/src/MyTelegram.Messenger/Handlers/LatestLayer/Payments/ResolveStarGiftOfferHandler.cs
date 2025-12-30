
namespace MyTelegram.Messenger.Handlers.Payments;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class ResolveStarGiftOfferHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestResolveStarGiftOffer, MyTelegram.Schema.IUpdates>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestResolveStarGiftOffer obj)
    {
        throw new NotImplementedException();
    }
}

