// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get dialog info of specified peers
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getPeerDialogs" />
///</summary>
internal sealed class GetPeerDialogsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetPeerDialogs, MyTelegram.Schema.Messages.IPeerDialogs>,
    Messages.IGetPeerDialogsHandler
{
    private readonly IDialogAppService _dialogAppService;
    private readonly IPeerHelper _peerHelper;
    private readonly IPtsHelper _ptsHelper;
    private readonly ILayeredService<IDialogConverter> _layeredService;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly ILogger<GetPeerDialogsHandler> _logger;
    private readonly IQueryProcessor _queryProcessor;
    public GetPeerDialogsHandler(IDialogAppService dialogAppService,
        IPeerHelper peerHelper,
        IPtsHelper ptsHelper,
        ILayeredService<IDialogConverter> layeredService,
        IAccessHashHelper accessHashHelper, ILogger<GetPeerDialogsHandler> logger, IQueryProcessor queryProcessor)
    {
        _dialogAppService = dialogAppService;
        _peerHelper = peerHelper;
        _ptsHelper = ptsHelper;
        _layeredService = layeredService;
        _accessHashHelper = accessHashHelper;
        _logger = logger;
        _queryProcessor = queryProcessor;
    }

    protected override async Task<IPeerDialogs> HandleCoreAsync(IRequestInput input,
        RequestGetPeerDialogs obj)
    {
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
                if (_peerHelper.IsEncryptedDialogPeer(peer.PeerId))
                {
                    continue;
                }

                await _accessHashHelper.CheckAccessHashAsync(dialogPeer.Peer);

                if (peer.PeerId != userId)
                {
                    peerList.Add(peer);
                }
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
        var pts = await _queryProcessor.ProcessAsync(new GetPtsByPeerIdQuery(input.UserId), default);
        var cachedPts = _ptsHelper.GetCachedPts(input.UserId);

        output.PtsReadModel = pts;
        output.CachedPts = cachedPts;
        var r = _layeredService.GetConverter(input.Layer).ToPeerDialogs(output);

        foreach (var dialog in r.Dialogs)
        {
            switch (dialog)
            {
                case Schema.TDialog d:
                    var m = output.MessageList.FirstOrDefault(p => p.MessageId == d.TopMessage);
                    break;
                case TDialogFolder dialogFolder:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dialog));
            }
        }

        return r;
    }
}
