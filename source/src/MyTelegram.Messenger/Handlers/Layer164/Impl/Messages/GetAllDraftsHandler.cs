// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Return all message <a href="https://corefork.telegram.org/api/drafts">drafts</a>.<br>
/// Returns all the latest <a href="https://corefork.telegram.org/constructor/updateDraftMessage">updateDraftMessage</a> updates related to all chats with drafts.
/// See <a href="https://corefork.telegram.org/method/messages.getAllDrafts" />
///</summary>
internal sealed class GetAllDraftsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetAllDrafts, MyTelegram.Schema.IUpdates>,
    Messages.IGetAllDraftsHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly ILayeredService<IUpdatesConverter> _layeredService;

    public GetAllDraftsHandler(IQueryProcessor queryProcessor,
        ILayeredService<IUpdatesConverter> layeredService)
    {
        _queryProcessor = queryProcessor;
        _layeredService = layeredService;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestGetAllDrafts obj)
    {
        var draftList = await _queryProcessor.ProcessAsync(new GetAllDraftQuery(input.UserId), CancellationToken.None)
            ;

        return _layeredService.GetConverter(input.Layer).ToDraftsUpdates(draftList);
    }
}
