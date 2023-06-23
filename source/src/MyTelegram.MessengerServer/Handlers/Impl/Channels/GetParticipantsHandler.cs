using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;
using IChannelParticipant = MyTelegram.Schema.IChannelParticipant;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class GetParticipantsHandler : RpcResultObjectHandler<RequestGetParticipants, IChannelParticipants>,
    IGetParticipantsHandler, IProcessedHandler
{
    private readonly ITlChatConverter _chatConverter;
    private readonly ILogger<GetParticipantsHandler> _logger;
    private readonly IQueryProcessor _queryProcessor;

    public GetParticipantsHandler(IQueryProcessor queryProcessor,
        ILogger<GetParticipantsHandler> logger,
        ITlChatConverter chatConverter //,
    )
    {
        _queryProcessor = queryProcessor;
        _logger = logger;
        _chatConverter = chatConverter;
    }

    protected override async Task<IChannelParticipants> HandleCoreAsync(IRequestInput input,
        RequestGetParticipants obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            var joinedChannelIdList = await _queryProcessor.ProcessAsync(new GetJoinedChannelIdListQuery(input.UserId,
                    new List<long> { inputChannel.ChannelId }),
                default);

            var channelReadModel = await _queryProcessor
                .ProcessAsync(new GetChannelByIdQuery(inputChannel.ChannelId), default);

            // Only channel creator or admin can access channel participants
            if (channelReadModel.Broadcast)
                if (channelReadModel.CreatorId != input.UserId &&
                    channelReadModel.AdminList.FirstOrDefault(p => p.UserId == input.UserId) == null)
                {
                    _logger.LogWarning("None admin get channel participants,userId={UserId},channelId={ChannelId}",
                        input.UserId,
                        inputChannel.ChannelId);
                    return new TChannelParticipants
                    {
                        Chats = new TVector<IChat>(),
                        Count = 0,
                        Participants = new TVector<IChannelParticipant>(),
                        Users = new TVector<IUser>()
                    };
                }

            if (joinedChannelIdList.Count == 0 && channelReadModel.Broadcast)
                return new TChannelParticipants
                {
                    Chats = new TVector<IChat>(),
                    Count = 0,
                    Participants = new TVector<IChannelParticipant>(),
                    Users = new TVector<IUser>()
                };

            //ThrowHelper.ThrowUserFriendlyException("CHANNEL_PRIVATE");
            void CheckAdminPermission(IChannelReadModel channel,
                long userId)
            {
                if (channelReadModel.Broadcast)
                    if (channel.CreatorId != userId &&
                        channel.AdminList.FirstOrDefault(p => p.UserId == userId) == null)
                        ThrowHelper.ThrowUserFriendlyException("CHAT_ADMIN_REQUIRED");
            }

            var memberUidList = new List<long>();
            var forceNotLeft = false;
            IQuery<IReadOnlyCollection<IChannelMemberReadModel>> query;
            switch (obj.Filter)
            {
                case TChannelParticipantsAdmins:
                    CheckAdminPermission(channelReadModel, input.UserId);
                    if (channelReadModel.AdminList.Count > 0)
                    {
                        memberUidList.AddRange(channelReadModel.AdminList.Select(p => p.UserId).ToList());
                        if (input.UserId == channelReadModel.CreatorId) memberUidList.Add(input.UserId);

                        query = new GetChannelMembersByChannelIdQuery(inputChannel.ChannelId,
                            memberUidList,
                            false,
                            obj.Offset,
                            obj.Limit);
                    }
                    else
                    {
                        return new TChannelParticipants
                        {
                            Participants = new TVector<IChannelParticipant>(),
                            Users = new TVector<IUser>(),
                            Chats = new TVector<IChat>()
                        };
                    }

                    break;
                case TChannelParticipantsBots:
                    return new TChannelParticipants
                    {
                        Participants = new TVector<IChannelParticipant>(),
                        Users = new TVector<IUser>(),
                        Chats = new TVector<IChat>()
                    };
                case TChannelParticipantsKicked:
                    CheckAdminPermission(channelReadModel, input.UserId);
                    forceNotLeft = true;
                    query = new GetKickedChannelMembersQuery(inputChannel.ChannelId, obj.Offset, obj.Limit);
                    break;
                default:
                    query = new GetChannelMembersByChannelIdQuery(inputChannel.ChannelId,
                        new List<long>(),
                        false,
                        obj.Offset,
                        obj.Limit);
                    break;
            }

            if (joinedChannelIdList.Contains(channelReadModel.ChannelId)) forceNotLeft = true;

            var channelMemberReadModels = await _queryProcessor
                .ProcessAsync(query,
                    default);
            //var selfChannelMember = channelMemberReadModels.FirstOrDefault(p => p.UserId == input.UserId);

            var userIdList = channelMemberReadModels.Select(p => p.UserId).ToList();
            var userReadModels = await _queryProcessor
                .ProcessAsync(new GetUsersByUidListQuery(userIdList), default);

            return _chatConverter.ToChannelParticipants(channelReadModel,
                channelMemberReadModels,
                userReadModels,
                input.UserId,
                DeviceType.Unknown,
                forceNotLeft);
        }

        throw new NotImplementedException();
    }
}