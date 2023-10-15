namespace MyTelegram.QueryHandlers.MongoDB.Contact;

public class GetContactListQueryHandler : IQueryHandler<GetContactListQuery, IReadOnlyCollection<IContactReadModel>>
{
    public Task<IReadOnlyCollection<IContactReadModel>> ExecuteQueryAsync(GetContactListQuery query,
        CancellationToken cancellationToken)
    {
        return Task.FromResult<IReadOnlyCollection<IContactReadModel>>(Array.Empty<IContactReadModel>());
    }
}
public class GetContactUserIdListQueryHandler : IQueryHandler<GetContactUserIdListQuery, IReadOnlyCollection<long>>
{
    public Task<IReadOnlyCollection<long>> ExecuteQueryAsync(GetContactUserIdListQuery query,
        CancellationToken cancellationToken)
    {
        return Task.FromResult<IReadOnlyCollection<long>>(Array.Empty<long>());
    }
}
