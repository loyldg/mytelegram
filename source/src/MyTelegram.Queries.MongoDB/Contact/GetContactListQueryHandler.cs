namespace MyTelegram.QueryHandlers.MongoDB.Contact;

public class GetContactListQueryHandler : IQueryHandler<GetContactListQuery, IReadOnlyCollection<IContactReadModel>>
{
    private readonly IMongoDbReadModelStore<ContactReadModel> _store;

    public GetContactListQueryHandler(IMongoDbReadModelStore<ContactReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IContactReadModel>> ExecuteQueryAsync(GetContactListQuery query,
        CancellationToken cancellationToken)
    {
        var idList = query.TargetUserIdList.Select(p => ContactId.Create(query.SelfUserId, p).Value).ToList();
        var cursor = await _store.FindAsync(p => idList.Contains(p.Id), cancellationToken: cancellationToken)
     ;
        return await cursor.ToListAsync(cancellationToken);
    }
}
public class GetContactUserIdListQueryHandler : IQueryHandler<GetContactUserIdListQuery, IReadOnlyCollection<long>>
{
    private readonly IMyMongoDbReadModelStore<ContactReadModel> _store;

    public GetContactUserIdListQueryHandler(IMyMongoDbReadModelStore<ContactReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<long>> ExecuteQueryAsync(GetContactUserIdListQuery query,
        CancellationToken cancellationToken)
    {
        var findOptions = new FindOptions<ContactReadModel, long>
        {
            Projection = new ProjectionDefinitionBuilder<ContactReadModel>().Expression(p => p.TargetUserId),
        };
        var cursor = await _store.FindAsync(p => p.SelfUserId == query.SelfUserId, findOptions, cancellationToken);

        return await cursor.ToListAsync(cancellationToken);
    }
}


public class GetContactListBySelfIdAndTargetUserIdQueryHandler : IQueryHandler<
    GetContactListBySelfIdAndTargetUserIdQuery, IReadOnlyCollection<IContactReadModel>>
{
    private readonly IMyMongoDbReadModelStore<ContactReadModel> _store;

    public GetContactListBySelfIdAndTargetUserIdQueryHandler(IMyMongoDbReadModelStore<ContactReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IContactReadModel>> ExecuteQueryAsync(GetContactListBySelfIdAndTargetUserIdQuery query, CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p =>
            (p.SelfUserId == query.SelfUserId && p.TargetUserId == query.TargetUserId) ||
            (p.SelfUserId == query.TargetUserId && p.TargetUserId == query.SelfUserId), cancellationToken: cancellationToken);

        return await cursor.ToListAsync(cancellationToken);
    }
}
