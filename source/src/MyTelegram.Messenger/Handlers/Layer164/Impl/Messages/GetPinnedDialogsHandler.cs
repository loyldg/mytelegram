// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get pinned dialogs
/// <para>Possible errors</para>
/// Code Type Description
/// 400 FOLDER_ID_INVALID Invalid folder ID.
/// See <a href="https://corefork.telegram.org/method/messages.getPinnedDialogs" />
///</summary>
internal sealed class GetPinnedDialogsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetPinnedDialogs, MyTelegram.Schema.Messages.IPeerDialogs>,
    Messages.IGetPinnedDialogsHandler
{
    private readonly IDialogAppService _dialogAppService;
    private readonly IPtsHelper _ptsHelper;
    private readonly ILayeredService<IDialogConverter> _layeredService;
    public GetPinnedDialogsHandler(IDialogAppService dialogAppService,
        IPtsHelper ptsHelper,
        ILayeredService<IDialogConverter> layeredService)
    {
        _dialogAppService = dialogAppService;
        _ptsHelper = ptsHelper;
        _layeredService = layeredService;
    }

    protected override async Task<IPeerDialogs> HandleCoreAsync(IRequestInput input,
        RequestGetPinnedDialogs obj)
    {
        var userId = input.UserId;
        var r = await _dialogAppService
            .GetDialogsAsync(new GetDialogInput
            {
                Pinned = true,
                OwnerId = userId,
                Limit = DefaultPageSize,
                FolderId = obj.FolderId
            });

        //var pts = await _queryProcessor.ProcessAsync(new GetPtsByPeerIdQuery(input.UserId), default)
        //    .;
        var cachedPts = _ptsHelper.GetCachedPts(input.UserId);

        //r.PtsReadModel = pts;
        r.CachedPts = cachedPts;

        return _layeredService.GetConverter(input.Layer).ToPeerDialogs(r);
    }
}
