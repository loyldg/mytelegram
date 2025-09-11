namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;

///<summary>
/// If the <a href="https://corefork.telegram.org/api/action-bar#add-contact">add contact action bar is active</a>, add that user as contact
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CONTACT_ADD_MISSING Contact to add is missing.
/// 400 CONTACT_ID_INVALID The provided contact ID is invalid.
/// 400 CONTACT_REQ_MISSING Missing contact request.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// See <a href="https://corefork.telegram.org/method/contacts.acceptContact" />
///</summary>
internal sealed class AcceptContactHandler(
    ICommandBus commandBus,
    IUserAppService userAppService,
    IPeerHelper peerHelper,
    IAccessHashHelper accessHashHelper)
    : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestAcceptContact, MyTelegram.Schema.IUpdates>
{
    protected override async Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestAcceptContact obj)
    {
        var peer = peerHelper.GetPeer(obj.Id);
        await accessHashHelper.CheckAccessHashAsync(input, obj.Id);
        var userReadModel = await userAppService.GetAsync(peer.PeerId);
        if (userReadModel == null)
        {
            RpcErrors.RpcErrors400.UserIdInvalid.ThrowRpcError();
        }

        var command = new AddContactCommand(ContactId.Create(input.UserId, peer.PeerId), input.ToRequestInfo(),
            input.UserId,
            peer.PeerId,
            userReadModel!.PhoneNumber,
            //null,
            userReadModel.FirstName,
            userReadModel.LastName,
            false
        );

        await commandBus.PublishAsync(command, default);

        return null!;
    }
}
