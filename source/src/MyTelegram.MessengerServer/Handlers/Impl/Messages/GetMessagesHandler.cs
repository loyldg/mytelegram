using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetMessagesHandler : RpcResultObjectHandler<RequestGetMessages, IMessages>,
    IGetMessagesHandler, IProcessedHandler
{
    private readonly IMessageAppService _messageAppService;
    private readonly IRpcResultProcessor _rpcResultProcessor;

    public GetMessagesHandler(IMessageAppService messageAppService,
        IRpcResultProcessor rpcResultProcessor)
    {
        _messageAppService = messageAppService;
        _rpcResultProcessor = rpcResultProcessor;
    }

    protected override async Task<IMessages> HandleCoreAsync(IRequestInput input,
        RequestGetMessages obj)
    {
        var idList = new List<int>();
        foreach (var inputMessage in obj.Id)
        {
            if (inputMessage is TInputMessageID inputMessageId)
            {
                idList.Add(inputMessageId.Id);
            }
        }

        var dto = await _messageAppService
            .GetMessagesAsync(new GetMessagesInput(input.UserId, input.UserId, idList, null) { Limit = 50 })
            ;

        return _rpcResultProcessor.ToMessages(dto);
    }
}

public class GetMessagesHandlerLayerN : RpcResultObjectHandler<Schema.LayerN.RequestGetMessages, IMessages>,
    IGetMessagesHandlerLayerN, IProcessedHandler
{
    private readonly IMessageAppService _messageAppService;
    private readonly IRpcResultProcessor _rpcResultProcessor;

    public GetMessagesHandlerLayerN(IMessageAppService messageAppService,
        IRpcResultProcessor rpcResultProcessor)
    {
        _messageAppService = messageAppService;
        _rpcResultProcessor = rpcResultProcessor;
    }

    protected override async Task<IMessages> HandleCoreAsync(IRequestInput input,
        Schema.LayerN.RequestGetMessages obj)
    {
        var idList = obj.Id.ToList(); // new List<int>();
        //foreach (var inputMessage in obj.Id)
        //{
        //    if (inputMessage is TInputMessageID inputMessageId)
        //    {
        //        idList.Add(inputMessageId.Id);
        //    }
        //}

        var dto = await _messageAppService
            .GetMessagesAsync(new GetMessagesInput(input.UserId, input.UserId, idList, null) { Limit = 50 })
            ;

        return _rpcResultProcessor.ToMessages(dto);
    }
}
