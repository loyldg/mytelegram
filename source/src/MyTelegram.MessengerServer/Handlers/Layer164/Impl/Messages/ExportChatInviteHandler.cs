using MyTelegram.Domain.Commands.Channel;
using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;
using IExportedChatInvite = MyTelegram.Schema.IExportedChatInvite;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class ExportChatInviteHandler : RpcResultObjectHandler<RequestExportChatInvite, IExportedChatInvite>,
    IExportChatInviteHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;
    private readonly IRandomHelper _randomHelper;

    public ExportChatInviteHandler(ICommandBus commandBus,
        IRandomHelper randomHelper)
    {
        _commandBus = commandBus;
        _randomHelper = randomHelper;
    }

    protected override async Task<IExportedChatInvite> HandleCoreAsync(IRequestInput input,
        RequestExportChatInvite obj)
    {
        if (obj.Peer is TInputPeerChannel inputPeerChannel)
        {
            var command = new ExportChatInviteCommand(ChannelId.Create(inputPeerChannel.ChannelId),
                input.ReqMsgId,
                input.UserId,
                obj.ExpireDate,
                obj.UsageLimit,
                obj.LegacyRevokePermanent,
                _randomHelper.GenerateRandomString(8),
                CurrentDate);
            await _commandBus.PublishAsync(command, CancellationToken.None);
            return null!;
        }

        throw new NotImplementedException();
    }
}