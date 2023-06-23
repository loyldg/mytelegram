using MyTelegram.Handlers.Updates;
using MyTelegram.Schema.Updates;

namespace MyTelegram.MessengerServer.Handlers.Impl.Updates;

public class GetDifferenceHandler : RpcResultObjectHandler<RequestGetDifference,
        IDifference>,
    IGetDifferenceHandler, IProcessedHandler
{
    private readonly IAckCacheService _ackCacheService;
    private readonly ITlDifferenceConverter _differenceConverter;
    private readonly ILogger<GetDifferenceHandler> _logger;
    private readonly IMessageAppService _messageAppService;
    private readonly IPtsHelper _ptsHelper;
    private readonly IQueryProcessor _queryProcessor;

    public GetDifferenceHandler(IMessageAppService messageAppService,
        IPtsHelper ptsHelper,
        IQueryProcessor queryProcessor,
        ILogger<GetDifferenceHandler> logger,
        IAckCacheService ackCacheService,
        ITlDifferenceConverter differenceConverter)
    {
        _messageAppService = messageAppService;
        _ptsHelper = ptsHelper;
        _queryProcessor = queryProcessor;
        _logger = logger;
        _ackCacheService = ackCacheService;
        _differenceConverter = differenceConverter;
    }

    protected override async Task<IDifference> HandleCoreAsync(IRequestInput input,
        RequestGetDifference obj)
    {
        var userId = input.UserId;
        if (userId == 0) return new TDifferenceEmpty();

        var cachedPts = _ptsHelper.GetCachedPts(userId);
        var ptsReadModel = await _queryProcessor.ProcessAsync(new GetPtsByPeerIdQuery(userId), default)
            ;
        var pts = Math.Max(cachedPts, ptsReadModel?.Pts ?? 0);

        var ptsForAuthKeyIdReadModel =
                await _queryProcessor.ProcessAsync(new GetPtsByPermAuthKeyIdQuery(userId, input.PermAuthKeyId), default)
            ;
        var ptsForAuthKeyId = ptsForAuthKeyIdReadModel?.Pts ?? 0;
        var diff = pts - ptsForAuthKeyId;
        if (diff == 0) diff = pts - obj.Pts;

        var joinedChannelIdList = await _queryProcessor
                .ProcessAsync(new GetChannelIdListByMemberUidQuery(input.UserId), default)
            ;

        if (joinedChannelIdList.Count == 0)
            if ((obj.Pts != 0 && obj.Pts == ptsForAuthKeyId) || diff == 0)
                return new TDifferenceEmpty { Date = CurrentDate, Seq = 0 };

        var limit = obj.PtsTotalLimit ?? MyTelegramServerDomainConsts.DefaultPtsTotalLimit;
        limit = Math.Min(limit, MyTelegramServerDomainConsts.DefaultPtsTotalLimit);

        var allPushUpdatesList = new List<IPushUpdatesReadModel>();
        GetMessageOutput? r = null;
        //GetMessageOutput r2 = null;
        var maxPts = 0;
        var channelMaxGlobalSeqNo = 0L;
        if (diff > 0 /*&& ptsForAuthKeyIdReadModel != null*/)
        {
            var pushUpdatesReadModelList = await _queryProcessor
                    .ProcessAsync(new GetPushUpdatesQuery(input.UserId, obj.Pts, limit), default)
                ;
            allPushUpdatesList.AddRange(pushUpdatesReadModelList.Where(p => p.ExcludeAuthKeyId != input.AuthKeyId));
            _logger.LogDebug("UserId={UserId} Normal updates count={Count},pts={Pts},reqPts={ReqPts}",
                userId,
                pushUpdatesReadModelList.Count,
                ptsForAuthKeyId,
                obj.Pts);
            r = await _messageAppService
                .GetDifferenceAsync(new GetDifferenceInput(userId, obj.Pts, limit, userId));
            if (pushUpdatesReadModelList.Count > 0) maxPts = pushUpdatesReadModelList.Max(p => p.Pts);
        }

        var hasUpdatesChannelIdList = new List<long>();
        var globalSeqNo = Math.Min(ptsReadModel?.GlobalSeqNo ?? 0, ptsForAuthKeyIdReadModel?.GlobalSeqNo ?? 0);
        // Console.WriteLine($"PeerId={input.UserId} GlobalSeqNo:{globalSeqNo}");
        if (globalSeqNo > 0)
            if (joinedChannelIdList.Count > 0)
            {
                var channelUpdatesReadModelList = await _queryProcessor
                    .ProcessAsync(
                        new GetChannelPushUpdatesBySeqNoQuery(joinedChannelIdList.ToList(),
                            globalSeqNo,
                            limit),
                        default);
                allPushUpdatesList.AddRange(
                    channelUpdatesReadModelList.Where(p => p.ExcludeAuthKeyId != input.AuthKeyId));
                _logger.LogDebug("UserId={UserId} Channel updates count={Count},minGlobalSeqNo={SeqNo}",
                    userId,
                    channelUpdatesReadModelList.Count,
                    globalSeqNo);
                //r2=await _messageAppService.GetChannelDifferenceAsync(new GetDifferenceInput())
                if (channelUpdatesReadModelList.Count > 0)
                {
                    channelMaxGlobalSeqNo = channelUpdatesReadModelList.Max(p => p.SeqNo);
                    hasUpdatesChannelIdList = channelUpdatesReadModelList.Select(p => p.PeerId).Distinct().ToList();
                }
            }

        var updatesList = new List<IUpdate>();
        //var userList = new List<IUser>();
        var chatList = new List<IChat>();
        var messageList = new List<IMessage>();
        foreach (var pushUpdatesReadModel in allPushUpdatesList)
        {
            var data = pushUpdatesReadModel.Data.ToTObject<IObject>();
            switch (data)
            {
                case TUpdates updates1:
                    updatesList.AddRange(updates1.Updates);
                    //userList.AddRange(updates1.Users);
                    chatList.AddRange(updates1.Chats);
                    break;
                case TUpdateShort updateShort:
                    updatesList.Add(updateShort.Update);
                    break;
                case TUpdateShortMessage updateShortMessage:
                {
                    var m = new TMessage
                    {
                        Date = updateShortMessage.Date,
                        Entities = updateShortMessage.Entities,
                        FromId = new TPeerUser { UserId = updateShortMessage.UserId },
                        PeerId = new TPeerUser { UserId = input.UserId },
                        FwdFrom = updateShortMessage.FwdFrom,
                        Out = updateShortMessage.Out,
                        Mentioned = updateShortMessage.Mentioned,
                        MediaUnread = updateShortMessage.MediaUnread,
                        Silent = updateShortMessage.Silent,
                        Id = updateShortMessage.Id,
                        ReplyTo = updateShortMessage.ReplyTo,
                        Message = updateShortMessage.Message,
                        TtlPeriod = updateShortMessage.TtlPeriod
                    };
                    messageList.Add(m);
                }
                    break;
                case TUpdateShortChatMessage updateShortChatMessage:
                {
                    var m = new TMessage
                    {
                        Date = updateShortChatMessage.Date,
                        Entities = updateShortChatMessage.Entities,
                        FromId = new TPeerUser { UserId = updateShortChatMessage.FromId },
                        PeerId = new TPeerChat { ChatId = updateShortChatMessage.ChatId },
                        FwdFrom = updateShortChatMessage.FwdFrom,
                        Out = updateShortChatMessage.Out,
                        Mentioned = updateShortChatMessage.Mentioned,
                        MediaUnread = updateShortChatMessage.MediaUnread,
                        Silent = updateShortChatMessage.Silent,
                        Id = updateShortChatMessage.Id,
                        ReplyTo = updateShortChatMessage.ReplyTo,
                        Message = updateShortChatMessage.Message,
                        TtlPeriod = updateShortChatMessage.TtlPeriod
                    };
                    messageList.Add(m);
                }
                    break;
                case IMessage message:
                    messageList.Add(message);
                    break;

                default:
                    throw new NotSupportedException($"Not supported push updates data:{data.GetType().FullName}");
            }
        }

        var alreadyLoadedChannelIdList = chatList.Select(p => p.Id).ToList();
        var channelIdListNeedToLoadFromDatabase =
            hasUpdatesChannelIdList.Where(p => !alreadyLoadedChannelIdList.Contains(p)).ToList();
        var channelReadModelList = await _queryProcessor
                .ProcessAsync(new GetChannelByChannelIdListQuery(channelIdListNeedToLoadFromDatabase), default)
            ;

        r ??= new GetMessageOutput(Array.Empty<IChannelReadModel>(),
            Array.Empty<IChannelMemberReadModel>(),
            Array.Empty<IChatReadModel>(),
            Array.Empty<long>(),
            Array.Empty<IMessageReadModel>(),
            Array.Empty<IUserReadModel>(),
            Array.Empty<IPollReadModel>(),
            Array.Empty<IPollAnswerVoterReadModel>(),
            false,
            false,
            0,
            input.UserId
        );

        r.ChannelList = channelReadModelList;

        if (maxPts > 0 || channelMaxGlobalSeqNo > 0)
            // Console.WriteLine($"Add pts ack:maxPts={maxPts},channelMaxGlobalSeqNo={channelMaxGlobalSeqNo},updatesCount={allPushUpdatesList.Count}");
            await _ackCacheService
                .AddRpcPtsToCacheAsync(input.ReqMsgId,
                    maxPts,
                    channelMaxGlobalSeqNo,
                    new Peer(PeerType.User, input.UserId));

        _logger.LogDebug(
            "UserId={UserId} Diff={Diff} AuthKeyId={AuthKeyId} PermAuthKeyId={PermAuthKeyId} Updates Count={Count},Data={@Data},Chats={@Chats},Messages={@Messages},Users={@Users}",
            input.UserId,
            diff,
            input.AuthKeyId,
            input.PermAuthKeyId,
            updatesList.Count,
            updatesList,
            r.ChannelList,
            r.MessageList,
            r.UserList);
        if (updatesList.Count == 0)
        {
            // Console.WriteLine("Count=0");
        }

        var difference = _differenceConverter.ToDifference(r,
            ptsReadModel,
            cachedPts,
            limit,
            updatesList,
            chatList);

        return difference;
    }
}