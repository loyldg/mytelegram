namespace MyTelegram.AuthServer.EventHandlers;

public class UnencryptedMessageHandler(
    ILogger<UnencryptedMessageHandler> logger,
    IHandlerHelper handlerHelper,
    IEventBus eventBus,
    IOptionsMonitor<MyTelegramAuthServerOptions> options,
    IHandshakeRateLimiter handshakeRateLimiter) : IEventHandler<UnencryptedMessage>, ITransientDependency
{
    // https://core.telegram.org/mtproto/auth_key: an incorrect query is answered with a -404 transport error
    private const int InvalidRequestTransportErrorCode = -404;

    private const uint ReqPqMultiConstructorId = 0xbe7e8ef1;
    private const uint ReqPqConstructorId = 0x60469778;
    private const uint ReqDhParamsConstructorId = 0xd712e4be;
    private const uint SetClientDhParamsConstructorId = 0xf5045f1f;

    private static readonly ConcurrentDictionary<string, IResPQ> Step1Results = new();

    public async Task HandleEventAsync(UnencryptedMessage eventData)
    {
        var isHandshakeRequest = false;
        try
        {
            isHandshakeRequest = eventData.ObjectId is ReqPqMultiConstructorId or ReqPqConstructorId
                or ReqDhParamsConstructorId or SetClientDhParamsConstructorId;
            if (!isHandshakeRequest)
            {
                return;
            }

            var rateLimitKey = string.IsNullOrEmpty(eventData.ClientIp) ? eventData.ConnectionId : eventData.ClientIp;
            if (!handshakeRateLimiter.TryAcquire(rateLimitKey))
            {
                // Dropped without a response: answering would let the client retry immediately.
                return;
            }

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
                    0,
                    0,
                    eventData.DcId
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
                ex,
                "Process request failed, connectionId: {ConnectionId}, reqMsgId: {ReqMsgId}, objectId: {ObjectId:x2}",
                eventData.ConnectionId,
                eventData.MessageId,
                eventData.ObjectId
            );

            if (isHandshakeRequest)
            {
                // The client must restart the handshake; without a response it would wait for a timeout.
                await TrySendTransportErrorAsync(eventData);
            }
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

    private async Task TrySendTransportErrorAsync(UnencryptedMessage eventData)
    {
        try
        {
            await eventBus.PublishAsync(new TransportErrorEvent(eventData.AuthKeyId, eventData.ConnectionId,
                InvalidRequestTransportErrorCode));
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to send transport error, connectionId: {ConnectionId}", eventData.ConnectionId);
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