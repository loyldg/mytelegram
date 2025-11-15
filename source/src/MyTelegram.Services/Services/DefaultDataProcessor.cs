using MyTelegram.Services.Extensions;
using System.Diagnostics;

namespace MyTelegram.Services.Services;

public class DefaultDataProcessor<TData>(
    IHandlerHelper handlerHelper,
    IObjectMessageSender objectMessageSender,
    ILogger<DefaultDataProcessor<TData>> logger,
    IExceptionProcessor exceptionProcessor,
    IRequestHelper requestHelper,
    IInvokeAfterMsgProcessor invokeAfterMsgProcessor)
    : IDataProcessor<TData>
    where TData : DataReceivedEvent
{
    public virtual Task ProcessAsync(TData obj)
    {
        Task.Run(async () =>
        {
            var sw = Stopwatch.StartNew();
            if (handlerHelper.TryGetHandler(obj.ObjectId, out var handler))
            {

                IObject? data = null;
                var req = GetRequestInput(obj);
                try
                {
                    data = GetData(obj);

                    bool needToCheckRequest = handler is IDistinctObjectHandler ||
                                              ObjectIdConsts.CommandServerHandlers.ContainsKey(req.ObjectId);

                    if (!needToCheckRequest && data is IHasSubQuery subQuery)
                    {
                        needToCheckRequest =
                            ObjectIdConsts.CommandServerHandlers.ContainsKey(subQuery.Query.ConstructorId);

                        if (!needToCheckRequest && subQuery.Query is IHasSubQuery subQuery2)
                        {
                            needToCheckRequest =
                                ObjectIdConsts.CommandServerHandlers.ContainsKey(subQuery2.Query.ConstructorId);
                        }
                    }

                    if (needToCheckRequest)
                    {
                        if (!await requestHelper.CheckRequestAsync(req))
                        {
                            return;
                        }
                    }


                    var handlerName = handler.GetType().Name;
                    // Updates.getDifference and langpack.getDifference use the same handler name
                    if (req.ObjectId == 0xcd984aa5)
                    {
                        handlerName = $"{handlerName}(langpack)";
                    }

                    if (data is IHasSubQuery)
                    {
                        handlerName = handlerHelper.GetHandlerFullName(data);
                    }

                    var r = await handler.HandleAsync(req, data);
                    sw.Stop();

                    logger.UserRequestHandled(
                        obj.UserId,
                        req.ReqMsgId,
                        handlerName,
                        obj.DeviceType,
                        obj.Layer,
                        sw.Elapsed.TotalMilliseconds
                    );

                    if (logger.IsEnabled(LogLevel.Debug))
                    {
                        logger.LogDebug(
                            "User {UserId} {Handler} {DeviceType}[{Layer}] {ElapsedMs}ms. [{@Input}] Request data: {@Request}, Response: {@Response}",
                            obj.UserId,
                            handlerName,
                            obj.DeviceType,
                            obj.Layer,
                            sw.Elapsed.TotalMilliseconds,
                            req,
                            data,
                            r
                        );
                    }

                    if (r != null!)
                    {
                        await SendMessageToPeerAsync(GetRequestInput(obj).ToRequestInfo(), r);
                    }

                    await invokeAfterMsgProcessor.AddCompletedReqMsgIdAsync(obj.ReqMsgId);
                }
                catch (Exception ex)
                {
                    await exceptionProcessor.HandleExceptionAsync(ex, req, data,
                        handler.GetType().Name);
                }
                finally
                {
                    if (obj is IMayHaveMemoryOwner mayHaveMemoryOwner)
                    {
                        mayHaveMemoryOwner.MemoryOwner?.Dispose();
                    }
                }
            }
        });

        return Task.CompletedTask;
    }

    protected virtual IObject GetData(TData obj)
    {
        return obj.Data.ToTObject<IObject>();
    }

    protected virtual IRequestInput GetRequestInput(TData obj)
    {
        var req = new RequestInput(
            obj.ConnectionId,
            obj.ConnectionType,
            obj.RequestId,
            obj.ObjectId,
            obj.ReqMsgId,
            obj.SeqNumber,
            obj.UserId,
            obj.AuthKeyId,
            obj.PermAuthKeyId,
            obj.Layer,
            obj.Date,
            obj.DeviceType,
            obj.ClientIp,
            obj.SessionId,
            obj.AccessHashKeyId
        );

        return req;
    }

    protected virtual Task SendMessageToPeerAsync(RequestInfo requestInfo,
        IObject data)
    {
        return objectMessageSender.SendMessageToPeerAsync(requestInfo, data);
    }
}