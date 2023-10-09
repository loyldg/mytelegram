using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace MyTelegram.Services.Services;

public class
    InMemoryRepository<TEntity, TPrimaryKey> : IInMemoryRepository<TEntity, TPrimaryKey>
    where TPrimaryKey : notnull //, ISingletonDependency
//where TEntity : IEntity<TPrimaryKey>
//where TPrimaryKey : IIdentity
{
    private readonly ConcurrentDictionary<TPrimaryKey, TEntity> _entities = new();

    public bool TryDelete(TPrimaryKey id,
        out TEntity? entity)
    {
        return _entities.TryRemove(id, out entity);
    }

    public TEntity? Find(TPrimaryKey id)
    {
        if (_entities.TryGetValue(id, out var entity))
        {
            return entity;
        }

        return default;
    }

    public TEntity Get(TPrimaryKey id)
    {
        if (_entities.TryGetValue(id, out var entity))
        {
            return entity;
        }

        //return default;
        throw new ArgumentException($"entity:{typeof(TEntity).FullName} can not find.{id}");
    }

    public List<TEntity> GetList(IList<TPrimaryKey> idList)
    {
        var entityList = new List<TEntity>();
        foreach (var id in idList)
        {
            if (_entities.TryGetValue(id, out var entity))
            {
                entityList.Add(entity);
            }
        }

        return entityList;
    }

    public List<TEntity> GetList(Func<TEntity, bool> predicate)
    {
        return _entities.Values.Where(predicate).ToList();
    }

    public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
    {
        return _entities.Values.FirstOrDefault(predicate.Compile());
    }

    public TEntity Insert(TPrimaryKey id,
        TEntity entity)
    {
        //if (!_entities.ContainsKey(entity.Id))
        //{
        //    _entities.TryAdd(entity.Id, entity);
        //}
        //else
        //{
        //    _entities.TryGetValue(entity.Id, out var oldItem);
        //    return oldItem;
        //}
        if (!_entities.TryGetValue(id, out var oldEntity))
        {
            oldEntity = entity;
            _entities.TryAdd(id, entity);
        }

        return oldEntity;
    }

    public bool Exists(TPrimaryKey id)
    {
        return _entities.ContainsKey(id);
    }

    //public IList<TEntity> InsertMany(IList<TEntity> entities)
    //{
    //    foreach (var entity in entities)
    //    {
    //        _entities.TryAdd(entity.Id, entity);
    //    }

    //    return entities;
    //}

    public void Delete(TPrimaryKey id)
    {
        _entities.TryRemove(id, out _);
    }

    //public void Delete(Expression<Func<TEntity, bool>> predicate)
    //{
    //    var entities = _entities.Values.Where(predicate.Compile());
    //    foreach (var entity in entities)
    //    {
    //        _entities.TryRemove(entity.Id, out _);
    //    }

    //}
}