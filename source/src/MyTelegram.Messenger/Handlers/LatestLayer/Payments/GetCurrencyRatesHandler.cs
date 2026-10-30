
using MyTelegram.Schema.Payments;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetCurrencyRatesHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetCurrencyRates, MyTelegram.Schema.Payments.ICurrencyRates>
{
    protected override Task<MyTelegram.Schema.Payments.ICurrencyRates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetCurrencyRates obj)
    {
        return Task.FromResult<MyTelegram.Schema.Payments.ICurrencyRates>(new TCurrencyRates
        {
            Rates = []
        });
    }
}

