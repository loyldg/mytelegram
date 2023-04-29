using MyTelegram.Domain.Commands.Channel;
using MyTelegram.Domain.Commands.Chat;
using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class EditChatAboutHandler : RpcResultObjectHandler<RequestEditChatAbout, IBool>,
    IEditChatAboutHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;

    public EditChatAboutHandler(ICommandBus commandBus,
        IPeerHelper peerHelper)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestEditChatAbout obj)
    {
        var peer = _peerHelper.GetPeer(obj.Peer, input.UserId);
        switch (peer.PeerType)
        {
            case PeerType.Channel:
            {
                var command =
                    new EditChannelAboutCommand(ChannelId.Create(peer.PeerId), input.ReqMsgId, input.UserId, obj.About);
                await _commandBus.PublishAsync(command, CancellationToken.None);
                //return new TBoolTrue();
                return null!;
            }
            case PeerType.Chat:
            {
                var command =
                    new EditChatAboutCommand(ChatId.Create(peer.PeerId), input.ReqMsgId, input.UserId, obj.About);
                await _commandBus.PublishAsync(command, CancellationToken.None);
                return null!;
            }
        }

        throw new NotImplementedException();
    }
}
