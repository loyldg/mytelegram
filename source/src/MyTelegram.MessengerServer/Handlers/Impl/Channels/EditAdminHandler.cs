using MyTelegram.Domain.Commands.Channel;
using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class EditAdminHandler : RpcResultObjectHandler<RequestEditAdmin, IUpdates>,
    IEditAdminHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;

    public EditAdminHandler(ICommandBus commandBus,
        IPeerHelper peerHelper)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestEditAdmin obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            var peer = _peerHelper.GetPeer(obj.UserId);
            var rights = obj.AdminRights;
            var command = new EditChannelAdminCommand(ChannelId.Create(inputChannel.ChannelId),
                input.ReqMsgId,
                input.UserId,
                input.UserId,
                false,
                peer.PeerId,
                new ChatAdminRights(rights.ChangeInfo,
                    rights.PostMessages,
                    rights.EditMessages,
                    rights.DeleteMessages,
                    rights.BanUsers,
                    rights.InviteUsers,
                    rights.PinMessages,
                    rights.AddAdmins,
                    rights.Anonymous,
                    rights.ManageCall,
                    rights.Other
                ),
                obj.Rank,
                Guid.NewGuid()
            );
            await _commandBus.PublishAsync(command, CancellationToken.None);
            return null!;
        }

        throw new NotImplementedException();
    }
}
