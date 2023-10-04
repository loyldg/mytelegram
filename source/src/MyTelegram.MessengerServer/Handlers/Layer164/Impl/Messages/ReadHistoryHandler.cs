using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;
using RequestReadHistory = MyTelegram.Schema.Messages.RequestReadHistory;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class ReadHistoryHandler : RpcResultObjectHandler<RequestReadHistory, IAffectedMessages>,
    IReadHistoryHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;

    public ReadHistoryHandler(ICommandBus commandBus,
        IPeerHelper peerHelper)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
    }

    protected override async Task<IAffectedMessages> HandleCoreAsync(IRequestInput input,
        RequestReadHistory obj)
    {
        var peer = _peerHelper.GetPeer(obj.Peer, input.UserId);
        var selfDialogId = DialogId.Create(input.UserId, peer);

        var readInboxMessageCommand = new ReadInboxMessageCommand2(selfDialogId,
            input.ToRequestInfo(),
            input.UserId,
            input.UserId,
            obj.MaxId,
            peer,
            Guid.NewGuid());
        // Console.WriteLine("SourceId:{0}",readInboxMessageCommand.GetSourceId().Value);
        await _commandBus.PublishAsync(readInboxMessageCommand, CancellationToken.None);

        return null!;
    }
}