namespace MyTelegram.QueryHandlers.MongoDB.Contact;

public class GetContactQueryHandler : IQueryHandler<GetContactQuery, IContactReadModel?>
{
    public Task<IContactReadModel?> ExecuteQueryAsync(GetContactQuery query,
        CancellationToken cancellationToken)
    {
        return Task.FromResult<IContactReadModel?>(null);
    }
}
