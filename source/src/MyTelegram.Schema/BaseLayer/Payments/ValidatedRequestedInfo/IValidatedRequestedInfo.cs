// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// Validated requested info
/// See <a href="https://corefork.telegram.org/constructor/payments.ValidatedRequestedInfo" />
///</summary>
public interface IValidatedRequestedInfo : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// ID
    ///</summary>
    string? Id { get; set; }

    ///<summary>
    /// Shipping options
    /// See <a href="https://corefork.telegram.org/type/ShippingOption" />
    ///</summary>
    TVector<MyTelegram.Schema.IShippingOption>? ShippingOptions { get; set; }
}
