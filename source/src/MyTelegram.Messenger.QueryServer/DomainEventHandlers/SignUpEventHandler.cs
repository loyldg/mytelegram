namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class SignUpEventHandler : ISubscribeSynchronousTo<UserAggregate, UserId, UserCreatedEvent>
{
    private readonly ICacheManager<UserCacheItem> _cacheManager;
    private readonly ILogger<SignUpEventHandler> _logger;

    public SignUpEventHandler(
        ICacheManager<UserCacheItem> cacheManager,
        ILogger<SignUpEventHandler> logger)
    {
        _cacheManager = cacheManager;
        _logger = logger;
    }

    public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogDebug(
            "User sign up success,phoneNumber={PhoneNumber} userId={UserId} firstName={FirstName} lastName={LastName} tempAuthKeyId={TempAuthKeyId} permAuthKeyId={PermAuthKeyId}",
            domainEvent.AggregateEvent.PhoneNumber,
            domainEvent.AggregateEvent.UserId,
            domainEvent.AggregateEvent.FirstName,
            domainEvent.AggregateEvent.LastName,
            domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
            domainEvent.AggregateEvent.RequestInfo.PermAuthKeyId
        );
        await _cacheManager.SetAsync(
            UserCacheItem.GetCacheKey(domainEvent.AggregateEvent.PhoneNumber),
            new UserCacheItem
            {
                UserId = domainEvent.AggregateEvent.UserId
            });
    }
}
