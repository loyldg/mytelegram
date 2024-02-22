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
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;
    private readonly IAccessHashHelper _accessHashHelper;
    public AddContactHandler(ICommandBus commandBus,
        IPeerHelper peerHelper,
        IAccessHashHelper accessHashHelper)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
        _accessHashHelper = accessHashHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestAddContact obj)
    {
        if (obj.Id is TInputUser inputUser)
        {
            await _accessHashHelper.CheckAccessHashAsync(inputUser.UserId, inputUser.AccessHash);
            var peer = _peerHelper.GetPeer(obj.Id, input.UserId);
            var command = new AddContactCommand(ContactId.Create(input.UserId, peer.PeerId),
                input.ToRequestInfo(),
                input.UserId,
                peer.PeerId,
                obj.Phone,
                obj.FirstName,
                obj.LastName,
                obj.AddPhonePrivacyException);
            await _commandBus.PublishAsync(command, default);

            return null!;
        }

        throw new NotImplementedException();
    }
}
