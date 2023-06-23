// ReSharper disable All

using MyTelegram.Schema.Payments;

namespace MyTelegram.Handlers.Payments;

public class ExportInvoiceHandler : RpcResultObjectHandler<RequestExportInvoice, IExportedInvoice>,
    Payments.IExportInvoiceHandler
{
    protected override Task<IExportedInvoice> HandleCoreAsync(IRequestInput input,
        RequestExportInvoice obj)
    {
        throw new NotImplementedException();
    }
}