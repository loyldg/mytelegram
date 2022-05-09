using MyTelegram.Handlers.Payments;
using MyTelegram.Schema.Payments;

namespace MyTelegram.MessengerServer.Handlers.Impl.Payments;

public class SendPaymentFormHandler : RpcResultObjectHandler<RequestSendPaymentForm, IPaymentResult>,
    ISendPaymentFormHandler
{
    protected override Task<IPaymentResult> HandleCoreAsync(IRequestInput input,
        RequestSendPaymentForm obj)
    {
        throw new NotImplementedException();
    }
}
