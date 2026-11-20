using MyTelegram.Domain.Aggregates.Messaging;
using MyTelegram.ReadModel.Impl;

namespace MyTelegram.QueryHandlers.InMemory.Messaging;

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

        var predicate = BuildPredicate(query);

        if (query.IsSearchGlobal)
        {
            // Message ids of different peers are unrelated, so searchGlobal pages by (offset_rate, offset_id) instead of offset_id/add_offset.
            if (query.Offset != null)
            {
                predicate = OffsetPagination.ApplyFilters(predicate, query.Offset, p => p.MessageId, p => p.Date);
            }

            return await store.FindAsync(predicate,
                0,
                query.Offset?.Limit ?? query.Limit,
                sort: new SortOptions<MessageReadModel>(p => p.Date).ThenBy(p => p.MessageId, SortType.Descending),
                cancellationToken: cancellationToken);
        }

        if (query.Offset != null)
        {
            return await OffsetPagination.FindAsync(query.Offset,
                predicate,
                p => p.MessageId,
                p => p.Date,
                (filter, skip, limit, ascending) => store.FindAsync(filter,
                    skip,
                    limit,
                    sort: new SortOptions<MessageReadModel>(p => p.MessageId, ascending ? SortType.Ascending : SortType.Descending),
                    cancellationToken: cancellationToken));
        }

        if (query.SortByPtsAscending)
        {
            // updates.getDifference/getChannelDifference: the oldest changes after the client pts must be returned first
            return await store.FindAsync(predicate,
                0,
                query.Limit,
                sort: new SortOptions<MessageReadModel>(p => p.Pts, SortType.Ascending),
                cancellationToken: cancellationToken);
        }

        var result = await store.FindAsync(predicate,
               0,
               query.Limit,
               sort: new SortOptions<MessageReadModel>(p => p.MessageId, SortType.Descending),
               cancellationToken: cancellationToken);

        return result.OrderByDescending(p => p.MessageId).ToList();
    }

    internal static Expression<Func<MessageReadModel, bool>> BuildPredicate(GetMessagesQuery query)
    {
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

        var offsetRate = query.OffsetRate;
        var offsetRateId = query.OffsetRateId;

        predicate = predicate
                .WhereIf(query.MessageActionType == null, p => p.MessageActionType != MessageActionType.CreateQuickReplyMessage)
                .WhereIf(query.MessageActionType.HasValue, p => p.MessageActionType == query.MessageActionType)
                .WhereIf(query.Q?.Length > 0, p => p.Message.Contains(query.Q!))
                .WhereIf(
                    query.MessageType != MessageType.Unknown && query.MessageType != MessageType.Pinned,
                    p => p.MessageType == query.MessageType)
                .WhereIf(query.MessageType == MessageType.Pinned, p => p.Pinned)
                .WhereIf(query.MessageIdList?.Count > 0, p => query.MessageIdList!.Contains(p.MessageId))
                .WhereIf(query.ChannelHistoryMinId > 0, p => p.MessageId > query.ChannelHistoryMinId)
                .WhereIf(offsetRate > 0, p => p.Date < offsetRate || (p.Date == offsetRate && p.MessageId < offsetRateId))
                .WhereIf(query.Pts > 0, p => p.Pts > query.Pts)
                .WhereIf(query.Peer != null && query.Peer.PeerType != PeerType.Empty,
                    p => p.ToPeerType == query.Peer!.PeerType && p.ToPeerId == query.Peer.PeerId)
                .WhereIf(query.ReplyToMsgId > 0, p => p.ReplyToMsgId == query.ReplyToMsgId)
                .WhereIf(query.BroadcastsOnly, p => p.ToPeerType == PeerType.Channel && p.Post)
                .WhereIf(query.GroupsOnly, p => p.ToPeerType == PeerType.Channel && !p.Post)
                .WhereIf(query.UsersOnly, p => p.ToPeerType == PeerType.User)
            ;

        return predicate;
    }

    internal static Expression<Func<MessageTokenReadModel, bool>> BuildTokenPredicate(GetMessagesQuery query)
    {
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

        var offsetRate = query.OffsetRate;
        var offsetRateId = query.OffsetRateId;

        predicate = predicate
                .WhereIf(query.Tokens?.Count > 0, p => query.Tokens!.Any(x => p.Tokens.Contains(x)))
                .WhereIf(
                    query.MessageType != MessageType.Unknown && query.MessageType != MessageType.Pinned,
                    p => p.MessageType == query.MessageType)
                .WhereIf(query.MessageType == MessageType.Pinned, p => p.Pinned)
                .WhereIf(query.MessageIdList?.Count > 0, p => query.MessageIdList!.Contains(p.MessageId))
                .WhereIf(query.ChannelHistoryMinId > 0, p => p.MessageId > query.ChannelHistoryMinId)
                .WhereIf(offsetRate > 0, p => p.Date < offsetRate || (p.Date == offsetRate && p.MessageId < offsetRateId))
                .WhereIf(query.Peer != null && query.Peer.PeerType != PeerType.Empty,
                    p => p.ToPeerType == query.Peer!.PeerType && p.ToPeerId == query.Peer.PeerId)
                .WhereIf(query.ReplyToMsgId > 0, p => p.ReplyToMsgId == query.ReplyToMsgId)
                .WhereIf(query.BroadcastsOnly, p => p.ToPeerType == PeerType.Channel && p.Post)
                .WhereIf(query.GroupsOnly, p => p.ToPeerType == PeerType.Channel && !p.Post)
                .WhereIf(query.UsersOnly, p => p.ToPeerType == PeerType.User)
            ;

        return predicate;
    }

    private async Task<IReadOnlyCollection<IMessageReadModel>> SearchByKeywordAsync(GetMessagesQuery query, CancellationToken cancellationToken)
    {
        var predicate = BuildTokenPredicate(query);
        List<TempMessageToken> tokens;

        if (query.Offset != null && !query.IsSearchGlobal)
        {
            tokens = await OffsetPagination.FindAsync<MessageTokenReadModel, TempMessageToken>(query.Offset,
                predicate,
                p => p.MessageId,
                p => p.Date,
                (filter, skip, limit, ascending) => messageTokenStore.FindAsync(filter,
                    p => new TempMessageToken(p.OwnerPeerId, p.MessageId),
                    skip,
                    limit,
                    sort: new SortOptions<MessageTokenReadModel>(p => p.MessageId, ascending ? SortType.Ascending : SortType.Descending),
                    cancellationToken: cancellationToken));
        }
        else
        {
            if (query.Offset != null)
            {
                predicate = OffsetPagination.ApplyFilters(predicate, query.Offset, p => p.MessageId, p => p.Date);
            }

            var sortOptions = query.IsSearchGlobal
                ? new SortOptions<MessageTokenReadModel>(p => p.Date).ThenBy(p => p.MessageId, SortType.Descending)
                : new SortOptions<MessageTokenReadModel>(p => p.MessageId);
            var result = await messageTokenStore.FindAsync(predicate,
                p => new TempMessageToken(p.OwnerPeerId, p.MessageId),
                0,
                query.Offset?.Limit ?? query.Limit,
                sort: sortOptions,
                cancellationToken: cancellationToken);
            tokens = result.ToList();
        }

        if (tokens.Count == 0)
        {
            return [];
        }

        var messageIds = tokens.Select(p => MessageId.Create(p.OwnerPeerId, p.MessageId).Value).ToList();
        var messages = await store.FindAsync(p => messageIds.Contains(p.Id), cancellationToken: cancellationToken);

        return query.IsSearchGlobal
            ? messages.OrderByDescending(p => p.Date).ThenByDescending(p => p.MessageId).ToList()
            : messages.OrderByDescending(p => p.MessageId).ToList();
    }
}

public record TempMessageToken(long OwnerPeerId, int MessageId);
