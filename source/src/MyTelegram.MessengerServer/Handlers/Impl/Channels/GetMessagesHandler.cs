using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.LayerN;
using MyTelegram.Schema.Messages;
using RequestGetMessages = MyTelegram.Schema.Channels.RequestGetMessages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class GetMessagesHandler : RpcResultObjectHandler<RequestGetMessages, IMessages>,
    IGetMessagesHandler, IProcessedHandler
{
    private readonly IMessageAppService _messageAppService;
    private readonly ITlMessageConverter _messageConverter;
    private readonly IRpcResultProcessor _rpcResultProcessor;

    public GetMessagesHandler(IMessageAppService messageAppService,
        IRpcResultProcessor rpcResultProcessor,
        ITlMessageConverter messageConverter)
    {
        _messageAppService = messageAppService;
        _rpcResultProcessor = rpcResultProcessor;
        _messageConverter = messageConverter;
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

        long channelId;
        if (obj.Channel is TInputChannel inputChannel)
        {
            channelId = inputChannel.ChannelId;
        }
        else
        {
            throw new BadRequestException("Only TInputChannel supported for get messages");
        }

        var dto = await _messageAppService
            .GetMessagesAsync(
                new GetMessagesInput(input.UserId,
                        channelId,
                        idList,
                        new Peer(PeerType.Channel, channelId))
                    { Limit = 50 });

        return _rpcResultProcessor.ToMessages(dto);
    }
}

public class GetMessagesHandlerLayerN : RpcResultObjectHandler<RequestGetChannelMessages, IMessages>,
    IGetMessagesHandler, IProcessedHandler
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
        RequestGetChannelMessages obj)
    {
        var idList = obj.Id.ToList();

        long channelId;
        if (obj.Channel is TInputChannel inputChannel)
        {
            channelId = inputChannel.ChannelId;
        }
        else
        {
            throw new BadRequestException("Only TInputChannel supported for get messages");
        }

        var dto = await _messageAppService
            .GetMessagesAsync(
                new GetMessagesInput(input.UserId,
                        channelId,
                        idList,
                        new Peer(PeerType.Channel, channelId))
                    { Limit = 50 });

        return _rpcResultProcessor.ToMessages(dto);
    }
}
