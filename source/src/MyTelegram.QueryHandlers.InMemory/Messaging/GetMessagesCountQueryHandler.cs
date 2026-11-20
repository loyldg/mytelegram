using MyTelegram.ReadModel.Impl;

namespace MyTelegram.QueryHandlers.InMemory.Messaging;

// ReSharper disable once UnusedMember.Global
public class GetMessagesCountQueryHandler(IQueryOnlyReadModelStore<MessageReadModel> store,
    IQueryOnlyReadModelStore<MessageTokenReadModel> messageTokenStore) : IQueryHandler<GetMessagesCountQuery, int>
{
    public async Task<int> ExecuteQueryAsync(GetMessagesCountQuery query, CancellationToken cancellationToken)
    {
        var messagesQuery = query.Query;
        if (messagesQuery.Tokens?.Count > 0)
        {
            var tokenPredicate = GetMessagesQueryHandler.BuildTokenPredicate(messagesQuery);
            if (messagesQuery.Offset != null)
            {
                tokenPredicate = OffsetPagination.ApplyFilters(tokenPredicate, messagesQuery.Offset, p => p.MessageId, p => p.Date);
            }

            if (query.GreaterThanMessageId > 0)
            {
                tokenPredicate = tokenPredicate.And(p => p.MessageId > query.GreaterThanMessageId);
            }

            return (int)await messageTokenStore.CountAsync(tokenPredicate, cancellationToken);
        }

        var predicate = GetMessagesQueryHandler.BuildPredicate(messagesQuery);
        if (messagesQuery.Offset != null)
        {
            predicate = OffsetPagination.ApplyFilters(predicate, messagesQuery.Offset, p => p.MessageId, p => p.Date);
        }

        if (query.GreaterThanMessageId > 0)
        {
            predicate = predicate.And(p => p.MessageId > query.GreaterThanMessageId);
        }

        return (int)await store.CountAsync(predicate, cancellationToken);
    }
}
