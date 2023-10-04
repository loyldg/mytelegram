// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object defines a contact from the user's phone book.
/// See <a href="https://corefork.telegram.org/constructor/InputContact" />
///</summary>
public interface IInputContact : IObject
{
    ///<summary>
    /// An arbitrary 64-bit integer: it should be set, for example, to an incremental number when using <a href="https://corefork.telegram.org/method/contacts.importContacts">contacts.importContacts</a>, in order to retry importing only the contacts that weren't imported successfully, according to the client_ids returned in <a href="https://corefork.telegram.org/constructor/contacts.importedContacts">contacts.importedContacts</a>.<code>retry_contacts</code>.
    ///</summary>
    long ClientId { get; set; }

    ///<summary>
    /// Phone number
    ///</summary>
    string Phone { get; set; }

    ///<summary>
    /// Contact's first name
    ///</summary>
    string FirstName { get; set; }

    ///<summary>
    /// Contact's last name
    ///</summary>
    string LastName { get; set; }
}
