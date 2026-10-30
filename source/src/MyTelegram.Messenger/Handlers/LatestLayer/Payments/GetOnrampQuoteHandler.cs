
namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetOnrampQuoteHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetOnrampQuote, MyTelegram.Schema.IOnrampQuote>
{
    protected override Task<MyTelegram.Schema.IOnrampQuote> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetOnrampQuote obj)
    {
        throw new NotImplementedException();
    }
}

