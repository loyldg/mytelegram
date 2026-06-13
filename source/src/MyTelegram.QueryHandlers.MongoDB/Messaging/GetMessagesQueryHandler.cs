using MyTelegram.Domain.Aggregates.Messaging;
using MyTelegram.ReadModel.MongoDB;

namespace MyTelegram.QueryHandlers.MongoDB.Messaging;

// ReSharper disable once UnusedMember.Global
public class
    GetMessagesQueryHandler(IQueryOnlyReadModelStore<MessageReadModel> store,
        IQueryOnlyReadModelStore<MessageTokenReadModel> messageTokenStore) : IQueryHandler<GetMessagesQuery, IReadOnlyCollection<IMessageReadModel>>
{
    public async Task<IReadOnlyCollection<IMessageReadModel>> ExecuteQueryAsync(GetMessagesQuery query,
        CancellationToken cancellationToken)
    {
        if (query.Tokens?.Count > 0)
        {
            return await SearchByKeywordAsync(query, cancellationToken);
        }

        //var filter = Builders<MessageReadModel>.Filter.Where(p => p.Out);
        Expression<Func<MessageReadModel, bool>> predicate;
        if (query.IsSearchGlobal)
        {
            predicate = x => x.OwnerPeerId == query.OwnerPeerId;
            if (query.JoinedChannelIdList?.Count > 0)
            {
                predicate = predicate.Or(p => query.JoinedChannelIdList.Contains(p.OwnerPeerId));
            }
        }
        else
        {
            predicate = x => x.OwnerPeerId == query.OwnerPeerId;
        }

        predicate = predicate
                .WhereIf(query.MessageActionType == null, p => p.MessageActionType != MessageActionType.CreateQuickReplyMessage)
                .WhereIf(query.MessageActionType.HasValue, p => p.MessageActionType == query.MessageActionType)
                //.WhereIf(query.Q?.Length > 2, p => p.Message.Contains(query.Q!))
                .WhereIf(query.Q?.Length > 0, p => p.Message.Contains(query.Q!))
                .WhereIf(
                    query.MessageType != MessageType.Unknown && query.MessageType != MessageType.Pinned,
                    p => p.MessageType == query.MessageType)
                .WhereIf(query.MessageType == MessageType.Pinned, p => p.Pinned)
                .WhereIf(query.MessageIdList?.Count > 0, p => query.MessageIdList!.Contains(p.MessageId))
                .WhereIf(query.ChannelHistoryMinId > 0, p => p.MessageId > query.ChannelHistoryMinId)
                //.WhereIf(query.Offset?.LoadType == LoadType.Forward, p => p.MessageId > query.Offset!.FromId)
                //.WhereIf(query.Offset?.MaxId > 0, p => p.MessageId < query.Offset!.MaxId)
                .WhereIf(query.Offset is { LoadType: LoadType.Backward, MaxId: > 0 }, p => p.MessageId < query.Offset!.MaxId)
                //.WhereIf(query.Offset is { LoadType: LoadType.AroundMessage, MaxId: > 0 }, p => p.MessageId < query.Offset!.MaxId)
                .WhereIf(query.Offset?.LoadType == LoadType.Forward, p => p.MessageId > query.Offset!.FromId)
                .WhereIf(query.Pts > 0, p => p.Pts > query.Pts)
                .WhereIf(query.Peer != null && query.Peer.PeerType != PeerType.Empty,
                    p => p.ToPeerType == query.Peer!.PeerType && p.ToPeerId == query.Peer.PeerId)
                .WhereIf(query.ReplyToMsgId > 0, p => p.ReplyToMsgId == query.ReplyToMsgId)
                .WhereIf(query.BroadcastsOnly, p => p.ToPeerType == PeerType.Channel && p.Post)
                .WhereIf(query.GroupsOnly, p => p.ToPeerType == PeerType.Channel && !p.Post)
                .WhereIf(query.UsersOnly, p => p.ToPeerType == PeerType.User)
            ;

        // Since the message IDs are not continuous, for example (1,2,3,100,101,102,1001,1002), it is unable to get the correct min/max message ID here.
        // We need to query twice to get the correct data.
        if (query.Offset?.LoadType == LoadType.AroundMessage)
        {
            var aroundMessageId = query.Offset.OffsetId;
            var predicate1 = predicate.And(p => p.MessageId <= aroundMessageId);
            var sortOptions1 = new SortOptions<MessageReadModel>(p => p.MessageId, SortType.Descending);
            var predicate2 = predicate.And(p => p.MessageId > aroundMessageId);
            var sortOptions2 = new SortOptions<MessageReadModel>(p => p.MessageId, SortType.Ascending);
            var limit = query.Limit / 2;

            var part1Result = await store.FindAsync(predicate1,
                0,
                limit,
                sort: sortOptions1,
                cancellationToken: cancellationToken);

            var part2Result = await store.FindAsync(predicate2,
                0,
                limit,
                sort: sortOptions2,
                cancellationToken: cancellationToken);

            return part1Result.Concat(part2Result).OrderByDescending(p => p.MessageId).ToList();
        }

        var sortOptions = new SortOptions<MessageReadModel>(p => p.MessageId, SortType.Descending);
        if (query.Offset?.LoadType == LoadType.Forward)
        {
            sortOptions = new(p => p.MessageId, SortType.Ascending);
        }

        var result = await store.FindAsync(predicate,
               0,
               query.Limit,
               sort: sortOptions,
               cancellationToken: cancellationToken);

        return result.OrderByDescending(p => p.MessageId).ToList();
    }


    private async Task<IReadOnlyCollection<IMessageReadModel>> SearchByKeywordAsync(GetMessagesQuery query, CancellationToken cancellationToken)
    {
        //var filter = Builders<MessageReadModel>.Filter.Where(p => p.Out);
        Expression<Func<MessageTokenReadModel, bool>> predicate;
        if (query.IsSearchGlobal)
        {
            predicate = x => x.OwnerPeerId == query.OwnerPeerId;
            if (query.JoinedChannelIdList?.Count > 0)
            {
                predicate = predicate.Or(p => query.JoinedChannelIdList.Contains(p.OwnerPeerId));
            }
        }
        else
        {
            predicate = x => x.OwnerPeerId == query.OwnerPeerId;
        }

        predicate = predicate
                .WhereIf(query.Tokens?.Count > 0, p => query.Tokens!.Any(x => p.Tokens.Contains(x)))
                //.WhereIf(query.Q?.Length > 2, p => p.Message.Contains(query.Q!))
                //.WhereIf(query.Q?.Length > 0, p => p.Message.Contains(query.Q!))
                .WhereIf(
                    query.MessageType != MessageType.Unknown && query.MessageType != MessageType.Pinned,
                    p => p.MessageType == query.MessageType)
                .WhereIf(query.MessageType == MessageType.Pinned, p => p.Pinned)
                .WhereIf(query.MessageIdList?.Count > 0, p => query.MessageIdList!.Contains(p.MessageId))
                .WhereIf(query.ChannelHistoryMinId > 0, p => p.MessageId > query.ChannelHistoryMinId)
                //.WhereIf(query.Offset?.LoadType == LoadType.Forward, p => p.MessageId > query.Offset!.FromId)
                //.WhereIf(query.Offset?.MaxId > 0, p => p.MessageId < query.Offset!.MaxId)
                .WhereIf(query.Offset is { LoadType: LoadType.Backward, MaxId: > 0 }, p => p.MessageId < query.Offset!.MaxId)
                //.WhereIf(query.Offset is { LoadType: LoadType.AroundMessage, MaxId: > 0 }, p => p.MessageId < query.Offset!.MaxId)
                .WhereIf(query.Offset?.LoadType == LoadType.Forward, p => p.MessageId > query.Offset!.FromId)
                //.WhereIf(query.Pts > 0, p => p.Pts > query.Pts)
                .WhereIf(query.Peer != null && query.Peer.PeerType != PeerType.Empty,
                    p => p.ToPeerType == query.Peer!.PeerType && p.ToPeerId == query.Peer.PeerId)
                .WhereIf(query.ReplyToMsgId > 0, p => p.ReplyToMsgId == query.ReplyToMsgId)
                .WhereIf(query.BroadcastsOnly, p => p.ToPeerType == PeerType.Channel && p.Post)
                .WhereIf(query.GroupsOnly, p => p.ToPeerType == PeerType.Channel && !p.Post)
                .WhereIf(query.UsersOnly, p => p.ToPeerType == PeerType.User)
                
                
            ;

        // Since the message IDs are not continuous, for example (1,2,3,100,101,102,1001,1002), it is unable to get the correct min/max message ID here.
        // We need to query twice to get the correct data.
        if (query.Offset?.LoadType == LoadType.AroundMessage)
        {
            var aroundMessageId = query.Offset.OffsetId;
            var predicate1 = predicate.And(p => p.MessageId <= aroundMessageId);
            var sortOptions1 = new SortOptions<MessageTokenReadModel>(p => p.MessageId, SortType.Descending);
            var predicate2 = predicate.And(p => p.MessageId > aroundMessageId);
            var sortOptions2 = new SortOptions<MessageTokenReadModel>(p => p.MessageId, SortType.Ascending);
            var limit = query.Limit / 2;

            var part1Result = await messageTokenStore.FindAsync(predicate1,
                p => new TempMessageToken(p.OwnerPeerId, p.MessageId),
                0,
                limit,
                sort: sortOptions1,
                cancellationToken: cancellationToken);

            var part2Result = await messageTokenStore.FindAsync(predicate2,
                p => new TempMessageToken(p.OwnerPeerId, p.MessageId),
                0,
                limit,
                sort: sortOptions2,
                cancellationToken: cancellationToken);

            var allResults = part1Result.Concat(part2Result).OrderByDescending(p => p.MessageId).ToList();
            var messageIds = allResults.Select(p => MessageId.Create(p.OwnerPeerId, p.MessageId).Value);

            return await store.FindAsync(p => messageIds.Contains(p.Id), cancellationToken: cancellationToken);
        }
        else
        {
            var sortOptions = new SortOptions<MessageTokenReadModel>(p => p.MessageId, SortType.Descending);
            if (query.Offset?.LoadType == LoadType.Forward)
            {
                sortOptions = new(p => p.MessageId, SortType.Ascending);
            }

            var result = await messageTokenStore.FindAsync(predicate,
                p => new TempMessageToken(p.OwnerPeerId, p.MessageId),
                0,
                query.Limit,
                sort: sortOptions,
                cancellationToken: cancellationToken);

            var messageTokenResult = result.OrderByDescending(p => p.MessageId).ToList();

            var messageIds = messageTokenResult.Select(p => MessageId.Create(p.OwnerPeerId, p.MessageId).Value);

            return await store.FindAsync(p => messageIds.Contains(p.Id), cancellationToken: cancellationToken);
        }
    }
}
public record TempMessageToken(long OwnerPeerId, int MessageId);
