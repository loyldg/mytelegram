// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// If the <a href="https://corefork.telegram.org/constructor/peerSettings">peer settings</a> of a new user allow us to add them as contact, add that user as contact
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CONTACT_ADD_MISSING Contact to add is missing.
/// 400 CONTACT_ID_INVALID The provided contact ID is invalid.
/// 400 CONTACT_REQ_MISSING Missing contact request.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// See <a href="https://corefork.telegram.org/method/contacts.acceptContact" />
///</summary>
internal sealed class AcceptContactHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestAcceptContact, MyTelegram.Schema.IUpdates>,
    Contacts.IAcceptContactHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestAcceptContact obj)
    {
        throw new NotImplementedException();
    }
}
