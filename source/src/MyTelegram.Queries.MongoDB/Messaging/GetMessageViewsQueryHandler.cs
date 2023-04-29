namespace MyTelegram.QueryHandlers.MongoDB.Messaging;

// ReSharper disable once UnusedMember.Global
public class GetMessageViewsQueryHandler : IQueryHandler<GetMessageViewsQuery, IReadOnlyCollection<MessageView>>
{
    private readonly IMyMongoDbReadModelStore<MessageReadModel> _store;

    public GetMessageViewsQueryHandler(IMyMongoDbReadModelStore<MessageReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<MessageView>> ExecuteQueryAsync(GetMessageViewsQuery query,
        CancellationToken cancellationToken)
    {
        var findOptions = new FindOptions<MessageReadModel, MessageView> {
            Projection = new ProjectionDefinitionBuilder<MessageReadModel>().Expression(p =>
                new MessageView { MessageId = p.MessageId, Views = p.Views ?? 0 })
        };
        var cursor = await _store
            .FindAsync(p => p.OwnerPeerId == query.ChannelId && query.MessageIdList.Contains(p.MessageId),
                findOptions,
                cancellationToken)
            ;
        return await cursor.ToListAsync(cancellationToken);
    }
}
