using Microsoft.Extensions.Logging;
using MyTelegram.Schema;
using System.Diagnostics;

namespace MyTelegram.Services.Services;

public class DefaultDataProcessor<TData> : IDataProcessor<TData>
    where TData : DataReceivedEvent
{
    private readonly IExceptionProcessor _exceptionProcessor;
    private readonly IHandlerHelper _handlerHelper;
    private readonly ILogger<DefaultDataProcessor<TData>> _logger;
    private readonly IObjectMessageSender _objectMessageSender;
    private readonly IRpcResultCacheAppService _rpcResultCacheAppService;
    private readonly IInvokeAfterMsgProcessor _invokeAfterMsgProcessor;

    public DefaultDataProcessor(IHandlerHelper handlerHelper,
        IObjectMessageSender objectMessageSender,
        IRpcResultCacheAppService rpcResultCacheAppService,
        ILogger<DefaultDataProcessor<TData>> logger,
        IExceptionProcessor exceptionProcessor,
        IInvokeAfterMsgProcessor invokeAfterMsgProcessor)
    {
        _handlerHelper = handlerHelper;
        _objectMessageSender = objectMessageSender;
        _rpcResultCacheAppService = rpcResultCacheAppService;
        _logger = logger;
        _exceptionProcessor = exceptionProcessor;
        _invokeAfterMsgProcessor = invokeAfterMsgProcessor;
    }

    public virtual Task ProcessAsync(TData obj)
    {
        Task.Run(async () =>
        {
            var sw = Stopwatch.StartNew();
            if (_handlerHelper.TryGetHandler(obj.ObjectId, out var handler))
            {
                if (_rpcResultCacheAppService.TryGetRpcResult(obj.UserId, obj.ReqMsgId, out var rpcResult))
                {
                    sw.Stop();
                    _logger.LogInformation(
                        "{Elapsed}ms UserId={UserId} reqMsgId={ReqMsgId} handler={Handler},returns data from cache",
                        sw.Elapsed.TotalMilliseconds,
                        obj.UserId,
                        obj.ReqMsgId,
                        handler.GetType().Name);

                    //await _objectMessageSender.SendMessageToPeerAsync(obj.ReqMsgId, rpcResult);
                    await SendMessageToPeerAsync(obj.ReqMsgId, rpcResult);
                    return;
                }

                try
                {
                    var req = GetRequestInput(obj);
                    var data = GetData(obj);
                    var handlerName = handler.GetType().Name;
                    if (data is IHasSubQuery)
                    {
                        handlerName = _handlerHelper.GetHandlerFullName(data);
                    }
                    var r = await handler.HandleAsync(req, data);
                    _logger.LogInformation(
                        "{Elapsed}ms UserId={UserId} authKeyId={AuthKeyId:x2} reqMsgId={ReqMsgId} layer={Layer} handler={Handler} {DeviceType}",
                        sw.Elapsed.TotalMilliseconds,
                        obj.UserId,
                        obj.AuthKeyId,
                        obj.ReqMsgId,
                        obj.Layer,
                        handlerName,
                        obj.DeviceType
                        );

                    if (r != null!)
                    {
                        await SendMessageToPeerAsync(obj.ReqMsgId, r);
                    }

                    await _invokeAfterMsgProcessor.AddCompletedReqMsgIdAsync(obj.ReqMsgId);
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
        });

        return Task.CompletedTask;
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
        var req = new RequestInput(
            obj.ConnectionId,
            obj.RequestId,
            obj.ObjectId,
            obj.ReqMsgId,
            obj.UserId,
            obj.AuthKeyId,
            obj.PermAuthKeyId,
            obj.Layer,
            obj.Date,
            obj.DeviceType,
            obj.ClientIp
        );

        return req;
    }

    protected virtual Task SendMessageToPeerAsync(long reqMsgId,
        IObject data)
    {
        return _objectMessageSender.SendMessageToPeerAsync(reqMsgId, data);
    }
}