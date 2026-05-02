using System.Collections.Concurrent;

namespace MyTelegram.AuthServer.EventHandlers;

public class UnencryptedMessageHandler(
    ILogger<UnencryptedMessageHandler> logger,
    IHandlerHelper handlerHelper,
    IEventBus eventBus,
    IOptionsMonitor<MyTelegramAuthServerOptions> options) : IEventHandler<UnencryptedMessage>, ITransientDependency
{
    private static readonly ConcurrentDictionary<string, IResPQ> Step1Results = new();

    public async Task HandleEventAsync(UnencryptedMessage eventData)
    {
        try
        {
            if (!handlerHelper.TryGetHandler(eventData.ObjectId, out var handler))
            {
                logger.LogWarning(
                    "Cannot find a handler with objectId {ObjectId:x2}, connectionId: {ConnectionId}",
                    eventData.ObjectId,
                    eventData.ConnectionId
                );

                return;
            }

            if (logger.IsEnabled(LogLevel.Trace))
            {
                logger.LogTrace(
                    "Processing unencrypted message, connectionId: {ConnectionId}, [objectId]: {ObjectId:x2}, handler: {Handler}, reqMsgId: {ReqMsgId}",
                    eventData.ConnectionId,
                    eventData.ObjectId,
                    handler.GetType().Name,
                    eventData.MessageId
                );
            }

            var obj = eventData.MessageData.ToTObject<IObject>();
            var r = await handler.HandleAsync(
                new RequestInput(
                    eventData.ConnectionId,
                    eventData.ConnectionType,
                    //eventData.DcId,
                    eventData.RequestId,
                    eventData.ObjectId,
                    eventData.MessageId,
                    0,
                    0,
                    eventData.AuthKeyId,
                    eventData.AuthKeyId,
                    0,
                    eventData.Date,
                    DeviceType.Unknown,
                    eventData.ClientIp,
                    0,
                    0
                ),
                obj
            );

            // After a server restart
            // The Android client then sends two ReqPqMultiHandler requests in a very short time(10ms), causing Nonce verification to fail.
            // This is resolved by delaying the response to ReqPqHandler / ReqPqMultiHandler.If multiple ReqPqHandler/ ReqPqMultiHandler requests
            // arrive on the same TCP connection within a short period(50ms), only the last request will be responded to.
            if (r is IResPQ resPq)
            {
                //Step1Results[eventData.ConnectionId] = resPq;
                Step1Results.TryRemove(eventData.ConnectionId, out _);
                Step1Results.TryAdd(eventData.ConnectionId, resPq);
                _ = Task.Delay(options.CurrentValue.AuthStep1DelayMs).ContinueWith(async _ =>
                {
                    if (Step1Results.Remove(eventData.ConnectionId, out var newResPq))
                    {
                        await SendUnencryptResultToClientAsync(eventData, newResPq);
                    }
                });

                return;
            }

            if (r != null!)
            {
                await SendUnencryptResultToClientAsync(eventData, r);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(
                "Process request failed, connectionId: {ConnectionId}, reqMsgId: {ReqMsgId}, error: {Error}",
                eventData.ConnectionId,
                eventData.MessageId,
                ex
            );
        }
        finally
        {
            eventData.MemoryOwner?.Dispose();
        }

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace(
                "Process unencrypted message completed, reqMsgId: {ReqMsgId}",
                eventData.MessageId
            );
        }
    }

    private async Task SendUnencryptResultToClientAsync(UnencryptedMessage unencryptedMessage, IObject result)
    {
        using var writer = new ArrayPoolBufferWriter<byte>();
        result.Serialize(writer);
        var unencryptedResponse = new UnencryptedMessageResponse(
            unencryptedMessage.AuthKeyId,
            writer.WrittenMemory,
            unencryptedMessage.ConnectionId,
            unencryptedMessage.MessageId
        );
        await eventBus.PublishAsync(unencryptedResponse);
    }
}