namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Returns the conversation history with one interlocutor / within a chat
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 400 FROZEN_PARTICIPANT_MISSING The current account is <a href="https://corefork.telegram.org/api/auth#frozen-accounts">frozen</a>, and cannot access the specified peer.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 500 NEED_DOC_INVALID  
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 TAKEOUT_INVALID The specified takeout ID is invalid.
/// 500 VOLUME_MOVE_INVALID  
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getHistory"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetHistoryHandler(IMessageAppService messageAppService, IQueryProcessor queryProcessor, IPeerHelper peerHelper, IChannelAppService channelAppService, IGetHistoryConverterService getHistoryConverterService) : RpcResultObjectHandler<RequestGetHistory, IMessages>
{
    protected override async Task<IMessages> HandleCoreAsync(IRequestInput input, RequestGetHistory obj)
    {
        var userId = input.UserId;
        var peer = peerHelper.GetPeer(obj.Peer, userId);
        var ownerPeerId = peer.PeerType == PeerType.Channel ? peer.PeerId : userId;
        if (peer.PeerType == PeerType.Channel)
        {
            var channelMember = await queryProcessor.ProcessAsync(new GetChannelMemberByUserIdQuery(peer.PeerId, input.UserId));
            if (channelMember?.Kicked == true)
            {
                return new TChannelMessages
                {
                    Chats = [],
                    Messages = [],
                    Users = []
                };
            }

            var channelReadModel = await channelAppService.GetAsync(peer.PeerId);
            if (await channelAppService.SendRpcErrorIfNotChannelMemberAsync(input, channelReadModel!))
            {
                return null!;
            }
        }

        int channelHistoryMinId;
        //if (peer.PeerType == PeerType.Channel || peer.PeerType == PeerType.Chat)
        {
            var dialogReadModel = await queryProcessor.ProcessAsync(new GetDialogByIdQuery(DialogId.Create(input.UserId, peer).Value));
            channelHistoryMinId = dialogReadModel?.ChannelHistoryMinId ?? 0;
        }

        var r = await messageAppService.GetHistoryAsync(new GetHistoryInput { OwnerPeerId = ownerPeerId, SelfUserId = userId, AddOffset = obj.AddOffset, Limit = obj.Limit, MaxId = obj.MaxId, MinId = obj.MinId, OffsetId = obj.OffsetId, Peer = peerHelper.GetPeer(obj.Peer, userId), ChannelHistoryMinId = channelHistoryMinId });
        return getHistoryConverterService.ToMessages(input, r, input.Layer);
    }
}