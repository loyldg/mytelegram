// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Specifies a document that will have to be downloaded from the URL by the telegram servers
/// See <a href="https://corefork.telegram.org/constructor/InputWebDocument" />
///</summary>
public interface IInputWebDocument : IObject
{
    ///<summary>
    /// Remote document URL to be downloaded using the appropriate <a href="https://corefork.telegram.org/api/files">method</a>
    ///</summary>
    string Url { get; set; }

    ///<summary>
    /// Remote file size
    ///</summary>
    int Size { get; set; }

    ///<summary>
    /// Mime type
    ///</summary>
    string MimeType { get; set; }

    ///<summary>
    /// Attributes for media types
    /// See <a href="https://corefork.telegram.org/type/DocumentAttribute" />
    ///</summary>
    TVector<MyTelegram.Schema.IDocumentAttribute> Attributes { get; set; }
}
