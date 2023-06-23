using MyTelegram.Handlers.Payments;
using MyTelegram.Schema.Payments;

namespace MyTelegram.MessengerServer.Handlers.Impl.Payments;

public class GetPaymentReceiptHandler : RpcResultObjectHandler<RequestGetPaymentReceipt, IPaymentReceipt>,
    IGetPaymentReceiptHandler
{
    protected override Task<IPaymentReceipt> HandleCoreAsync(IRequestInput input,
        RequestGetPaymentReceipt obj)
    {
        throw new NotImplementedException();
    }
}