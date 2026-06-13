namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Get a payment form
/// Possible errors
/// Code Type Description
/// 406 API_GIFT_RESTRICTED_UPDATE_APP Please update the app to access the gift API.
/// 400 BOOST_PEER_INVALID The specified <code>boost_peer</code> is invalid.
/// 403 BOT_ACCESS_FORBIDDEN The specified method <em>can</em> be used over a <a href="https://corefork.telegram.org/api/bots/connected-business-bots">business connection</a> for some operations, but the specified query attempted an operation that is not allowed over a business connection.
/// 400 BOT_INVOICE_INVALID The specified invoice is invalid.
/// 400 BUSINESS_CONNECTION_INVALID The <code>connection_id</code> passed to the wrapping <a href="https://corefork.telegram.org/api/business">invokeWithBusinessConnection</a> call is invalid.
/// 400 GIFT_MONTHS_INVALID The value passed in invoice.inputInvoicePremiumGiftStars.months is invalid.
/// 400 GIFT_STARS_INVALID The specified amount of stars is invalid.
/// 400 INVOICE_INVALID The specified invoice is invalid.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 MESSAGE_TOO_LONG The provided message is too long.
/// 400 MONTH_INVALID The number of months specified in inputInvoicePremiumGiftStars.months is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 NO_PAYMENT_NEEDED The upgrade/transfer of the specified gift was already paid for or is free.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 PREMIUM_PURPOSE_INVALID  
/// 400 SLUG_INVALID The specified invoice slug is invalid.
/// 400 STARGIFT_ALREADY_CONVERTED The specified star gift was already converted to Stars.
/// 400 STARGIFT_ALREADY_REFUNDED The specified star gift was already refunded.
/// 400 STARGIFT_ALREADY_UPGRADED The specified gift was already upgraded to a collectible gift.
/// 406 STARGIFT_EXPORT_IN_PROGRESS A gift export is in progress, a detailed and localized description for the error will be emitted via an <a href="https://corefork.telegram.org/api/errors#406-not-acceptable">updateServiceNotification as specified here »</a>.
/// 400 STARGIFT_INVALID The passed gift is invalid.
/// 400 STARGIFT_MESSAGE_INVALID  
/// 400 STARGIFT_NOT_FOUND The specified gift was not found.
/// 400 STARGIFT_NOT_OWNER  
/// 400 STARGIFT_OWNER_INVALID You cannot transfer or sell a gift owned by another user.
/// 400 STARGIFT_PEER_INVALID The specified inputSavedStarGiftChat.peer is invalid.
/// 400 STARGIFT_RESELL_CURRENCY_NOT_ALLOWED You can't buy the gift using the specified currency (i.e. trying to pay in Stars for TON gifts).
/// 400 STARGIFT_RESELL_TOO_EARLY_%d  
/// 400 STARGIFT_SLUG_INVALID The specified gift slug is invalid.
/// 400 STARGIFT_TRANSFER_TOO_EARLY_%d You cannot transfer this gift yet, wait %d seconds.
/// 400 STARGIFT_UPGRADE_UNAVAILABLE A received gift can only be upgraded to a collectible gift if the <a href="https://corefork.telegram.org/constructor/messageActionStarGift">messageActionStarGift</a>/<a href="https://corefork.telegram.org/constructor/savedStarGift">savedStarGift</a>.<code>can_upgrade</code> flag is set.
/// 406 STARS_FORM_AMOUNT_MISMATCH The form amount has changed, please fetch the new form using <a href="https://corefork.telegram.org/method/payments.getPaymentForm">payments.getPaymentForm</a> and restart the process.
/// 400 TO_ID_INVALID The specified <code>to_id</code> of the passed inputInvoiceStarGiftResale or inputInvoiceStarGiftTransfer is invalid.
/// 400 UNTIL_DATE_INVALID Invalid until date provided.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.getPaymentForm"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✔]
/// </remarks>
internal sealed class GetPaymentFormHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetPaymentForm, MyTelegram.Schema.Payments.IPaymentForm>
{
    protected override Task<MyTelegram.Schema.Payments.IPaymentForm> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetPaymentForm obj)
    {
        throw new NotImplementedException();
    }
}