// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Add an existing telegram user as contact.Use <a href="https://corefork.telegram.org/method/contacts.importContacts">contacts.importContacts</a> to add contacts by phone number, without knowing their Telegram ID.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CONTACT_ID_INVALID The provided contact ID is invalid.
/// 400 CONTACT_NAME_EMPTY Contact name empty.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// See <a href="https://corefork.telegram.org/method/contacts.addContact" />
///</summary>
internal sealed class AddContactHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestAddContact, MyTelegram.Schema.IUpdates>,
    Contacts.IAddContactHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestAddContact obj)
    {
        throw new NotImplementedException();
    }
}
