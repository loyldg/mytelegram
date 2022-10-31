using MyTelegram.Domain.Commands.Chat;
using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class DeleteHistoryHandler : RpcResultObjectHandler<RequestDeleteHistory, IAffectedHistory>,
    IDeleteHistoryHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;

    private readonly IPeerHelper _peerHelper;

    //private readonly IRequestCacheAppService _requestCacheAppService;
    private readonly IPtsHelper _ptsHelper;
    private readonly IQueryProcessor _queryProcessor;
    private readonly IRandomHelper _randomHelper;

    public DeleteHistoryHandler(ICommandBus commandBus,
        IRandomHelper randomHelper,
        IPeerHelper peerHelper,
        IQueryProcessor queryProcessor,
        //IRequestCacheAppService requestCacheAppService,
        IPtsHelper ptsHelper)
    {
        _commandBus = commandBus;
        _randomHelper = randomHelper;
        _peerHelper = peerHelper;
        _queryProcessor = queryProcessor;
        //_requestCacheAppService = requestCacheAppService;
        _ptsHelper = ptsHelper;
    }

    protected override async Task<IAffectedHistory> HandleCoreAsync(IRequestInput input,
        RequestDeleteHistory obj)
    {
        var peer = _peerHelper.GetPeer(obj.Peer, input.UserId);
        var pageSize = MyTelegramServerDomainConsts.ClearHistoryDefaultPageSize;
        var messageIdList = await _queryProcessor
            .ProcessAsync(new GetMessageIdListQuery(input.UserId,
                    peer.PeerId,
                    obj.MaxId,
                    pageSize),
                default)
            .ConfigureAwait(false);

        //var nextMaxId = messageIdList.Count == pageSize
        //    ? messageIdList.Min()
        //    : 0;

        if (messageIdList.Count == 0)
        {
            //var command = new ClearHistoryCommand(DialogId.Create(input.UserId, peer),
            //    input.ToRequestInfo(),
            //    obj.Revoke,
            //    new TMessageActionHistoryClear().ToBytes().ToHexString(),
            //    _randomHelper.NextLong(),
            //    messageIdList,
            //    nextMaxId,
            //    Guid.NewGuid());
            //await _commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);

            //_requestCacheAppService.TryRemoveRequest(input.ReqMsgId, out _);
            var cachedPts = _ptsHelper.GetCachedPts(input.UserId);
            return new TAffectedHistory { Offset = 0, Pts = cachedPts, PtsCount = 0 };
        }

        switch (peer.PeerType)
        {
            case PeerType.Chat:
                {
                    var command = new StartDeleteChatMessagesCommand(ChatId.Create(peer.PeerId),
                        input.ToRequestInfo(),
                        messageIdList,
                        obj.Revoke,
                        true,
                        Guid.NewGuid());
                    await _commandBus.PublishAsync(command, default).ConfigureAwait(false);
                }
                break;
            case PeerType.User:
                {
                    var command = new StartDeleteUserMessagesCommand(DialogId.Create(input.UserId, peer),
            input.ToRequestInfo(),
            obj.Revoke,
            messageIdList,
                        true,
            Guid.NewGuid());
                    await _commandBus.PublishAsync(command, default).ConfigureAwait(false);
                }
                break;
        }

        return null!;
    }
}
