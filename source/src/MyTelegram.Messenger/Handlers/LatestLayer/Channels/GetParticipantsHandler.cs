namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Get the participants of a <a href="https://corefork.telegram.org/api/channel">supergroup/channel</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// See <a href="https://corefork.telegram.org/method/channels.getParticipants" />
///</summary>
internal sealed class GetParticipantsHandler(
    IQueryProcessor queryProcessor,
    IChatConverterService chatConverterService,
    IAccessHashHelper accessHashHelper,
    IUserConverterService userConverterService,
    IPhotoAppService photoAppService,
    IRpcErrorHelper rpcErrorHelper,
    IChannelAdminRightsChecker channelAdminRightsChecker,
    IChannelAppService channelAppService)
    : RpcResultObjectHandler<RequestGetParticipants,
            IChannelParticipants>
{
    protected override async Task<IChannelParticipants> HandleCoreAsync(IRequestInput input,
        RequestGetParticipants obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await accessHashHelper.CheckAccessHashAsync(input, inputChannel.ChannelId, inputChannel.AccessHash, AccessHashType.Channel);
            var channelReadModel = await channelAppService.GetAsync(inputChannel.ChannelId);
            channelReadModel.ThrowExceptionIfChannelDeleted();

            var participants = new TChannelParticipants
            {
                Chats = [],
                Count = channelReadModel.ParticipantsCount ?? 0,
                Participants = [],
                Users = []
            };

            if (channelReadModel.Broadcast)
            {
                if (channelReadModel.CreatorId != input.UserId &&
                    channelReadModel.AdminList.FirstOrDefault(p => p.UserId == input.UserId) == null)
                {
                    return participants;
                }
            }
            else
            {
                if (channelReadModel.ParticipantsHidden)
                {
                    if (!await channelAdminRightsChecker.HasChatAdminRightAsync(inputChannel.ChannelId, input.UserId,
                            p => p.IsCreator || p.AdminRights.ChangeInfo))
                    {
                        return participants;
                    }
                }

                var joinedChannelIdList = await queryProcessor.ProcessAsync(new GetJoinedChannelIdListQuery(input.UserId,
                    [inputChannel.ChannelId]));
                // Private group
                if (string.IsNullOrEmpty(channelReadModel.UserName) && joinedChannelIdList.Count == 0)
                {
                    RpcErrors.RpcErrors400.ChannelPrivate.ThrowRpcError();
                }
            }

            var channelMemberReadModel =
                await queryProcessor.ProcessAsync(
                    new GetChannelMemberByUserIdQuery(channelReadModel.ChannelId, input.UserId));
            //ChatAdminRights? adminRights = null;
            bool isAdmin = channelMemberReadModel != null && channelMemberReadModel.AdminRights != 0 && channelMemberReadModel.IsAdmin;
            var onlyAdmin = false;
            var onlyBanned = false;
            var onlyBots = false;
            var onlyKicked = false;
            string? keyword = null;


            switch (obj.Filter)
            {
                case TChannelParticipantsAdmins:
                    onlyAdmin = true;
                    break;
                case TChannelParticipantsBanned:

                    if (!isAdmin)
                    {
                        return participants;
                    }

                    onlyBanned = true;
                    break;
                case TChannelParticipantsBots:
                    onlyBots = true;
                    break;
                case TChannelParticipantsContacts:
                    break;
                case TChannelParticipantsKicked channelParticipantsKicked:
                    if (!isAdmin)
                    {
                        return participants;
                    }
                    onlyKicked = true;
                    keyword = channelParticipantsKicked.Q;
                    break;
                case TChannelParticipantsMentions:
                    break;
                case TChannelParticipantsRecent:
                    break;
                case TChannelParticipantsSearch channelParticipantsSearch:
                    keyword = channelParticipantsSearch.Q;
                    break;
            }

            var channelMemberReadModels = await queryProcessor.ProcessAsync(new GetChannelMembersByChannelIdQuery(
                inputChannel.ChannelId,
                [],
                obj.Offset,
                obj.Limit,
                onlyAdmin,
                onlyBots,
                onlyKicked,
                onlyBanned,
                keyword
            ));
            var participantCount = channelReadModel.ParticipantsCount ?? 0;
            if (!isAdmin)
            {
                // Remove anonymous admin
                var newChannelMemberReadModels = channelMemberReadModels.ToList();
                newChannelMemberReadModels.RemoveAll(p =>
                {
                    if (p is { IsAdmin: true, IsBot: false })
                    {
                        var anonymous = (p.AdminRights & (1 << 10)) != 0;
                        participantCount--;

                        return anonymous;
                    }

                    return false;
                });
                channelMemberReadModels = newChannelMemberReadModels;
            }

            if (channelMemberReadModels.Count == 0)
            {
                return participants;
            }

            var forceNotLeft = false;
            var userIds = channelMemberReadModels.Select(p => p.UserId).ToList();

            if (channelMemberReadModel != null)
            {
                userIds.Add(channelMemberReadModel.InviterId);
                if (channelMemberReadModel is { Kicked: false, Left: false })
                {
                    forceNotLeft = true;
                }
            }
            var users = await userConverterService.GetUserListAsync(input, userIds, false, false, input.Layer);
            var photoReadModel = await photoAppService.GetAsync(channelReadModel.PhotoId);

            return chatConverterService.ToChannelParticipants(input, channelReadModel, photoReadModel, participantCount,
                channelMemberReadModels, users, input.DeviceType, forceNotLeft, input.Layer);
        }

        throw new NotImplementedException();
    }
}
