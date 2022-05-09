namespace MyTelegram.QueryHandlers.MongoDB.Dialog;

// ReSharper disable once UnusedMember.Global
public class GetDialogByIdQueryHandler : IQueryHandler<GetDialogByIdQuery, IDialogReadModel?>
{
    private readonly IMongoDbReadModelStore<DialogReadModel> _store;

    public GetDialogByIdQueryHandler(IMongoDbReadModelStore<DialogReadModel> store)
    {
        _store = store;
    }

    public async Task<IDialogReadModel?> ExecuteQueryAsync(GetDialogByIdQuery query,
        CancellationToken cancellationToken)
    {
        var item = await _store.GetAsync(query.Id.Value, cancellationToken).ConfigureAwait(false);

        return item.ReadModel;
    }
}
