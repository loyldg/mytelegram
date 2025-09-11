using GetChatInviteByLinkQuery = MyTelegram.Queries.GetChatInviteByLinkQuery;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Get info about the users that joined the chat using a specific chat invite
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 INVITE_HASH_EXPIRED The invite link has expired.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 SEARCH_WITH_LINK_NOT_SUPPORTED You cannot provide a search query and an invite link at the same time.
/// See <a href="https://corefork.telegram.org/method/messages.getChatInviteImporters" />
///</summary>
internal sealed class GetChatInviteImportersHandler(
    IQueryProcessor queryProcessor,
    IAccessHashHelper accessHashHelper,
    IPeerHelper peerHelper,
    IUserConverterService userConverterService)
    : RpcResultObjectHandler<RequestGetChatInviteImporters,
            IChatInviteImporters>
{
    protected override async Task<IChatInviteImporters> HandleCoreAsync(IRequestInput input,
        RequestGetChatInviteImporters obj)
    {
        if (obj.Peer is TInputPeerChannel inputPeerChannel)
        {
            await accessHashHelper.CheckAccessHashAsync(input, inputPeerChannel);
            var userPeer = peerHelper.GetPeer(obj.OffsetUser);

            var channelAdminReadModel = await queryProcessor.ProcessAsync(new GetChatAdminQuery(inputPeerChannel.ChannelId, input.UserId));
            if (channelAdminReadModel == null)
            {
                RpcErrors.RpcErrors403.ChatAdminRequired.ThrowRpcError();
            }

            long? inviteId = null;
            if (!string.IsNullOrEmpty(obj.Link))
            {
                var chatInviteReadModel = await queryProcessor.ProcessAsync(new GetChatInviteByLinkQuery(obj.Link));
                inviteId = chatInviteReadModel?.InviteId;
            }

            var inviteImporterReadModels = await queryProcessor.ProcessAsync(
                new GetChatInviteImportersQuery(inputPeerChannel.ChannelId,
                    obj.Requested ? ChatInviteRequestState.WaitingForApproval : null,
                    inviteId,
                    obj.OffsetDate,
                    userPeer.PeerId, obj.Q, obj.Limit));

            var importers = new List<TChatInviteImporter>();
            var userIds = new List<long>();
            var users = await userConverterService.GetUserListAsync(input, userIds, false, false, input.Layer);
            //var userDict = users.ToDictionary(k => k.Id);
            foreach (var readModel in inviteImporterReadModels)
            {
                //userDict.TryGetValue(readModel.UserId, out var user);

                var importer = new TChatInviteImporter
                {
                    //About = user?.About,
                    ApprovedBy = readModel.ProcessedByUserId,
                    Date = readModel.Date,
                    Requested = !readModel.IsJoinRequestProcessed,
                    UserId = readModel.UserId,
                    //ViaChatlist = readModel.ViaChatList
                };
                importers.Add(importer);
                userIds.Add(readModel.UserId);
            }


            return new TChatInviteImporters
            {
                Importers = [.. importers],
                Users = [.. users],
            };
        }

        return new TChatInviteImporters
        {
            Importers = [],
            Users = [],
        };
    }
}
