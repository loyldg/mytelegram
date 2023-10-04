// ReSharper disable All

using MyTelegram.Domain.Commands.Channel;
using MyTelegram.Schema.Channels;
using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Channels;

public class DeleteParticipantHistoryHandler :
    RpcResultObjectHandler<RequestDeleteParticipantHistory, Schema.Messages.IAffectedHistory>,
    Channels.IDeleteParticipantHistoryHandler, IProcessedHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;
    private readonly IPtsHelper _ptsHelper;
    private readonly IQueryProcessor _queryProcessor;

    public DeleteParticipantHistoryHandler(IQueryProcessor queryProcessor,
        ICommandBus commandBus,
        IPeerHelper peerHelper,
        IPtsHelper ptsHelper)
    {
        _queryProcessor = queryProcessor;
        _commandBus = commandBus;
        _peerHelper = peerHelper;
        _ptsHelper = ptsHelper;
    }

    protected override async Task<Schema.Messages.IAffectedHistory> HandleCoreAsync(IRequestInput input,
        RequestDeleteParticipantHistory obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            var peer = _peerHelper.GetPeer(obj.Participant);
            var messageIds = await _queryProcessor
                .ProcessAsync(new GetMessageIdListByUserIdQuery(inputChannel.ChannelId,
                        peer.PeerId,
                        MyTelegramServerDomainConsts.ClearHistoryDefaultPageSize),
                    default);
            if (messageIds.Count > 0)
            {
                var command = new StartDeleteParticipantHistoryCommand(ChannelId.Create(inputChannel.ChannelId),
                    input.ToRequestInfo(),
                    messageIds.ToList(),
                    Guid.NewGuid()
                );
                await _commandBus.PublishAsync(command, default);
                return null!;
            }
            else
            {
                return new TAffectedHistory
                {
                    Pts = _ptsHelper.GetCachedPts(inputChannel.ChannelId),
                    PtsCount = 0,
                    Offset = 0
                };
            }
        }

        throw new NotImplementedException();
    }
}