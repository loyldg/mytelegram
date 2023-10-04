// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A document.
/// See <a href="https://corefork.telegram.org/constructor/Document" />
///</summary>
public interface IDocument : IObject
{
    ///<summary>
    /// Document ID
    ///</summary>
    long Id { get; set; }
}
