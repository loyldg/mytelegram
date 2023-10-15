using MyTelegram.Schema.Extensions;

namespace MyTelegram.Messenger.QueryServer.EventHandlers;

public class DuplicateOperationExceptionHandler : IEventHandler<DuplicateCommandEvent>
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly IObjectMessageSender _messageSender;
    private readonly ILogger<DuplicateOperationExceptionHandler> _logger;
    public DuplicateOperationExceptionHandler(IQueryProcessor queryProcessor, IObjectMessageSender messageSender, ILogger<DuplicateOperationExceptionHandler> logger)
    {
        _queryProcessor = queryProcessor;
        _messageSender = messageSender;
        _logger = logger;
    }

    public async Task HandleEventAsync(DuplicateCommandEvent eventData)
    {
        _logger.LogWarning("Duplicate command:{UserId},{ReqMsgId}", eventData.UserId, eventData.ReqMsgId);
        var rpcResult = await _queryProcessor.ProcessAsync(new GetRpcResultQuery(eventData.UserId, eventData.ReqMsgId));
        if (rpcResult != null)
        {
            await _messageSender.SendRpcMessageToClientAsync(eventData.ReqMsgId,
                rpcResult.RpcData.ToTObject<IObject>());
        }
        else
        {
            _logger.LogWarning("Can not find rpc result,userId={UserId},reqMsgId={ReqMsgId}", eventData.UserId, eventData.ReqMsgId);
        }
    }
}