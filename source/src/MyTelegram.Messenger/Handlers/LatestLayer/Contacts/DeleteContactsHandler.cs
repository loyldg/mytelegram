namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;

///<summary>
/// Deletes several contacts from the list.
/// See <a href="https://corefork.telegram.org/method/contacts.deleteContacts" />
///</summary>
internal sealed class DeleteContactsHandler(
    ICommandBus commandBus,
    IPeerHelper peerHelper,
    IAccessHashHelper accessHashHelper)
    : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestDeleteContacts, MyTelegram.Schema.IUpdates>
{
    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestDeleteContacts obj)
    {
        foreach (TInputUser inputUser in obj.Id)
        {
            await accessHashHelper.CheckAccessHashAsync(input, inputUser.UserId, inputUser.AccessHash, AccessHashType.User);
            var peer = peerHelper.GetPeer(inputUser, input.UserId);
            var command = new DeleteContactCommand(ContactId.Create(input.UserId, peer.PeerId),
                input.ToRequestInfo(),
                peer.PeerId);
            await commandBus.PublishAsync(command);
        }

        return null!;
    }
}
