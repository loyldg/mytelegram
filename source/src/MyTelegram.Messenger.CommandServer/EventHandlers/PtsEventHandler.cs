using MyTelegram.Schema.Extensions;

namespace MyTelegram.Messenger.CommandServer.EventHandlers;

public class PtsEventHandler(
    IAckCacheService ackCacheService,
    ICommandBus commandBus)
    :
        ITransientDependency,
        IEventHandler<NewPtsMessageHasSentEvent>,
        IEventHandler<NewQtsMessageHasSentEvent>,
        IEventHandler<RpcMessageHasSentEvent>,
        IEventHandler<AcksDataReceivedEvent>
{
    public async Task HandleEventAsync(AcksDataReceivedEvent eventData)
    {
        try
        {
            var data = eventData.Data.ToTObject<TMsgsAck>();

            foreach (var msgId in data.MsgIds)
            {
                if (ackCacheService.TryGetPts(msgId, out var ackCacheItem))
                {
                    if (ackCacheItem.IsQts)
                    {
                        var command = new QtsAckedCommand(PtsId.Create(eventData.UserId, eventData.PermAuthKeyId),
                            eventData.UserId,
                            eventData.PermAuthKeyId,
                            msgId,
                            ackCacheItem.Pts,
                            ackCacheItem.GlobalSeqNo,
                            ackCacheItem.ToPeer,
                            ackCacheItem.IsFromGetDifference
                        );
                        await commandBus.PublishAsync(command, default);
                    }
                    else
                    {
                        var command = new PtsAckedCommand(PtsId.Create(eventData.UserId, eventData.PermAuthKeyId),
                            eventData.UserId,
                            eventData.PermAuthKeyId,
                            msgId,
                            ackCacheItem.Pts,
                            ackCacheItem.GlobalSeqNo,
                            ackCacheItem.ToPeer,
                            ackCacheItem.IsFromGetDifference
                        );
                        await commandBus.PublishAsync(command, default);
                    }
                }
                else
                {
                    if (ackCacheService.TryGetRpcPtsCache(msgId, out ackCacheItem))
                    {
                        var command = new PtsAckedCommand(PtsId.Create(eventData.UserId, eventData.PermAuthKeyId),
                            eventData.UserId,
                            eventData.PermAuthKeyId,
                            msgId,
                            ackCacheItem.Pts,
                            ackCacheItem.GlobalSeqNo,
                            ackCacheItem.ToPeer,
                            ackCacheItem.IsFromGetDifference
                        );
                        await commandBus.PublishAsync(command, default);
                    }
                }
            }
        }
        finally
        {
            eventData.MemoryOwner?.Dispose();
        }
    }

    public Task HandleEventAsync(NewPtsMessageHasSentEvent eventData)
    {
        ackCacheService.AddMsgIdToCacheAsync(eventData.MsgId, eventData.Pts, eventData.GlobalSeqNo, eventData.ToPeer);
        return Task.CompletedTask;
    }

    public Task HandleEventAsync(RpcMessageHasSentEvent eventData)
    {
        ackCacheService.AddRpcMsgIdToCache(eventData.MsgId, eventData.ReqMsgId);

        return Task.CompletedTask;
    }

    public Task HandleEventAsync(NewQtsMessageHasSentEvent eventData)
    {
        ackCacheService.AddMsgIdToCacheAsync(eventData.MsgId, eventData.Qts, eventData.GlobalSeqNo, eventData.ToPeer, true);

        return Task.CompletedTask;
    }
}
