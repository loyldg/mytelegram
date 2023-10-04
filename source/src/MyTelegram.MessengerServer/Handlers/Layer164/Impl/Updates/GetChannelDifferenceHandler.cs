using MyTelegram.Handlers.Updates;
using MyTelegram.Schema.Updates;

namespace MyTelegram.MessengerServer.Handlers.Impl.Updates;

public class GetChannelDifferenceHandler : RpcResultObjectHandler<RequestGetChannelDifference, IChannelDifference>,
    IGetChannelDifferenceHandler, IProcessedHandler
{
    private readonly IAckCacheService _ackCacheService;
    private readonly ITlDifferenceConverter _differenceConverter;
    private readonly IMessageAppService _messageAppService;
    private readonly IQueryProcessor _queryProcessor;

    public GetChannelDifferenceHandler(IMessageAppService messageAppService,
        IQueryProcessor queryProcessor,
        IAckCacheService ackCacheService,
        ITlDifferenceConverter differenceConverter)
    {
        _messageAppService = messageAppService;
        _queryProcessor = queryProcessor;
        _ackCacheService = ackCacheService;
        _differenceConverter = differenceConverter;
    }

    protected override async Task<IChannelDifference> HandleCoreAsync(IRequestInput input,
        RequestGetChannelDifference obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            var channelMemberReadModel = await _queryProcessor
                    .ProcessAsync(new GetChannelMemberByUidQuery(inputChannel.ChannelId, input.UserId), default)
                ;
            var isChannelMember = channelMemberReadModel != null;

            if (channelMemberReadModel is { Kicked: true })
                ThrowHelper.ThrowUserFriendlyException("CHANNEL_PUBLIC_GROUP_NA");

            // if (obj.Force)
            // {
            //     var channelMemberReadModel = await _queryProcessor
            //         .ProcessAsync(new GetChannelMemberByUidQuery(inputChannel.ChannelId, input.UserId), default)
            //         ;
            //     isChannelMember = channelMemberReadModel != null;
            // }
            var limit = obj.Limit == 0 ? MyTelegramServerDomainConsts.DefaultPtsTotalLimit : obj.Limit;
            limit = Math.Min(limit, MyTelegramServerDomainConsts.DefaultPtsTotalLimit);

            var dto = await _messageAppService
                .GetChannelDifferenceAsync(new GetDifferenceInput(inputChannel.ChannelId,
                    obj.Pts,
                    obj.Limit,
                    input.UserId));

            var pushUpdatesReadModelList = await _queryProcessor
                    .ProcessAsync(new GetPushUpdatesQuery(inputChannel.ChannelId, obj.Pts, limit), default)
                ;
            var allUpdateList = new List<IUpdate>();
            var maxPts = 0;
            if (pushUpdatesReadModelList.Count > 0)
            {
                maxPts = pushUpdatesReadModelList.Max(p => p.Pts);
                var channelMaxGlobalSeqNo = pushUpdatesReadModelList.Max(p => p.SeqNo);

                var updatesList = new List<IUpdate>();
                //var userList = new List<IUser>();
                //var chatList = new List<IChat>();
                //var messageList = new List<IMessage>();
                foreach (var pushUpdatesReadModel in pushUpdatesReadModelList)
                {
                    // Console.WriteLine($"##### objectId={BitConverter.ToUInt32(pushUpdatesReadModel.Data):x2}");
                    if (pushUpdatesReadModel.ExcludeAuthKeyId == input.AuthKeyId) continue;

                    var data = pushUpdatesReadModel.Data.ToTObject<IObject>();
                    switch (data)
                    {
                        case TUpdates updates1:
                            updatesList.AddRange(updates1.Updates);
                            //userList.AddRange(updates1.Users);
                            //chatList.AddRange(updates1.Chats);
                            break;
                        case TUpdateShort updateShort:
                            updatesList.Add(updateShort.Update);
                            break;
                        case IMessage:
                            //messageList.Add(message);
                            break;

                        default:
                            //throw new ArgumentOutOfRangeException();
                            throw new NotSupportedException("Not supported push updates data");
                    }
                }

                allUpdateList = updatesList;

                await _ackCacheService.AddRpcPtsToCacheAsync(input.ReqMsgId,
                    0,
                    channelMaxGlobalSeqNo,
                    new Peer(PeerType.Channel, inputChannel.ChannelId));

                //var maxPts = updatesReadModelList.Max(p => p.Pts);
                //await _sequenceService.SetPtsForPeerAsync(input.UserId, PeerType.User, maxPts, input.PermAuthKeyId)
                //    ;
            }

            // Console.WriteLine($"User {input.UserId} get Channel difference,channelId={inputChannel.ChannelId},req pts={obj.Pts},maxPts={maxPts},limit={obj.Limit},updates count={pushUpdatesReadModelList.Count}");

            return _differenceConverter.ToChannelDifference(dto, isChannelMember, allUpdateList, maxPts);
        }

        throw new NotImplementedException();
    }
}