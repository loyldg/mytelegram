//using MyTelegram.Handlers.Services;

//namespace MyTelegram.Messenger.CommandServer.EventHandlers;

//public class PtsEventHandler :
//    IEventHandler<NewPtsMessageHasSentEvent>,
//    IEventHandler<RpcMessageHasSentEvent>,
//    IEventHandler<AcksDataReceivedEvent>//,

//    , IDistributedEventHandler<NewPtsMessageHasSentEvent>,
//    IDistributedEventHandler<RpcMessageHasSentEvent>,
//    IDistributedEventHandler<AcksDataReceivedEvent>

//{
//    private readonly IAckCacheService _ackCacheService;
//    private readonly ICommandBus _commandBus;
//    private readonly ILogger<PtsEventHandler> _logger;

//    public PtsEventHandler(IAckCacheService ackCacheService,
//        ICommandBus commandBus,
//        ILogger<PtsEventHandler> logger)
//    {
//        _ackCacheService = ackCacheService;
//        _commandBus = commandBus;
//        _logger = logger;
//    }

//    public async Task HandleEventAsync(AcksDataReceivedEvent eventData)
//    {
//        var data = eventData.Data.ToTObject<TMsgsAck>();
//        _logger.LogTrace("Receive ack from {UserId}(authKeyId={AuthKeyId:x2}):{@MsgIds}",
//            eventData.UserId,
//            eventData.AuthKeyId,
//            data.MsgIds);
//        foreach (var msgId in data.MsgIds)
//        {
//            if (_ackCacheService.TryGetPts(msgId, out var ackCacheItem))
//            {
//                _logger.LogInformation("Ack pts,msgId:{MsgId},userId:{UserId},pts:{Pts},globalSeqNo:{GlobalSeqNo}", msgId, eventData.UserId, ackCacheItem.Pts, ackCacheItem.GlobalSeqNo);

//                // Console.WriteLine($"Ack:UserId={eventData.UserId} ReqMsgId={eventData.ReqMsgId} GlobalSeqNo={ackCacheItem.GlobalSeqNo}");
//                var command = new PtsAckedCommand(PtsId.Create(eventData.UserId, eventData.PermAuthKeyId),
//                    eventData.UserId,
//                    eventData.PermAuthKeyId,
//                    msgId,
//                    ackCacheItem.Pts,
//                    ackCacheItem.GlobalSeqNo,
//                    ackCacheItem.ToPeer
//                );
//                await _commandBus.PublishAsync(command, default);
//            }
//            else
//            {
//                if (_ackCacheService.TryGetRpcPtsCache(msgId, out ackCacheItem))
//                {
//                    _logger.LogInformation("Ack pts,msgId:{MsgId},userId:{UserId},pts:{Pts},globalSeqNo:{GlobalSeqNo}", msgId, eventData.UserId, ackCacheItem.Pts, ackCacheItem.GlobalSeqNo);

//                    // Console.WriteLine($"Rpc Ack:UserId={eventData.UserId} ReqMsgId={eventData.ReqMsgId} Pts={ackCacheItem.Pts} GlobalSeqNo={ackCacheItem.GlobalSeqNo}");
//                    var command = new PtsAckedCommand(PtsId.Create(eventData.UserId, eventData.PermAuthKeyId),
//                        eventData.UserId,
//                        eventData.PermAuthKeyId,
//                        msgId,
//                        ackCacheItem.Pts,
//                        ackCacheItem.GlobalSeqNo,
//                        ackCacheItem.ToPeer
//                    );
//                    await _commandBus.PublishAsync(command, default);
//                }
//            }
//        }
//    }

//    public Task HandleEventAsync(NewPtsMessageHasSentEvent eventData)
//    {
//        _logger.LogTrace("New message has sent,msgId={MsgId},userId={UserId},pts={Pts}",
//            eventData.MsgId,
//            eventData.ToUid,
//            eventData.Pts);
//        _ackCacheService.AddMsgIdToCacheAsync(eventData.MsgId, eventData.Pts, eventData.GlobalSeqNo, eventData.ToPeer);
//        return Task.CompletedTask;
//    }

//    public Task HandleEventAsync(RpcMessageHasSentEvent eventData)
//    {
//        _logger.LogTrace("New rpc message has sent,msgId={MsgId},userId={UserId},globalSeqNo={GlobalSeqNo}",
//            eventData.MsgId,
//            eventData.UserId,
//            eventData.GlobalSeqNo
//        );
//        _ackCacheService.AddRpcMsgIdToCache(eventData.MsgId, eventData.ReqMsgId);

//        return Task.CompletedTask;
//    }
//}
