
namespace MyTelegram.Messenger.Handlers.Payments;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class SendStarGiftOfferHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestSendStarGiftOffer, MyTelegram.Schema.IUpdates>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestSendStarGiftOffer obj)
    {
        throw new NotImplementedException();
    }
}

