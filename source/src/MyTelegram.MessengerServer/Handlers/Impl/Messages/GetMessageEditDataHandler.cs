using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetMessageEditDataHandler : RpcResultObjectHandler<RequestGetMessageEditData, IMessageEditData>,
    IGetMessageEditDataHandler, IProcessedHandler
{
    private readonly IOptions<MyTelegramMessengerServerOptions> _options;
    private readonly IQueryProcessor _queryProcessor;

    public GetMessageEditDataHandler(IQueryProcessor queryProcessor,
        IOptions<MyTelegramMessengerServerOptions> options)
    {
        _queryProcessor = queryProcessor;
        _options = options;
    }

    protected override async Task<IMessageEditData> HandleCoreAsync(IRequestInput input,
        RequestGetMessageEditData obj)
    {
        var message = await _queryProcessor
            .ProcessAsync(
                new GetMessageByIdQuery(
                    MessageId.Create(input.UserId, obj.Id).Value),
                default).ConfigureAwait(false);
        var canEdit = message != null && message.Date + _options.Value.EditTimeLimit > CurrentDate;
        return new TMessageEditData { Caption = canEdit };
    }
}
