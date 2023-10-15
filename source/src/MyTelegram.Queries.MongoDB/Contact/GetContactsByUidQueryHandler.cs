namespace MyTelegram.QueryHandlers.MongoDB.Contact;

public class
    GetContactsByUidQueryHandler : IQueryHandler<GetContactsByUidQuery, IReadOnlyCollection<IContactReadModel>>
{
    public Task<IReadOnlyCollection<IContactReadModel>> ExecuteQueryAsync(GetContactsByUidQuery query,
        CancellationToken cancellationToken)
    {
        return Task.FromResult<IReadOnlyCollection<IContactReadModel>>(Array.Empty<IContactReadModel>());
    }
}
