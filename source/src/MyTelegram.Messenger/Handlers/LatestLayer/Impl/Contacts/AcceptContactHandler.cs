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
    private readonly ICommandBus _commandBus;
    private readonly IQueryProcessor _queryProcessor;
    private readonly IPeerHelper _peerHelper;
    private readonly IAccessHashHelper _accessHashHelper;
    public AcceptContactHandler(ICommandBus commandBus, IQueryProcessor queryProcessor, IPeerHelper peerHelper, IAccessHashHelper accessHashHelper)
    {
        _commandBus = commandBus;
        _queryProcessor = queryProcessor;
        _peerHelper = peerHelper;
        _accessHashHelper = accessHashHelper;
    }

    protected override async Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestAcceptContact obj)
    {
        var peer = _peerHelper.GetPeer(obj.Id);
        await _accessHashHelper.CheckAccessHashAsync(peer);
        var userReadModel = await _queryProcessor.ProcessAsync(new GetUserByIdQuery(peer.PeerId));
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

        await _commandBus.PublishAsync(command, default);

        return null!;
    }
}
