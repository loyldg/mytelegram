namespace MyTelegram.Messenger.Services.Caching;

public interface IDomainEventCacheHelper<TDomainEvent>
{
    void Add(long key, TDomainEvent domainEvent);

    bool TryRemoveDomainEvent(long key,
        [NotNullWhen(true)] out TDomainEvent? domainEvent);

    bool TryGetDomainEvent(long key,
        [NotNullWhen(true)] out TDomainEvent? domainEvent);
}