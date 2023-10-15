namespace MyTelegram.QueryHandlers.MongoDB.Contact;

public class SearchContactQueryHandler : IQueryHandler<SearchContactQuery, IReadOnlyCollection<IContactReadModel>>
{
    public Task<IReadOnlyCollection<IContactReadModel>> ExecuteQueryAsync(SearchContactQuery query,
        CancellationToken cancellationToken)
    {
       return Task.FromResult<IReadOnlyCollection<IContactReadModel>>(Array.Empty<IContactReadModel>());
    }
}
