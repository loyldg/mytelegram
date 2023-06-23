using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetDialogsHandler : RpcResultObjectHandler<RequestGetDialogs, IDialogs>,
    IGetDialogsHandler, IProcessedHandler
{
    private readonly IDialogAppService _dialogAppService;
    private readonly ITlDialogConverter _dialogConverter;
    private readonly IPeerHelper _peerHelper;
    private readonly IRpcResultProcessor _rpcResultProcessor;

    public GetDialogsHandler(IDialogAppService dialogAppService,
        IRpcResultProcessor rpcResultProcessor,
        IPeerHelper peerHelper,
        ITlDialogConverter dialogConverter)
    {
        _dialogAppService = dialogAppService;
        _rpcResultProcessor = rpcResultProcessor;
        _peerHelper = peerHelper;
        _dialogConverter = dialogConverter;
    }

    protected override async Task<IDialogs> HandleCoreAsync(IRequestInput input,
        RequestGetDialogs obj)
    {
        // Archived Folders
        if (obj.FolderId == 1)
            return new TDialogsNotModified
            {
                Count = 0
            };

        var userId = input.UserId;
        var offsetPeer = _peerHelper.GetPeer(obj.OffsetPeer);
        bool? pinned = null;
        if (obj.ExcludePinned) pinned = false;

        var r = await _dialogAppService.GetDialogsAsync(new GetDialogInput
        {
            FolderId = obj.FolderId,
            Limit = obj.Limit,
            Pinned = pinned,
            //Pinned = !obj.ExcludePinned,
            //ExcludePinned = obj.ExcludePinned,
            OwnerId = userId,
            OffsetPeer = offsetPeer
        });

        return _dialogConverter.ToDialogs(r);
    }
}