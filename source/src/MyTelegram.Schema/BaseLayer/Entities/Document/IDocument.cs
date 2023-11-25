// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A document.
/// See <a href="https://corefork.telegram.org/constructor/Document" />
///</summary>
[JsonDerivedType(typeof(TDocumentEmpty), nameof(TDocumentEmpty))]
[JsonDerivedType(typeof(TDocument), nameof(TDocument))]
public interface IDocument : IObject
{
    ///<summary>
    /// Document ID
    ///</summary>
    long Id { get; set; }
}
