//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using EventFlow.ReadStores;
//using EventFlow.ReadStores.InMemory;

//namespace MyTelegram.Messenger.Services.Queries;

//public class CachedReadModelByIdQuery<TReadModel> : IQuery<TReadModel>
//where TReadModel : IReadModel
//{
//    public string Id { get; }
//    public CachedReadModelByIdQuery(string id)
//    {
//        if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

//        Id = id;
//    }

//    public CachedReadModelByIdQuery(IIdentity identity) : this(identity.Value)
//    {
//    }
//}

//public class CachedReadModelByIdQueryHandler<TReadStore, TCachedReadStore, TReadModel, TCachedReadModel> : IQueryHandler<CachedReadModelByIdQuery<TReadModel>, TReadModel>
//    where TReadStore : IReadModelStore<TReadModel>
//    where TCachedReadStore : IReadModelStore<TReadModel>
//    where TReadModel : class, IReadModel
//{
//    private readonly TCachedReadStore _cachedReadStore;
//    private readonly TReadStore _readStore;

//    public CachedReadModelByIdQueryHandler(TCachedReadStore cachedReadStore, TReadStore readStore)
//    {
//        _cachedReadStore = cachedReadStore;
//        _readStore = readStore;
//    }

//    public async Task<TReadModel> ExecuteQueryAsync(CachedReadModelByIdQuery<TReadModel> query, CancellationToken cancellationToken)
//    {
//        var cachedReadModel = await _cachedReadStore.GetAsync(query.Id, cancellationToken);
//        if (cachedReadModel.ReadModel != null)
//        {
//            return cachedReadModel.ReadModel;
//        }

//        var readModelEnvelope = await _readStore.GetAsync(query.Id, cancellationToken);

//        return readModelEnvelope.ReadModel;
//    }
//}