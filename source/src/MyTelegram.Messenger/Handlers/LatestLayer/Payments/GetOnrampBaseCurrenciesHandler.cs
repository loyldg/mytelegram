
namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetOnrampBaseCurrenciesHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetOnrampBaseCurrencies, MyTelegram.Schema.TVector<string>>
{
    protected override Task<MyTelegram.Schema.TVector<string>> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetOnrampBaseCurrencies obj)
    {
        throw new NotImplementedException();
    }
}

