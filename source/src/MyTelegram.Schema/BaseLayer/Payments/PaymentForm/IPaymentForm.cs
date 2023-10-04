// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// Payment form
/// See <a href="https://corefork.telegram.org/constructor/payments.PaymentForm" />
///</summary>
public interface IPaymentForm : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether the user can choose to save credentials.
    ///</summary>
    bool CanSaveCredentials { get; set; }

    ///<summary>
    /// Indicates that the user can save payment credentials, but only after setting up a <a href="https://corefork.telegram.org/api/srp">2FA password</a> (currently the account doesn't have a <a href="https://corefork.telegram.org/api/srp">2FA password</a>)
    ///</summary>
    bool PasswordMissing { get; set; }

    ///<summary>
    /// Form ID
    ///</summary>
    long FormId { get; set; }

    ///<summary>
    /// Bot ID
    ///</summary>
    long BotId { get; set; }

    ///<summary>
    /// Form title
    ///</summary>
    string Title { get; set; }

    ///<summary>
    /// Description
    ///</summary>
    string Description { get; set; }

    ///<summary>
    /// Product photo
    /// See <a href="https://corefork.telegram.org/type/WebDocument" />
    ///</summary>
    MyTelegram.Schema.IWebDocument? Photo { get; set; }

    ///<summary>
    /// Invoice
    /// See <a href="https://corefork.telegram.org/type/Invoice" />
    ///</summary>
    MyTelegram.Schema.IInvoice Invoice { get; set; }

    ///<summary>
    /// Payment provider ID.
    ///</summary>
    long ProviderId { get; set; }

    ///<summary>
    /// Payment form URL
    ///</summary>
    string Url { get; set; }

    ///<summary>
    /// Payment provider name.<br>One of the following:<br>- <code>stripe</code>
    ///</summary>
    string? NativeProvider { get; set; }

    ///<summary>
    /// Contains information about the payment provider, if available, to support it natively without the need for opening the URL.<br>A JSON object that can contain the following fields:<br><br>- <code>apple_pay_merchant_id</code>: Apple Pay merchant ID<br>- <code>google_pay_public_key</code>: Google Pay public key<br>- <code>need_country</code>: True, if the user country must be provided,<br>- <code>need_zip</code>: True, if the user ZIP/postal code must be provided,<br>- <code>need_cardholder_name</code>: True, if the cardholder name must be provided<br>
    /// See <a href="https://corefork.telegram.org/type/DataJSON" />
    ///</summary>
    MyTelegram.Schema.IDataJSON? NativeParams { get; set; }

    ///<summary>
    /// Additional payment methods
    /// See <a href="https://corefork.telegram.org/type/PaymentFormMethod" />
    ///</summary>
    TVector<MyTelegram.Schema.IPaymentFormMethod>? AdditionalMethods { get; set; }

    ///<summary>
    /// Saved server-side order information
    /// See <a href="https://corefork.telegram.org/type/PaymentRequestedInfo" />
    ///</summary>
    MyTelegram.Schema.IPaymentRequestedInfo? SavedInfo { get; set; }

    ///<summary>
    /// Contains information about saved card credentials
    /// See <a href="https://corefork.telegram.org/type/PaymentSavedCredentials" />
    ///</summary>
    TVector<MyTelegram.Schema.IPaymentSavedCredentials>? SavedCredentials { get; set; }

    ///<summary>
    /// Users
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
