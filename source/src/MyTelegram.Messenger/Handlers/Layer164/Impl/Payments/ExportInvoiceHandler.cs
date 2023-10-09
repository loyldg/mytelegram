// ReSharper disable All

namespace MyTelegram.Handlers.Payments;

///<summary>
/// Generate an <a href="https://corefork.telegram.org/api/links#invoice-links">invoice deep link</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CURRENCY_TOTAL_AMOUNT_INVALID The total amount of all prices is invalid.
/// 400 INVOICE_PAYLOAD_INVALID The specified invoice payload is invalid.
/// 400 MEDIA_INVALID Media invalid.
/// 400 PAYMENT_PROVIDER_INVALID The specified payment provider is invalid.
/// See <a href="https://corefork.telegram.org/method/payments.exportInvoice" />
///</summary>
internal sealed class ExportInvoiceHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestExportInvoice, MyTelegram.Schema.Payments.IExportedInvoice>,
    Payments.IExportInvoiceHandler
{
    protected override Task<MyTelegram.Schema.Payments.IExportedInvoice> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestExportInvoice obj)
    {
        throw new NotImplementedException();
    }
}
