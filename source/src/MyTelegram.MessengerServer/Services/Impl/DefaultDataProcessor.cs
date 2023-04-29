namespace MyTelegram.MessengerServer.Services.Impl;

public class DefaultDataProcessor<TData> : IDataProcessor<TData>
    where TData : DataReceivedEvent
{
    private readonly IExceptionProcessor _exceptionProcessor;
    private readonly IHandlerHelper _handlerHelper;
    private readonly ILogger<DefaultDataProcessor<TData>> _logger;
    private readonly IObjectMessageSender _objectMessageSender;
    private readonly IRpcResultCacheAppService _rpcResultCacheAppService;

    public DefaultDataProcessor(IHandlerHelper handlerHelper,
        IObjectMessageSender objectMessageSender,
        IRpcResultCacheAppService rpcResultCacheAppService,
        ILogger<DefaultDataProcessor<TData>> logger,
        IExceptionProcessor exceptionProcessor)
    {
        _handlerHelper = handlerHelper;
        _objectMessageSender = objectMessageSender;
        _rpcResultCacheAppService = rpcResultCacheAppService;
        _logger = logger;
        _exceptionProcessor = exceptionProcessor;
    }

    public virtual async Task ProcessAsync(TData obj)
    {
        var sw = Stopwatch.StartNew();
        if (_handlerHelper.TryGetHandler(obj.ObjectId, out var handler))
        {
            if (_rpcResultCacheAppService.TryGetRpcResult(obj.UserId, obj.ReqMsgId, out var rpcResult))
            {
                sw.Stop();
                _logger.LogInformation(
                    "{Elapsed} request from userId={UserId} reqMsgId={ReqMsgId} handler={Handler},returns data from cache",
                    sw.Elapsed,
                    obj.UserId,
                    obj.ReqMsgId,
                    handler.GetType().Name);

                await SendMessageToPeerAsync(obj.ReqMsgId, rpcResult);
                return;
            }

            try
            {
                var req = GetRequestInput(obj);
                var r = await handler.HandleAsync(req, GetData(obj));
                _logger.LogInformation("{Elapsed} request from userId={UserId} reqMsgId={ReqMsgId} handler={Handler}",
                    sw.Elapsed,
                    obj.UserId,
                    obj.ReqMsgId,
                    handler.GetType().Name);

                if (r != null!)
                {
                    await SendMessageToPeerAsync(obj.ReqMsgId, r);
                }
            }
            catch (Exception ex)
            {
                await _exceptionProcessor.HandleExceptionAsync(ex,
                    obj.UserId,
                    handler.GetType().Name,
                    obj.ReqMsgId,
                    obj.AuthKeyId,
                    false);
            }
        }
    }

    protected virtual IObject GetData(TData obj)
    {
        return obj.Data.ToTObject<IObject>();
    }

    //private async Task SendAckAsync(uint objectId, int seqNo, long reqMsgId)
    //{
    //    if (seqNo % 2 == 1 && !ObjectIdConsts.NotNeedAckObjectIdToNames.ContainsKey(objectId))
    //    {
    //        var ack = new TMsgsAck
    //        {
    //            MsgIds = new TVector<long>(reqMsgId)
    //        };
    //        //await _objectMessageSender.PushMessageToPeerAsync();
    //    }
    //}

    protected virtual IRequestInput GetRequestInput(TData obj)
    {
        var req = new RequestInput(obj.ObjectId,
            obj.ReqMsgId,
            obj.UserId,
            obj.AuthKeyId,
            obj.PermAuthKeyId);

        return req;
    }

    protected virtual Task SendMessageToPeerAsync(long reqMsgId,
        IObject data)
    {
        return _objectMessageSender.SendMessageToPeerAsync(reqMsgId, data);
    }
}
