// ReSharper disable All

namespace MyTelegram.Handlers.Payments;

public class RequestRecurringPaymentHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestRequestRecurringPayment, MyTelegram.Schema.IUpdates>,
    Payments.IRequestRecurringPaymentHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestRequestRecurringPayment obj)
    {
        throw new NotImplementedException();
    }
}
