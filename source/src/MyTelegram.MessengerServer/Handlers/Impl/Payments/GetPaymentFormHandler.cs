using MyTelegram.Handlers.Payments;
using MyTelegram.Schema.Payments;

namespace MyTelegram.MessengerServer.Handlers.Impl.Payments;

public class GetPaymentFormHandler : RpcResultObjectHandler<RequestGetPaymentForm, IPaymentForm>,
    IGetPaymentFormHandler
{
    protected override Task<IPaymentForm> HandleCoreAsync(IRequestInput input,
        RequestGetPaymentForm obj)
    {
        throw new NotImplementedException();
    }
}