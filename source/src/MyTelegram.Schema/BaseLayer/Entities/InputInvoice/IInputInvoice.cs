// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// An invoice
/// See <a href="https://corefork.telegram.org/constructor/InputInvoice" />
///</summary>
[JsonDerivedType(typeof(TInputInvoiceMessage), nameof(TInputInvoiceMessage))]
[JsonDerivedType(typeof(TInputInvoiceSlug), nameof(TInputInvoiceSlug))]
[JsonDerivedType(typeof(TInputInvoicePremiumGiftCode), nameof(TInputInvoicePremiumGiftCode))]
public interface IInputInvoice : IObject
{

}
