// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object contains info on a successfully imported contact.
/// See <a href="https://corefork.telegram.org/constructor/ImportedContact" />
///</summary>
public interface IImportedContact : IObject
{
    ///<summary>
    /// User identifier
    ///</summary>
    long UserId { get; set; }

    ///<summary>
    /// The contact's client identifier (passed to one of the <a href="https://corefork.telegram.org/type/InputContact">InputContact</a> constructors)
    ///</summary>
    long ClientId { get; set; }
}
