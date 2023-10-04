using MyTelegram.Domain.Commands.Channel;
using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class EditBannedHandler : RpcResultObjectHandler<RequestEditBanned, IUpdates>,
    IEditBannedHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;

    public EditBannedHandler(IPeerHelper peerHelper,
        ICommandBus commandBus)
    {
        _peerHelper = peerHelper;
        _commandBus = commandBus;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestEditBanned obj)
    {
        var channel = _peerHelper.GetChannel(obj.Channel);
        var peer = _peerHelper.GetPeer(obj.Participant);
        var bannedRights = ChatBannedRights.FromValue(obj.BannedRights.Flags.ToInt(), obj.BannedRights.UntilDate);
        var command = new EditBannedCommand(ChannelMemberId.Create(channel.PeerId, peer.PeerId),
            input.ReqMsgId,
            input.UserId,
            channel.PeerId,
            peer.PeerId,
            bannedRights);
        await _commandBus.PublishAsync(command, default);
        return null!;
    }
}