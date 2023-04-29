using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SearchGlobalHandler : RpcResultObjectHandler<RequestSearchGlobal, IMessages>,
    ISearchGlobalHandler, IProcessedHandler
{
    private readonly IMessageAppService _messageAppService;
    private readonly IRpcResultProcessor _rpcResultProcessor;

    public SearchGlobalHandler(IMessageAppService messageAppService,
        IRpcResultProcessor rpcResultProcessor)
    {
        _messageAppService = messageAppService;
        _rpcResultProcessor = rpcResultProcessor;
    }

    protected override async Task<IMessages> HandleCoreAsync(IRequestInput input,
        RequestSearchGlobal obj)
    {
        //var userId = await GetUidAsync(input);
        var userId = input.UserId;

        var r = await _messageAppService.SearchGlobalAsync(
            new SearchGlobalInput(MessageType.Unknown,
                userId,
                obj.Q,
                userId,
                obj.FolderId)
            {
                Limit = obj.Limit,
                OffsetId = obj.OffsetId
            });

        return _rpcResultProcessor.ToMessages(r);
    }
}
