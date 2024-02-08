// ReSharper disable All

namespace MyTelegram.Handlers.Messages.LayerN;

///<summary>
/// Returns the list of messages by their IDs.
/// See <a href="https://corefork.telegram.org/method/messages.getMessages" />
///</summary>
internal sealed class GetMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.LayerN.RequestGetMessages, MyTelegram.Schema.Messages.IMessages>,
    Messages.LayerN.IGetMessagesHandler
{
    private readonly IMessageAppService _messageAppService;
    //private readonly IRpcResultProcessor _rpcResultProcessor;
    private readonly ILayeredService<IRpcResultProcessor> _layeredService;

    public GetMessagesHandler(IMessageAppService messageAppService,
        //IRpcResultProcessor rpcResultProcessor,
        ILayeredService<IRpcResultProcessor> layeredService)
    {
        _messageAppService = messageAppService;
        _layeredService = layeredService;
        //_rpcResultProcessor = rpcResultProcessor;
    }

    protected override async Task<MyTelegram.Schema.Messages.IMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.LayerN.RequestGetMessages obj)
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

        //return _rpcResultProcessor.ToMessages(dto, input.Layer);
        return _layeredService.GetConverter(input.Layer).ToMessages(dto, input.Layer);
    }
}
