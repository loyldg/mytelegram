namespace MyTelegram.QueryHandlers.MongoDB.Messaging;

public class GetPollIdByMessageIdQueryHandler : IQueryHandler<GetPollIdByMessageIdQuery, long?>
{
    private readonly IMyMongoDbReadModelStore<MessageReadModel> _store;

    public GetPollIdByMessageIdQueryHandler(IMyMongoDbReadModelStore<MessageReadModel> store)
    {
        _store = store;
    }

    public async Task<long?> ExecuteQueryAsync(GetPollIdByMessageIdQuery query,
        CancellationToken cancellationToken)
    {
        var findOptions = new FindOptions<MessageReadModel, long?>
        {
            Projection = new ProjectionDefinitionBuilder<MessageReadModel>().Expression(p => p.PollId),
            Limit = 1
        };
        var cursor = await _store.FindAsync(p => p.ToPeerId == query.PeerId && p.MessageId == query.MessageId, findOptions, cancellationToken)
            ;

        return await cursor.FirstOrDefaultAsync(cancellationToken);
    }
}
