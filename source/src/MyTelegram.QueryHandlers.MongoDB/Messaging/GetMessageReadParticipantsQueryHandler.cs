using MongoDB.Driver.Linq;

namespace MyTelegram.QueryHandlers.MongoDB.Messaging;

public class GetMessageReadParticipantsQueryHandler(IQueryOnlyReadModelStore<ReadingHistoryReadModel> store) :
    IQueryHandler<GetMessageReadParticipantsQuery,
    IReadOnlyCollection<IReadingHistoryReadModel>>
{
    public async Task<IReadOnlyCollection<IReadingHistoryReadModel>> ExecuteQueryAsync(GetMessageReadParticipantsQuery query,
        CancellationToken cancellationToken)
    {
        var readingHistoryList = await store.GetAll()
                .Where(p => p.TargetPeerId == query.TargetPeerId &&
                            p.MessageId >= query.MessageId && p.ReaderPeerId != query.SelfUserId)
                .OrderBy(p => p.MessageId)
                .GroupBy(p => p.ReaderPeerId)
                .Select(g => g.First())
                //.ToAsyncEnumerable()
                .ToListAsync(cancellationToken: cancellationToken)
             ;
        return readingHistoryList;
        //return await store.FindAsync(p => p.TargetPeerId == query.TargetPeerId &&
        //                                  p.MessageId == query.MessageId, cancellationToken: cancellationToken);
    }
}
