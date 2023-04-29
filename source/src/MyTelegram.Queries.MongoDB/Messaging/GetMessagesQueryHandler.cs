namespace MyTelegram.QueryHandlers.MongoDB.Messaging;

// ReSharper disable once UnusedMember.Global
public class
    GetMessagesQueryHandler : IQueryHandler<GetMessagesQuery, IReadOnlyCollection<IMessageReadModel>>
{
    private readonly IMongoDbReadModelStore<MessageReadModel> _store;

    public GetMessagesQueryHandler(IMongoDbReadModelStore<MessageReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IMessageReadModel>> ExecuteQueryAsync(GetMessagesQuery query,
        CancellationToken cancellationToken)
    {
        //var filter = Builders<MessageReadModel>.Filter.Where(p => p.Out);
        Expression<Func<MessageReadModel, bool>> predicate = x => x.OwnerPeerId == query.OwnerPeerId;
        predicate = predicate.WhereIf(query.Q?.Length > 2, p => p.Message.Contains(query.Q!))
                .WhereIf(
                    query.MessageType != MessageType.Unknown && query.MessageType != MessageType.Pinned,
                    p => p.MessageType == query.MessageType)
                .WhereIf(query.MessageType == MessageType.Pinned, p => p.Pinned)
                .WhereIf(query.MessageIdList?.Count > 0, p => query.MessageIdList!.Contains(p.MessageId))
                .WhereIf(query.ChannelHistoryMinId > 0, p => p.MessageId > query.ChannelHistoryMinId)
                .WhereIf(query.Offset?.LoadType == LoadType.Forward, p => p.MessageId > query.Offset!.FromId)
                .WhereIf(query.Offset?.MaxId > 0, p => p.MessageId < query.Offset!.MaxId)
                .WhereIf(query.Pts > 0, p => p.Pts > query.Pts)
                .WhereIf(query.Peer != null,
                    p => p.ToPeerType == query.Peer!.PeerType && p.ToPeerId == query.Peer.PeerId)
                .WhereIf(query.ReplyToMsgId>0,p=>p.ReplyToMsgId==query.ReplyToMsgId)
            ;
        var options = new FindOptions<MessageReadModel, MessageReadModel>
        {
            Limit = query.Limit,
            Skip = 0,
            Sort = Builders<MessageReadModel>.Sort.Descending(p => p.MessageId)
        };

        var cursor = await _store.FindAsync(predicate, options, cancellationToken);

        return await cursor.ToListAsync(cancellationToken);
    }
}
