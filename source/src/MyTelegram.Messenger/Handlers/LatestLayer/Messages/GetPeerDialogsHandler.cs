using MyTelegram.Schema.Updates;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

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
internal sealed class GetPeerDialogsHandler(
    IDialogAppService dialogAppService,
    IPeerHelper peerHelper,
    IPtsHelper ptsHelper,
    IAccessHashHelper accessHashHelper,
    IQueryProcessor queryProcessor,
    IUserConverterService userConverterService,
    IDialogConverterService dialogConverterService)
    : RpcResultObjectHandler<RequestGetPeerDialogs, IPeerDialogs>
{
    protected override async Task<IPeerDialogs> HandleCoreAsync(IRequestInput input,
        RequestGetPeerDialogs obj)
    {
        var userId = input.UserId;
        var peerList = new List<Peer>();
        foreach (var inputDialogPeer in obj.Peers)
        {
            switch (inputDialogPeer)
            {
                case TInputDialogPeer dialogPeer when dialogPeer.Peer is TInputPeerSelf || dialogPeer.Peer is TInputPeerEmpty:
                    continue;
                case TInputDialogPeer dialogPeer:
                    {
                        var peer = peerHelper.GetPeer(dialogPeer.Peer, userId);
                        if (peerHelper.IsEncryptedDialogPeer(peer.PeerId))
                        {
                            continue;
                        }

                        var shouldCheckAccessHash = true;
                        switch (dialogPeer.Peer)
                        {
                            case TInputPeerUser inputPeerUser:
                                if (inputPeerUser.UserId != input.UserId)
                                {
                                    shouldCheckAccessHash = false;
                                }
                                break;
                        }

                        if (shouldCheckAccessHash)
                        {
                            await accessHashHelper.CheckAccessHashAsync(input, dialogPeer.Peer);
                        }

                        peerList.Add(peer);
                        break;
                    }
                case TInputDialogPeerFolder inputDialogPeerFolder:
                    {
                        var peers = await queryProcessor.ProcessAsync(
                            new GetDialogsByFolderIdQuery(userId, inputDialogPeerFolder.FolderId));
                        peerList.AddRange(peers);
                    }
                    break;
            }
        }

        var limit = peerList.Count == 0 ? 10 : peerList.Count;
        var output = await dialogAppService
            .GetDialogsAsync(new GetDialogInput
            {
                OwnerId = userId,
                Limit = limit,
                PeerIdList = peerList.Select(p => p.PeerId).ToList()
            });
        var pts = await queryProcessor.ProcessAsync(new GetPtsByPeerIdQuery(input.UserId));
        var cachedPts = ptsHelper.GetCachedPts(input.UserId);

        output.PtsReadModel = pts;
        output.CachedPts = cachedPts;
        var peerDialogs = dialogConverterService.ToPeerDialogs(input, output, input.Layer);

        foreach (var dialog in peerDialogs.Dialogs)
        {
            switch (dialog)
            {
                case TDialog d:
                    var m = output.MessageList.FirstOrDefault(p => p.MessageId == d.TopMessage);
                    break;
                case TDialogFolder dialogFolder:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dialog));
            }
        }

        if (peerDialogs.Dialogs.Count == 0)
        {
            var userIds = peerList.Where(p => p.PeerType == PeerType.User).Select(p => p.PeerId).Distinct().ToList();
            var users = await userConverterService.GetUserListAsync(input, userIds, false, false, input.Layer);

            var channels = output.ChannelList.ToDictionary(k => k.ChannelId, v => v);
            peerDialogs.Dialogs = [.. peerList.Select(p =>
            {
                var d = new TDialog
                {
                    Peer = p.ToPeer(),
                    NotifySettings = new TPeerNotifySettings(),
                };
                if (p.PeerType == PeerType.Channel)
                {
                    if (channels.TryGetValue(p.PeerId, out var channel))
                    {
                        d.TopMessage = channel.TopMessageId;
                        d.Pts = channel.Pts;
                    }
                }

                return d;
            })];

            if (peerDialogs.Users == null)
            {
                peerDialogs.Users = [];
            }

            foreach (var user in users)
            {
                peerDialogs.Users.Add(user);
            }
        }

        var ptsCacheItem = await ptsHelper.GetPtsForUserAsync(input.UserId);
        peerDialogs.State = new TState
        {
            Pts = ptsCacheItem.Pts,
            Qts = ptsCacheItem.Qts,
            UnreadCount = ptsCacheItem.UnreadCount,
            Date = ptsCacheItem.Date
        };

        return peerDialogs;
    }
}
