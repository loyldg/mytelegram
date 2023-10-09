namespace MyTelegram.Messenger.Services.Caching;

public class DomainEventCacheHelper<TDomainEvent> : IDomainEventCacheHelper<TDomainEvent>
{
    private readonly ConcurrentDictionary<long, TDomainEvent> _cache = new();

    public void Add(long key,
        TDomainEvent domainEvent)
    {
        _cache.TryAdd(key, domainEvent);
    }

    public bool TryRemoveDomainEvent(long key,
        [NotNullWhen(true)] out TDomainEvent? domainEvent)
    {
        return _cache.TryRemove(key, out domainEvent);
    }

    public bool TryGetDomainEvent(long key,
        [NotNullWhen(true)] out TDomainEvent? domainEvent)
    {
        return _cache.TryGetValue(key, out domainEvent);
    }
}