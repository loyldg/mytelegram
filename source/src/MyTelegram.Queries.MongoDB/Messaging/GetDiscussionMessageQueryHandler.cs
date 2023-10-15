using MyTelegram.Domain.ValueObjects;

namespace MyTelegram.QueryHandlers.MongoDB.Messaging;

public class GetDiscussionMessageQueryHandler : IQueryHandler<GetDiscussionMessageQuery, IMessageReadModel?>
{
    private readonly IMongoDbReadModelStore<MessageReadModel> _store;

    public GetDiscussionMessageQueryHandler(IMongoDbReadModelStore<MessageReadModel> store)
    {
        _store = store;
    }

    public async Task<IMessageReadModel?> ExecuteQueryAsync(GetDiscussionMessageQuery query,
        CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p =>
                p.FwdHeader!.SavedFromPeer!.PeerId == query.SavedFromPeerId &&
                p.FwdHeader.SavedFromMsgId == query.SavedFromMessageId,
            cancellationToken: cancellationToken);
        return await cursor.FirstOrDefaultAsync(cancellationToken);
    }
}

public class GetUnreadCountQueryHandler : IQueryHandler<GetUnreadCountQuery, int>
{
    private readonly IMyMongoDbReadModelStore<MessageReadModel> _store;

    public GetUnreadCountQueryHandler(IMyMongoDbReadModelStore<MessageReadModel> store)
    {
        _store = store;
    }

    public async Task<int> ExecuteQueryAsync(GetUnreadCountQuery query, CancellationToken cancellationToken)
    {

        return 0;
        //var count = await _store.CountAsync(p =>
        //    p.OwnerPeerId == query.OwnerUserId && p.ToPeerId == query.ToPeerId && p.MessageId > query.MaxMessageId, cancellationToken);

        //return (int)count;
    }
}

public class
    GetReplyToMsgIdListQueryHandler : IQueryHandler<GetReplyToMsgIdListQuery, IReadOnlyCollection<ReplyToMsgItem>?>
{
    private readonly IMyMongoDbReadModelStore<MessageReadModel> _store;

    public GetReplyToMsgIdListQueryHandler(IMyMongoDbReadModelStore<MessageReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<ReplyToMsgItem>?> ExecuteQueryAsync(GetReplyToMsgIdListQuery query, CancellationToken cancellationToken)
    {
        if (!query.ReplyToMsgId.HasValue)
        {
            return null;
        }

        var findOptions = new FindOptions<MessageReadModel, ReplyToMsgItem>
        {
            Projection = new ProjectionDefinitionBuilder<MessageReadModel>().Expression(p => new ReplyToMsgItem(p.OwnerPeerId, p.MessageId)),
        };

        var cursor = await _store.FindAsync(p =>
            p.ToPeerId == query.ToPeerId && p.SenderPeerId == query.SenderUserId &&
            p.MessageId == query.ReplyToMsgId, findOptions, cancellationToken);

        return await cursor.ToListAsync(cancellationToken: cancellationToken);
    }
}