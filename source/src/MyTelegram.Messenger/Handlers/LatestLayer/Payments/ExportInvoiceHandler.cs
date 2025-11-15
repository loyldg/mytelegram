namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Generate an <a href="https://corefork.telegram.org/api/links#invoice-links">invoice deep link</a>
/// Possible errors
/// Code Type Description
/// 400 BUSINESS_CONNECTION_INVALID The <code>connection_id</code> passed to the wrapping <a href="https://corefork.telegram.org/api/business">invokeWithBusinessConnection</a> call is invalid.
/// 400 CURRENCY_TOTAL_AMOUNT_INVALID The total amount of all prices is invalid.
/// 400 INVOICE_PAYLOAD_INVALID The specified invoice payload is invalid.
/// 400 MEDIA_INVALID Media invalid.
/// 400 PAYMENT_PROVIDER_INVALID The specified payment provider is invalid.
/// 400 STARS_INVOICE_INVALID The specified Telegram Star invoice is invalid.
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// 400 WEBDOCUMENT_MIME_INVALID Invalid webdocument mime type provided.
/// 400 WEBDOCUMENT_URL_EMPTY The passed web document URL is empty.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.exportInvoice"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✖] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class ExportInvoiceHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestExportInvoice, MyTelegram.Schema.Payments.IExportedInvoice>
{
    protected override Task<MyTelegram.Schema.Payments.IExportedInvoice> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestExportInvoice obj)
    {
        throw new NotImplementedException();
    }
}