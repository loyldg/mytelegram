// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Requested payment info
/// See <a href="https://corefork.telegram.org/constructor/PaymentRequestedInfo" />
///</summary>
[JsonDerivedType(typeof(TPaymentRequestedInfo), nameof(TPaymentRequestedInfo))]
public interface IPaymentRequestedInfo : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// User's full name
    ///</summary>
    string? Name { get; set; }

    ///<summary>
    /// User's phone number
    ///</summary>
    string? Phone { get; set; }

    ///<summary>
    /// User's email address
    ///</summary>
    string? Email { get; set; }

    ///<summary>
    /// User's shipping address
    /// See <a href="https://corefork.telegram.org/type/PostAddress" />
    ///</summary>
    MyTelegram.Schema.IPostAddress? ShippingAddress { get; set; }
}
