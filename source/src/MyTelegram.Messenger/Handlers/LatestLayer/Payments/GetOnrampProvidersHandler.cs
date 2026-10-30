
namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetOnrampProvidersHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetOnrampProviders, MyTelegram.Schema.TVector<IOnrampProviderInfo>>
{
    protected override Task<MyTelegram.Schema.TVector<IOnrampProviderInfo>> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetOnrampProviders obj)
    {
        throw new NotImplementedException();
    }
}

