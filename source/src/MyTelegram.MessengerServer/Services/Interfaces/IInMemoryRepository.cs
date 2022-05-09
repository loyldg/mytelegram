namespace MyTelegram.MessengerServer.Services.Interfaces;

public interface IInMemoryRepository<TEntity, TPrimaryKey>
//where TEntity : IEntity<TIdentity>
//where TIdentity : IIdentity
{
    //IList<TEntity> InsertMany(IList<TEntity> entities);

    void Delete(TPrimaryKey id);
    bool Exists(TPrimaryKey id);
    TEntity? Find(TPrimaryKey id);
    TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
    TEntity Get(TPrimaryKey id);
    List<TEntity> GetList(IList<TPrimaryKey> idList);
    List<TEntity> GetList(Func<TEntity, bool> predicate);

    TEntity Insert(TPrimaryKey id,
        TEntity entity);

    bool TryDelete(TPrimaryKey id,
        out TEntity? entity);
    //void Delete(TEntity entity);

    //void Delete(Expression<Func<TEntity, bool>> predicate);
}