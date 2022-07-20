// ReSharper disable All

namespace MyTelegram.Handlers.Payments;

public class ExportInvoiceHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestExportInvoice, MyTelegram.Schema.Payments.IExportedInvoice>,
    Payments.IExportInvoiceHandler
{
    protected override Task<MyTelegram.Schema.Payments.IExportedInvoice> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestExportInvoice obj)
    {
        throw new NotImplementedException();
    }
}
