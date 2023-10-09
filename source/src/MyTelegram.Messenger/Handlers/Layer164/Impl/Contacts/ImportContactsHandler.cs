// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Imports contacts: saves a full list on the server, adds already registered contacts to the contact list, returns added contacts and their info.Use <a href="https://corefork.telegram.org/method/contacts.addContact">contacts.addContact</a> to add Telegram contacts without actually using their phone number.
/// See <a href="https://corefork.telegram.org/method/contacts.importContacts" />
///</summary>
internal sealed class ImportContactsHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestImportContacts, MyTelegram.Schema.Contacts.IImportedContacts>,
    Contacts.IImportContactsHandler
{
    protected override Task<MyTelegram.Schema.Contacts.IImportedContacts> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestImportContacts obj)
    {
        throw new NotImplementedException();
    }
}
