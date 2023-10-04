using MyTelegram.Handlers.Interfaces;

namespace MyTelegram.MessengerServer.Handlers.Impl;
//public class MsgsAckHandler : BaseObjectHandler<TMsgsAck, IObject>, IMsgsAckHandler,IProcessedHandler
//{
//    protected override Task<IObject> HandleCoreAsync(RequestInput input,
//        TMsgsAck obj)
//    {
//        Logger.LogDebug($"receive ack:{JsonConvert.SerializeObject(obj.MsgIds)}");
//        return null!;
//    }
//}

public class MsgContainerHandler : BaseObjectHandler<TMsgContainer, IObject>, IMsgContainerHandler,
    IProcessedHandler
{
    private readonly IExceptionProcessor _exceptionProcessor;

    private readonly IHandlerHelper _handlerHelper;

    //private readonly IMessageIdHelper _messageIdHelper;
    //private readonly IPermissionChecker _permissionChecker;
    //private readonly IRequestCacheAppService _requestCacheAppService;
    private readonly ILogger<MsgContainerHandler> _logger;
    private readonly IMessageIdGenerator _messageIdGenerator;

    private readonly IObjectMessageSender _messageSender;
    private readonly IRpcResultCacheAppService _rpcResultCacheAppService;

    public MsgContainerHandler(IHandlerHelper handlerHelper,
        //IMessageIdHelper messageIdHelper,
        //IPermissionChecker permissionChecker,
        //IRequestCacheAppService requestCacheAppService,
        ILogger<MsgContainerHandler> logger,
        IExceptionProcessor exceptionProcessor,
        IRpcResultCacheAppService rpcResultCacheAppService,
        IObjectMessageSender messageSender,
        IMessageIdGenerator messageIdGenerator)
    {
        _handlerHelper = handlerHelper;
        //_messageIdHelper = messageIdHelper;
        //_permissionChecker = permissionChecker;
        //_requestCacheAppService = requestCacheAppService;
        _logger = logger;
        _exceptionProcessor = exceptionProcessor;
        _rpcResultCacheAppService = rpcResultCacheAppService;
        _messageSender = messageSender;
        _messageIdGenerator = messageIdGenerator;
    }

    protected override async Task<IObject> HandleCoreAsync(IRequestInput input,
        TMsgContainer obj)
    {
        var containerMessageList = new List<TContainerMessage>();
        var count = obj.Messages.Length;
        var currentCount = 0;
        //long reqMsgId = 0;
        //RequestInvokeWithLayer requestInvokeWithLayer = null;
        //bool needCreateDevice = false;

        //bool isBindTempAuthKey = false;
        //bool isNewSessionCreated = false;

        foreach (var m in obj.Messages)
        {
            var handlerName = string.Empty;
            var sw = Stopwatch.StartNew();
            try
            {
                currentCount++;
                var objectId = m.Body.ConstructorId;
                if (!_handlerHelper.TryGetHandler(objectId, out var handler))
                {
                    //_logger.LogDebug($"[{currentCount}/{count}][{sw.Elapsed}] {handlerName} can not find handler to process the request,skip for this request.userId={input.UserId},authKeyId={input.AuthKeyId:x2}");
                    _logger.LogWarning(
                        "{TimeSpan}[{CurrentCount}/{Count}] {HandlerName} can not find handler to process the request,skip for this request.userId={UserId},authKeyId={AuthKeyId:x2}",
                        sw.Elapsed,
                        currentCount,
                        count,
                        handlerName,
                        input.UserId,
                        input.AuthKeyId
                    );
                    continue;
                }

                if (_rpcResultCacheAppService.TryGetRpcResult(input.UserId, m.MsgId, out var cachedRpcResult))
                {
                    await _messageSender.SendMessageToPeerAsync(m.MsgId, cachedRpcResult);
                    //await _localEventBus.PublishAsync(new SendMessageToClientEto(input.AuthKeyData,
                    //    input.ServerSalt,
                    //    cachedRpcResult,
                    //    input.ConnectionId,
                    //    m.SeqNo + 1,
                    //    input.AuthKeyId,
                    //    input.RequestSessionId,
                    //    input.ReqMsgId));
                    continue;
                }

                //if (!isBindTempAuthKey)
                //{
                //    isBindTempAuthKey = objectId == ObjectIdConsts.BindTempAuthKey;
                //}

                //if (isBindTempAuthKey && !isNewSessionCreated)
                //{
                //    var newSession = new TNewSessionCreated
                //    {
                //        FirstMsgId = m.MsgId,
                //        ServerSalt = BitConverter.ToInt64(input.ServerSalt),
                //        UniqueId = _messageIdHelper.GenerateUniqueId(),
                //    };
                //    var newSessionStream = new MemoryStream();
                //    newSession.Serialize(new BinaryWriter(newSessionStream));
                //    containerMessageList.Add(new TContainerMessage
                //    {
                //        MsgId = _messageIdHelper.GenerateMessageId(),
                //        Bytes = (int)newSessionStream.Length,
                //        SeqNo = 1,
                //        Body = newSession,

                //    });
                //    isNewSessionCreated = true;
                //}

                _handlerHelper.TryGetHandlerShortName(objectId, out handlerName);
                //if (objectId == ObjectIdConsts.InvokeWithLayer)
                //{
                //    if (m.Body is RequestInvokeWithLayer requestInvokeWithLayer)
                //    {
                //        _handlerHelper.TryGetHandlerShortName(requestInvokeWithLayer.Query.ConstructorId, out var handlerName2);
                //        handlerName = $"{handlerName}->{handlerName2}";                            
                //    }
                //}

                //Logger.LogDebug($"req:msgContainer->(userId:{input.UserId})#{objectId:x2} {handlerName}");
                //_logger.LogDebug($"MsgContainer->[{currentCount}/{count}],reqMsgId={m.MsgId:x2},userId={input.UserId},handler={handlerName}");
                _logger.LogTrace(
                    "AuthKeyId={AuthKeyId:x2},userId={UserId},MsgContainer->[{CurrentCount}/{Count}] reqMsgId={ReqMsgId:x2},handler={HandlerName} [{Elapsed}]",

                    //input.ConnectionId,
                    input.AuthKeyId,
                    input.UserId,
                    //input.,
                    currentCount,
                    count,
                    m.MsgId,
                    handlerName,
                    sw.Elapsed
                );
                _logger.LogDebug("{TimeSpan} UserId={UserId} msgContainer->[{CurrentCount}/{Count}] {HandlerName}",
                    sw.Elapsed,
                    input.UserId,
                    currentCount,
                    count,
                    handlerName);

                //CheckPermission(input.AuthKeyId, objectId, input.IsAuthKeyActive);
                IRequestInput newReq = input switch
                {
                    SessionRequestInput sessionRequestInput => sessionRequestInput with { ReqMsgId = m.MsgId },
                    RequestInput requestInput1 => requestInput1 with { ReqMsgId = m.MsgId },
                    _ => throw new ArgumentOutOfRangeException(nameof(input))
                };

                //if (handler is IShouldCacheRequest)
                //{
                //    _requestCacheAppService.AddRequest(newReq.MessageId,
                //        newReq.AuthKeyId,
                //        newReq.RequestSessionId,
                //        newReq.SeqNumber);
                //}

                //if (handler is IMsgsAckHandler)
                //{
                //    sw.Stop();
                //    //_logger.LogDebug($"[{currentCount}/{count}][{sw.Elapsed}] Ack handler. receive ack from:{newReq.AuthKeyId:x2},userId={input.UserId},");
                //    _logger.LogTrace(
                //        "{TimeSpan} ConnectionId={ConnectionId},authKeyId={AuthKeyId:x2},userId={UserId},sessionId={SessionId:x2} msgContainer-> [{CurrentCount}/{Count}]ack handler",
                //        sw.Elapsed,
                //        input.ConnectionId,
                //        input.AuthKeyId,
                //        input.UserId,
                //        input.RequestSessionId,
                //        currentCount,
                //        count
                //    );

                //    continue;
                //}

                var r = await handler.HandleAsync(newReq, m.Body);
                sw.Stop();

                //if (handler is IMsgsAckHandler)
                //{
                //    Logger.LogDebug($"[{currentCount}/{count}] Ack handler. receive ack from:{newReq.AuthKeyId}");
                //    continue;
                //}

                //if (sw.ElapsedMilliseconds > MyTelegramConsts.ProcessRequestTooSlowMilliseconds)
                //{
                //    _logger.LogWarning(
                //        $"[{currentCount}/{count}][{sw.Elapsed}]process request reqMsgId={m.MsgId:x2} completed,userId={input.UserId},handler={handlerName}[response too slow].");
                //}
                //else
                //{
                //    _logger.LogInformation($"[{currentCount}/{count}][{sw.Elapsed}]process request reqMsgId={m.MsgId:x2} completed,userId={input.UserId},handler={handlerName}.");
                //}
                var isTooSlow = sw.ElapsedMilliseconds > MyTelegramServerConsts.ProcessRequestTooSlowMilliseconds;
                _logger.LogTrace(
                    "Elapsed={TimeSpan},handler=[{CurrentCount}/{Count}] {HandlerName},authKeyId={AuthKeyId:x2},userId={UserId}, process request {ReqMsgId} completed {Description}",
                    sw.Elapsed,
                    currentCount,
                    count,
                    handlerName,
                    //input.ConnectionId,
                    input.AuthKeyId,
                    input.UserId,
                    //input.RequestSessionId,
                    input.ReqMsgId,
                    isTooSlow ? "[response too slow]" : string.Empty
                );

                if (isTooSlow)
                    _logger.LogWarning(
                        "{TimeSpan} UserId={UserId} request handler=msgContainer->[{CurrentCount}/{Count}] {HandlerName} [response too slow]",
                        sw.Elapsed,
                        input.UserId,
                        currentCount,
                        count,
                        handlerName);
                else
                    _logger.LogInformation(
                        "{TimeSpan} UserId={UserId} request handler=msgContainer->[{CurrentCount}/{Count}] {HandlerName}",
                        sw.Elapsed,
                        input.UserId,
                        currentCount,
                        count,
                        handlerName);

                if (r == null!)
                    //Logger.LogDebug($"[{currentCount}/{count}][{sw.Elapsed.TotalMilliseconds:0000}ms]Processed reqMsgId={m.MsgId:x2} ok,handler={handlerName},handler returns null,continue to process next request.");
                    continue;

                //await using var stream = new MemoryStream();
                //var bw = new BinaryWriter(stream);
                //r.Serialize(bw);
                var seqNo = m.SeqNo + 1;

                if (r is TRpcResult)
                {
                    await _messageSender.SendMessageToPeerAsync(m.MsgId, r);
                    //await _localEventBus.PublishAsync(new SendMessageToClientEto(input.AuthKeyData,
                    //    input.ServerSalt,
                    //    r,
                    //    input.ConnectionId,
                    //    m.SeqNo + 1,
                    //    input.AuthKeyId,
                    //    input.RequestSessionId,
                    //    input.ReqMsgId));

                    _rpcResultCacheAppService.TryAdd(input.UserId, m.MsgId, r);
                    continue;
                }

                {
                    var messageId = await _messageIdGenerator.GenerateServerMessageIdAsync();
                    containerMessageList.Add(new TContainerMessage
                    {
                        Body = r,
                        MsgId = messageId, // _messageIdHelper.GenerateMessageId(),
                        Bytes = r.GetLength(),
                        SeqNo = seqNo
                    });
                }

                if (m.SeqNo % 2 == 1 && !ObjectIdConsts.NotNeedAckObjectIdToNames.ContainsKey(objectId))
                {
                    // send ack msg to client
                    var ack = new TMsgsAck { MsgIds = new TVector<long>(m.MsgId) };
                    //var ackStream = new MemoryStream();
                    //var ackWriter = new BinaryWriter(ackStream);
                    //ack.Serialize(ackWriter);
                    var messageId = await _messageIdGenerator.GenerateServerMessageIdAsync();
                    containerMessageList.Add(new TContainerMessage
                    {
                        Body = ack,
                        MsgId = messageId, // _messageIdHelper.GenerateMessageId(),
                        Bytes = ack.GetLength(),
                        SeqNo = m.SeqNo + 1
                    });
                    //await _distributedEventBus.PublishAsync(new PushMessageToClientIntegrationEvent(ack,
                    //    input.ConnectionId, input.SeqNumber + 1, input.AuthKeyId, input.RequestSessionId));
                }
            }
            catch (Exception ex)
            {
                await _exceptionProcessor.HandleExceptionAsync(ex,
                    input.UserId,
                    handlerName,
                    m.MsgId,
                    //input.AuthKeyData,
                    //input.ServerSalt,
                    //input.ConnectionId,
                    //m.SeqNo + 1,
                    input.AuthKeyId,
                    //input.RequestSessionId,
                    true);
            }
        }

        if (containerMessageList.Count == 0) return null!;

        return new TMsgContainer { Messages = containerMessageList.ToArray() };
    }

    //private void CheckPermission(long authKeyId,
    //    uint objectId, bool isAuthKeyActive)
    //{
    //    // 如果没有登录，可以访问部分API，
    //    // 如果Session被同一个用户的其他设备重置，则只能访问部分API

    //    //if (!_permissionChecker.HasPermission(authKeyId, objectId))
    //    //{
    //    //    Logger.LogWarning($"req:#{objectId:x2} has no permission.");
    //    //    //throw new UnauthorizedException("AUTH_KEY_PERM_EMPTY");

    //    //    // AUTH_KEY_UNREGISTERED client will logout and goto login ui
    //    //    throw new UnauthorizedAccessException("AUTH_KEY_UNREGISTERED");
    //    //}

    //    _permissionChecker.CheckPermission(authKeyId, objectId);

    //    if (!isAuthKeyActive && !_permissionChecker.IsNotLoginApi(objectId))
    //    {
    //        throw new UnauthorizedException("AUTH_KEY_UNREGISTERED");
    //    }
    //}
}