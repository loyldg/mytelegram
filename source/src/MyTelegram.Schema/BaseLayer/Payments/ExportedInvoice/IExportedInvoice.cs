// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// Exported invoice
/// See <a href="https://corefork.telegram.org/constructor/payments.ExportedInvoice" />
///</summary>
[JsonDerivedType(typeof(TExportedInvoice), nameof(TExportedInvoice))]
public interface IExportedInvoice : IObject
{
    ///<summary>
    /// Exported <a href="https://corefork.telegram.org/api/links#invoice-links">invoice deep link</a>
    ///</summary>
    string Url { get; set; }
}
