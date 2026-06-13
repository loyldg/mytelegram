using System.Linq.Expressions;
using EventFlow.ReadStores;

namespace MyTelegram.EventFlow.ReadStores;

public interface IMyReadModel : IReadModel, ISoftDelete
{
    long? CreatedBy { get; set; }
    long CreatedAt { get; set; }
    long? LastModificationTime { get; set; }
    long? LastModifiedBy { get; set; }
    long? DeletionTime { get; set; }
    long? DeletedBy { get; set; }
}

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}

public interface IQueryOnlyReadModel : IReadModel
{
}

public interface IQueryOnlyReadModelStore<TQueryOnlyReadModel> //: IReadModelStore<TQueryOnlyReadModel>
    where TQueryOnlyReadModel : class, IReadModel
{
    IQueryable<TQueryOnlyReadModel> GetAll();
    Task<IReadOnlyCollection<TQueryOnlyReadModel>> FindAsync(
        Expression<Func<TQueryOnlyReadModel, bool>> filter,
        int skip = 0,
        int limit = 0,
        SortOptions<TQueryOnlyReadModel>? sort = null,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<TResult>> FindAsync<TResult>(Expression<Func<TQueryOnlyReadModel, bool>> filter,
        Expression<Func<TQueryOnlyReadModel, TResult>> createResult,
        int skip = 0,
        int limit = 0,
        SortOptions<TQueryOnlyReadModel>? sort = null,
        CancellationToken cancellationToken = default);

    Task<TQueryOnlyReadModel?> FirstOrDefaultAsync(Expression<Func<TQueryOnlyReadModel, bool>> filter,
        CancellationToken cancellationToken = default);

    Task<TResult?> FirstOrDefaultAsync<TResult>(Expression<Func<TQueryOnlyReadModel, bool>> filter,
        Expression<Func<TQueryOnlyReadModel, TResult>> createResult,
        SortOptions<TQueryOnlyReadModel>? sort = null,
        CancellationToken cancellationToken = default);

    Task<long> CountAsync(Expression<Func<TQueryOnlyReadModel, bool>> filter, CancellationToken cancellationToken = default);

    Task<List<TResult>> GroupByAsync<TKey, TResult>(
        Expression<Func<TQueryOnlyReadModel, bool>>? filter,
        Expression<Func<TQueryOnlyReadModel, TKey>> keySelector,
        Expression<Func<IGrouping<TKey, TQueryOnlyReadModel>, TResult>> resultSelector);
}