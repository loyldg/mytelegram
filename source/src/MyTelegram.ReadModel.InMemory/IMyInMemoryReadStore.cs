namespace MyTelegram.ReadModel.InMemory;

public interface IMyInMemoryReadStore<TReadModel> : IInMemoryReadStore<TReadModel> where TReadModel : class, IReadModel
{
    Task<IQueryable<TReadModel>> AsQueryable(CancellationToken cancellationToken = default);
}
