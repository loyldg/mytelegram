using MyTelegram.Domain.Commands.Channel;
using MyTelegram.Domain.Commands.Chat;
using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class EditChatDefaultBannedRightsHandler : RpcResultObjectHandler<RequestEditChatDefaultBannedRights, IUpdates>,
    IEditChatDefaultBannedRightsHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;

    public EditChatDefaultBannedRightsHandler(ICommandBus commandBus,
        IPeerHelper peerHelper)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestEditChatDefaultBannedRights obj)
    {
        var peer = _peerHelper.GetPeer(obj.Peer, input.UserId);
        switch (peer.PeerType)
        {
            case PeerType.Chat:
            {
                var rights = obj.BannedRights;
                var command = new EditChatDefaultBannedRightsCommand(ChatId.Create(peer.PeerId),
                    input.ReqMsgId,
                    new ChatBannedRights(rights.ViewMessages,
                        rights.SendMessages,
                        rights.SendMedia,
                        rights.SendStickers,
                        rights.SendGifs,
                        rights.SendGames,
                        rights.SendInline,
                        rights.EmbedLinks,
                        rights.SendPolls,
                        rights.ChangeInfo,
                        rights.InviteUsers,
                        rights.PinMessages,
                        rights.UntilDate
                    ),
                    input.UserId);
                await _commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);
            }
                break;
            case PeerType.Channel:
            {
                var rights = obj.BannedRights;
                var command = new EditChannelDefaultBannedRightsCommand(ChannelId.Create(peer.PeerId),
                    input.ReqMsgId,
                    new ChatBannedRights(rights.ViewMessages,
                        rights.SendMessages,
                        rights.SendMedia,
                        rights.SendStickers,
                        rights.SendGifs,
                        rights.SendGames,
                        rights.SendInline,
                        rights.EmbedLinks,
                        rights.SendPolls,
                        rights.ChangeInfo,
                        rights.InviteUsers,
                        rights.PinMessages,
                        rights.UntilDate
                    ),
                    input.UserId);
                await _commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);
            }
                break;
        }

        return null!;
    }
}
