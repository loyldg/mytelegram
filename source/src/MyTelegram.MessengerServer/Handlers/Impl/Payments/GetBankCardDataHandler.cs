using MyTelegram.Handlers.Payments;
using MyTelegram.Schema.Payments;

namespace MyTelegram.MessengerServer.Handlers.Impl.Payments;

public class GetBankCardDataHandler : RpcResultObjectHandler<RequestGetBankCardData, IBankCardData>,
    IGetBankCardDataHandler
{
    protected override Task<IBankCardData> HandleCoreAsync(IRequestInput input,
        RequestGetBankCardData obj)
    {
        throw new NotImplementedException();
    }
}