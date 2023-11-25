// ReSharper disable All

namespace MyTelegram.Schema.Contacts;

///<summary>
/// Info on the current user's contact list.
/// See <a href="https://corefork.telegram.org/constructor/contacts.Contacts" />
///</summary>
[JsonDerivedType(typeof(TContactsNotModified), nameof(TContactsNotModified))]
[JsonDerivedType(typeof(TContacts), nameof(TContacts))]
public interface IContacts : IObject
{

}
