// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Dismiss or approve a chat <a href="https://corefork.telegram.org/api/invites#join-requests">join request</a> related to a specific chat or channel.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNELS_TOO_MUCH You have joined too many channels/supergroups.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 HIDE_REQUESTER_MISSING The join request was missing or was already handled.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USER_ALREADY_PARTICIPANT The user is already in the group.
/// 403 USER_CHANNELS_TOO_MUCH One of the users you tried to add is already in too many channels/supergroups.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.hideChatJoinRequest" />
///</summary>
internal sealed class HideChatJoinRequestHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestHideChatJoinRequest, MyTelegram.Schema.IUpdates>,
    Messages.IHideChatJoinRequestHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly IPeerHelper _peerHelper;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly ICommandBus _commandBus;
    public HideChatJoinRequestHandler(IQueryProcessor queryProcessor, IPeerHelper peerHelper, IAccessHashHelper accessHashHelper, ICommandBus commandBus)
    {
        _queryProcessor = queryProcessor;
        _peerHelper = peerHelper;
        _accessHashHelper = accessHashHelper;
        _commandBus = commandBus;
    }

    protected override async Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestHideChatJoinRequest obj)
    {
        await _accessHashHelper.CheckAccessHashAsync(obj.Peer);
        await _accessHashHelper.CheckAccessHashAsync(obj.UserId);

        var channelPeer = _peerHelper.GetPeer(obj.Peer);
        var userPeer = _peerHelper.GetPeer(obj.UserId);
        var chatInviteImporterReadModel =
            await _queryProcessor.ProcessAsync(new GetChatInviteImporterQuery(channelPeer.PeerId, userPeer.PeerId));
        if (chatInviteImporterReadModel == null )
        {
            RpcErrors.RpcErrors400.HideRequesterMissing.ThrowRpcError();
        }

        if (chatInviteImporterReadModel!.ChatInviteRequestState != ChatInviteRequestState.NeedApprove)
        {
            RpcErrors.RpcErrors400.HideRequesterMissing.ThrowRpcError();
        }

        var command = new HideChatJoinRequestCommand(ChannelId.Create(channelPeer.PeerId), input.ToRequestInfo(),
            userPeer.PeerId, obj.Approved);
        await _commandBus.PublishAsync(command, default);

        return null!;
    }
}
