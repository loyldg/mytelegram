namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Make a payment using <a href="https://corefork.telegram.org/api/stars#using-stars">Telegram Stars, see here »</a> for more info.
/// Possible errors
/// Code Type Description
/// 406 API_GIFT_RESTRICTED_UPDATE_APP Please update the app to access the gift API.
/// 400 BALANCE_TOO_LOW The transaction cannot be completed because the current <a href="https://corefork.telegram.org/api/stars">Telegram Stars balance</a> is too low.
/// 403 BOT_ACCESS_FORBIDDEN The specified method <em>can</em> be used over a <a href="https://corefork.telegram.org/api/bots/connected-business-bots">business connection</a> for some operations, but the specified query attempted an operation that is not allowed over a business connection.
/// 400 BOT_INVOICE_INVALID The specified invoice is invalid.
/// 400 BUSINESS_CONNECTION_INVALID The <code>connection_id</code> passed to the wrapping <a href="https://corefork.telegram.org/api/business">invokeWithBusinessConnection</a> call is invalid.
/// 400 FORM_EXPIRED The form was generated more than 10 minutes ago and has expired, please re-generate it using <a href="https://corefork.telegram.org/method/payments.getPaymentForm">payments.getPaymentForm</a> and pass the new <code>form_id</code>.
/// 400 FORM_ID_EMPTY The specified form ID is empty.
/// 400 FORM_SUBMIT_DUPLICATE The same payment form was already submitted.  .
/// 400 FORM_UNSUPPORTED Please update your client.
/// 400 GIFT_STARS_INVALID The specified amount of stars is invalid.
/// 400 MEDIA_ALREADY_PAID You already paid for the specified media.
/// 400 MONTH_INVALID The number of months specified in inputInvoicePremiumGiftStars.months is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 406 PRECHECKOUT_FAILED Precheckout failed, a detailed and localized description for the error will be emitted via an <a href="https://corefork.telegram.org/api/errors#406-not-acceptable">updateServiceNotification as specified here »</a>.
/// 400 PURPOSE_INVALID The specified payment purpose is invalid.
/// 400 STARGIFT_ALREADY_UPGRADED The specified gift was already upgraded to a collectible gift.
/// 400 STARGIFT_NOT_FOUND The specified gift was not found.
/// 400 STARGIFT_OWNER_INVALID You cannot transfer or sell a gift owned by another user.
/// 400 STARGIFT_SLUG_INVALID The specified gift slug is invalid.
/// 400 STARGIFT_USAGE_LIMITED The gift is sold out.
/// 400 STARGIFT_USER_USAGE_LIMITED You've reached the starGift.limited_per_user limit, you can't buy any more gifts of this type.
/// 406 STARS_FORM_AMOUNT_MISMATCH The form amount has changed, please fetch the new form using <a href="https://corefork.telegram.org/method/payments.getPaymentForm">payments.getPaymentForm</a> and restart the process.
/// 400 TO_ID_INVALID The specified <code>to_id</code> of the passed inputInvoiceStarGiftResale or inputInvoiceStarGiftTransfer is invalid.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.sendStarsForm"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class SendStarsFormHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestSendStarsForm, MyTelegram.Schema.Payments.IPaymentResult>
{
    protected override Task<MyTelegram.Schema.Payments.IPaymentResult> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestSendStarsForm obj)
    {
        throw new NotImplementedException();
    }
}