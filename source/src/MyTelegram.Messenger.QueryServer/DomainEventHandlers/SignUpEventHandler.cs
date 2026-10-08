namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class SignUpEventHandler(
    ICacheManager<UserCacheItem> cacheManager,
    ILogger<SignUpEventHandler> logger)
    : ISubscribeAsynchronousTo<UserAggregate, UserId, UserCreatedEvent>
{
    public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        logger.LogDebug(
            "User created successfully, phoneNumber: {PhoneNumber} userId: {UserId} firstName: {FirstName} lastName: {LastName} tempAuthKeyId: {TempAuthKeyId} permAuthKeyId: {PermAuthKeyId}",
            domainEvent.AggregateEvent.PhoneNumber,
            domainEvent.AggregateEvent.UserId,
            domainEvent.AggregateEvent.FirstName,
            domainEvent.AggregateEvent.LastName,
            domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
            domainEvent.AggregateEvent.RequestInfo.PermAuthKeyId
        );
        await cacheManager.SetAsync(
            UserCacheItem.GetCacheKey(domainEvent.AggregateEvent.PhoneNumber),
            new UserCacheItem
            {
                UserId = domainEvent.AggregateEvent.UserId
            });
    }
}
