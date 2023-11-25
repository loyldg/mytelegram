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
            Projection = new ProjectionDefinitionBuilder<MessageReadModel>()
                .Expression(p => new ReplyToMsgItem(p.OwnerPeerId, p.MessageId)),
        };

        var findOptions2 = new FindOptions<MessageReadModel, ReplyToMsgItem>
        {
            Projection = new ProjectionDefinitionBuilder<MessageReadModel>()
                .Expression(p => new ReplyToMsgItem(p.OwnerPeerId, p.SenderMessageId)),
        };

        switch (query.ToPeer.PeerType)
        {
            case PeerType.User:
                {
                    var cursor = await _store.FindAsync(p =>
                        p.OwnerPeerId == query.SelfUserId && p.ToPeerId == query.ToPeer.PeerId &&
                        p.MessageId == query.ReplyToMsgId, cancellationToken: cancellationToken);
                    var messageReadModel = await cursor.FirstOrDefaultAsync(cancellationToken: cancellationToken);
                    if (messageReadModel == null)
                    {
                        return null;
                    }

                    // Reply to a message sent by ToPeerId
                    if (!messageReadModel.Out)
                    {
                        return new[] { new ReplyToMsgItem(messageReadModel.SenderPeerId, messageReadModel.SenderMessageId) };
                    }

                    // Reply to a message sent by myself
                    var item = await (await _store.FindAsync(p =>
                        p.OwnerPeerId == query.ToPeer.PeerId && p.ToPeerId == query.SelfUserId &&
                        p.SenderMessageId == query.ReplyToMsgId, findOptions, cancellationToken))
                        .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                    return new[] { item };
                }
            case PeerType.Chat:
                {
                    var selfMessageReadModel = await (await _store.FindAsync(p =>
                        p.ToPeerId == query.ToPeer.PeerId && p.OwnerPeerId ==
                            query.SelfUserId && p.MessageId == query.ReplyToMsgId, cancellationToken: cancellationToken))
                        .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                    if (selfMessageReadModel == null)
                    {
                        return null;
                    }

                    var senderUserId = selfMessageReadModel.SenderPeerId;
                    var senderMessageId = selfMessageReadModel.SenderMessageId;

                    var cursor = await _store.FindAsync(p =>
                        p.ToPeerId == query.ToPeer.PeerId && p.SenderPeerId == senderUserId &&
                        p.SenderMessageId == senderMessageId, findOptions, cancellationToken: cancellationToken);


                    return await cursor.ToListAsync(cancellationToken: cancellationToken);
                }
            case PeerType.Channel:
                {
                    var cursor = await _store.FindAsync(p =>
                        p.OwnerPeerId == query.ToPeer.PeerId &&
                        p.MessageId == query.ReplyToMsgId, findOptions, cancellationToken: cancellationToken);

                    return await cursor.ToListAsync(cancellationToken: cancellationToken);
                }
        }
        return null;

        //var cursor = await _store.FindAsync(p =>
        //    p.OwnerPeerId == query.SelfUserId && p.ToPeerId == query.ToPeer.PeerId &&
        //    p.MessageId == query.ReplyToMsgId);


        //var cursor = await _store.FindAsync(p =>
        //    p.ToPeerId == query.ToPeer.PeerId && p.SenderPeerId == query.SenderUserId &&
        //    p.MessageId == query.ReplyToMsgId, findOptions, cancellationToken);

        //return await cursor.ToListAsync(cancellationToken: cancellationToken);
    }
}