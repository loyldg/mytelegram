using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class DeleteMessagesHandler : RpcResultObjectHandler<RequestDeleteMessages, IAffectedMessages>,
    IDeleteMessagesHandler, IProcessedHandler //, IShouldCacheRequest
{
    //private readonly IRequestCacheAppService _requestCacheAppService;
    private readonly ICommandBus _commandBus;
    private readonly IPtsHelper _ptsHelper;

    public DeleteMessagesHandler(
        //IRequestCacheAppService requestCacheAppService,
        ICommandBus commandBus,
        IPtsHelper ptsHelper)
    {
        //_requestCacheAppService = requestCacheAppService;
        _commandBus = commandBus;
        _ptsHelper = ptsHelper;
    }

    protected override async Task<IAffectedMessages> HandleCoreAsync(IRequestInput input,
        RequestDeleteMessages obj)
    {
        // todo:set top message id after delete messages
        if (obj.Id.Count > 0)
        {
            var id = obj.Id.First();
            var command = new StartDeleteMessagesCommand(MessageId.Create(input.UserId, id),
                input.ToRequestInfo(),
                obj.Revoke,
                obj.Id.ToList(),
                //0,
                //0,
                //null,
                Guid.NewGuid());
            await _commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);

            return null!;
        }

        //var pts = await QueryProcessor.ProcessAsync(new GetPtsByPeerIdQuery(input.UserId), CancellationToken.None)
        //    .ConfigureAwait(false);
        var pts = _ptsHelper.GetCachedPts(input.UserId);

        return new TAffectedMessages { Pts = pts, PtsCount = 0 };
    }
}
