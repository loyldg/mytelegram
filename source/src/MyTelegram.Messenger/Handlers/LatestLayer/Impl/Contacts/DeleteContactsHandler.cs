// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Deletes several contacts from the list.
/// See <a href="https://corefork.telegram.org/method/contacts.deleteContacts" />
///</summary>
internal sealed class DeleteContactsHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestDeleteContacts, MyTelegram.Schema.IUpdates>,
    Contacts.IDeleteContactsHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;
    private readonly IAccessHashHelper _accessHashHelper;
    public DeleteContactsHandler(ICommandBus commandBus,
        IPeerHelper peerHelper,
        IAccessHashHelper accessHashHelper)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
        _accessHashHelper = accessHashHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestDeleteContacts obj)
    {
        foreach (TInputUser inputUser in obj.Id)
        {
            await _accessHashHelper.CheckAccessHashAsync(inputUser.UserId, inputUser.AccessHash);
            var peer = _peerHelper.GetPeer(inputUser, input.UserId);
            var command = new DeleteContactCommand(ContactId.Create(input.UserId, peer.PeerId),
                input.ToRequestInfo(),
                peer.PeerId);
            await _commandBus.PublishAsync(command, default);
        }

        return null!;
    }
}
