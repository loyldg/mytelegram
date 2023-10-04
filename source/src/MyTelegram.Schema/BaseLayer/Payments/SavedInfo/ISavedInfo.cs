// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// Saved payment info
/// See <a href="https://corefork.telegram.org/constructor/payments.SavedInfo" />
///</summary>
public interface ISavedInfo : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether the user has some saved payment credentials
    ///</summary>
    bool HasSavedCredentials { get; set; }

    ///<summary>
    /// Saved server-side order information
    /// See <a href="https://corefork.telegram.org/type/PaymentRequestedInfo" />
    ///</summary>
    MyTelegram.Schema.IPaymentRequestedInfo? SavedInfo { get; set; }
}
