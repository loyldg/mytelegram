// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Deletes several contacts from the list.
/// See <a href="https://corefork.telegram.org/method/contacts.deleteContacts" />
///</summary>
internal sealed class DeleteContactsHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestDeleteContacts, MyTelegram.Schema.IUpdates>,
    Contacts.IDeleteContactsHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestDeleteContacts obj)
    {
        throw new NotImplementedException();
    }
}
