// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Defines a document for subsequent interaction.
/// See <a href="https://corefork.telegram.org/constructor/InputDocument" />
///</summary>
[JsonDerivedType(typeof(TInputDocumentEmpty), nameof(TInputDocumentEmpty))]
[JsonDerivedType(typeof(TInputDocument), nameof(TInputDocument))]
public interface IInputDocument : IObject
{

}
