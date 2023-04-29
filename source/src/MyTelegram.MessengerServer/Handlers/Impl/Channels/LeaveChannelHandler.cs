using MyTelegram.Domain.Commands.Channel;
using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class LeaveChannelHandler : RpcResultObjectHandler<RequestLeaveChannel,
        IUpdates>,
    ILeaveChannelHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;

    public LeaveChannelHandler(IPeerHelper peerHelper,
        ICommandBus commandBus)
    {
        _peerHelper = peerHelper;
        _commandBus = commandBus;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestLeaveChannel obj)
    {
        var channel = _peerHelper.GetChannel(obj.Channel);
        var command = new LeaveChannelCommand(ChannelMemberId.Create(channel.PeerId, input.UserId),
            input.ReqMsgId,
            channel.PeerId,
            input.UserId);
        await _commandBus.PublishAsync(command, default);

        return null!;
    }
}
