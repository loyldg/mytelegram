using MyTelegram.Messenger.Services.Caching;

namespace MyTelegram.Messenger.CommandServer.DomainEventHandlers;

public class AccessHashDomainEventHandler :
    ISubscribeSynchronousTo<UserAggregate, UserId, UserCreatedEvent>,
    ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelCreatedEvent>
{
    private readonly IAccessHashHelper _accessHashHelper;

    public AccessHashDomainEventHandler(IAccessHashHelper accessHashHelper)
    {
        _accessHashHelper = accessHashHelper;
    }

    public Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserCreatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        _accessHashHelper.AddAccessHash(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.AccessHash);
        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelCreatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        _accessHashHelper.AddAccessHash(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.AccessHash);
        return Task.CompletedTask;
    }
}
