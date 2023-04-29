using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetPeerDialogsHandler : RpcResultObjectHandler<RequestGetPeerDialogs, IPeerDialogs>,
    IGetPeerDialogsHandler, IProcessedHandler
{
    private readonly IDialogAppService _dialogAppService;
    private readonly IPeerHelper _peerHelper;
    private readonly IPtsHelper _ptsHelper;
    private readonly ITlDialogConverter _dialogConverter;
    public GetPeerDialogsHandler(IDialogAppService dialogAppService,
        IPeerHelper peerHelper,
        IPtsHelper ptsHelper,
        ITlDialogConverter dialogConverter)
    {
        _dialogAppService = dialogAppService;
        _peerHelper = peerHelper;
        _ptsHelper = ptsHelper;
        _dialogConverter = dialogConverter;
    }

    protected override async Task<IPeerDialogs> HandleCoreAsync(IRequestInput input,
        RequestGetPeerDialogs obj)
    {
        // Console.WriteLine($"######## {input.UserId} GetPeerDialogs ,req={JsonSerializer.Serialize(obj)}");
        var userId = input.UserId; // await GetUidAsync(input);
        var peerList = new List<Peer>();

        foreach (var inputDialogPeer in obj.Peers)
        {
            if (inputDialogPeer is TInputDialogPeer dialogPeer)
            {
                if (dialogPeer.Peer is TInputPeerSelf || dialogPeer.Peer is TInputPeerEmpty)
                {
                    continue;
                }

                var peer = _peerHelper.GetPeer(dialogPeer.Peer, userId);
                // Console.WriteLine($"Add peer to list:{peer} {dialogPeer.Peer.GetType().Name}");

                if (peer.PeerId != userId)
                {
                    peerList.Add(peer);
                }
                //Logger.LogInformation($"get peer dialogs:{peer}");
            }
        }

        var limit = peerList.Count == 0 ? 10 : peerList.Count;
        var output = await _dialogAppService
            .GetDialogsAsync(new GetDialogInput
            {
                OwnerId = userId,
                Limit = limit,
                PeerIdList = peerList.Select(p => p.PeerId).ToList()
            });
        //var pts = await _queryProcessor.ProcessAsync(new GetPtsByPeerIdQuery(input.UserId), default)
        //    ;
        var cachedPts = _ptsHelper.GetCachedPts(input.UserId);

        //output.PtsReadModel = pts;
        output.CachedPts = cachedPts;
        // Console.WriteLine($"Get Peer Dialogs:{userId} peerList={JsonConvert.SerializeObject(peerList)}");

        return _dialogConverter.ToPeerDialogs(output);
    }
}
