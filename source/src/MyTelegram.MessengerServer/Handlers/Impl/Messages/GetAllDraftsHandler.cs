using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetAllDraftsHandler : RpcResultObjectHandler<RequestGetAllDrafts, IUpdates>,
    IGetAllDraftsHandler, IProcessedHandler
{
    private readonly IQueryProcessor _queryProcessor;

    public GetAllDraftsHandler(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestGetAllDrafts obj)
    {
        var draftList = await _queryProcessor.ProcessAsync(new GetAllDraftQuery(input.UserId), CancellationToken.None)
            .ConfigureAwait(false);
        var draftUpdates = draftList.Select(p => new TUpdateDraftMessage
        {
            Draft = new TDraftMessage
            {
                Date = p.Draft.Date,
                Message = p.Draft.Message,
                Entities = p.Draft.Entities.ToTObject<TVector<IMessageEntity>>(),
                NoWebpage = p.Draft.NoWebpage,
                ReplyToMsgId = p.Draft.ReplyToMsgId
            },
            TopMsgId = p.Draft.TopMsgId,
            Peer = p.Peer.ToPeer()
        }).ToList();

        return new TUpdates
        {
            Chats = new TVector<IChat>(),
            Date = CurrentDate,
            Users = new TVector<IUser>(),
            Updates = new TVector<IUpdate>(draftUpdates)
        };
    }
}
