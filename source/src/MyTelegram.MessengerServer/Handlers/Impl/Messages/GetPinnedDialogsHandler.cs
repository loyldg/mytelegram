using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetPinnedDialogsHandler : RpcResultObjectHandler<RequestGetPinnedDialogs, IPeerDialogs>,
    IGetPinnedDialogsHandler, IProcessedHandler
{
    private readonly IDialogAppService _dialogAppService;
    private readonly IPtsHelper _ptsHelper;
    private readonly ITlDialogConverter _dialogConverter;

    public GetPinnedDialogsHandler(IDialogAppService dialogAppService,
        IPtsHelper ptsHelper,
        ITlDialogConverter dialogConverter)
    {
        _dialogAppService = dialogAppService;
        _ptsHelper = ptsHelper;
        _dialogConverter = dialogConverter;
    }

    protected override async Task<IPeerDialogs> HandleCoreAsync(IRequestInput input,
        RequestGetPinnedDialogs obj)
    {
        var userId = input.UserId;
        var r = await _dialogAppService
            .GetDialogsAsync(new GetDialogInput {
                Pinned = true, OwnerId = userId, Limit = DefaultPageSize, FolderId = obj.FolderId
            }).ConfigureAwait(false);

        //var pts = await _queryProcessor.ProcessAsync(new GetPtsByPeerIdQuery(input.UserId), default)
        //    .ConfigureAwait(false);
        var cachedPts = _ptsHelper.GetCachedPts(input.UserId);

        //r.PtsReadModel = pts;
        r.CachedPts = cachedPts;

        return _dialogConverter.ToPeerDialogs(r);
    }
}
