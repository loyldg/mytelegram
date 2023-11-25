// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Remote document
/// See <a href="https://corefork.telegram.org/constructor/WebDocument" />
///</summary>
[JsonDerivedType(typeof(TWebDocument), nameof(TWebDocument))]
[JsonDerivedType(typeof(TWebDocumentNoProxy), nameof(TWebDocumentNoProxy))]
public interface IWebDocument : IObject
{
    ///<summary>
    /// Document URL
    ///</summary>
    string Url { get; set; }

    ///<summary>
    /// File size
    ///</summary>
    int Size { get; set; }

    ///<summary>
    /// MIME type
    ///</summary>
    string MimeType { get; set; }

    ///<summary>
    /// Attributes for media types
    /// See <a href="https://corefork.telegram.org/type/DocumentAttribute" />
    ///</summary>
    TVector<MyTelegram.Schema.IDocumentAttribute> Attributes { get; set; }
}
