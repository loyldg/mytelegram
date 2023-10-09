// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Returns the current user's contact list.
/// See <a href="https://corefork.telegram.org/method/contacts.getContacts" />
///</summary>
internal sealed class GetContactsHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestGetContacts, MyTelegram.Schema.Contacts.IContacts>,
    Contacts.IGetContactsHandler
{
    protected override Task<MyTelegram.Schema.Contacts.IContacts> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestGetContacts obj)
    {
        throw new NotImplementedException();
    }
}
