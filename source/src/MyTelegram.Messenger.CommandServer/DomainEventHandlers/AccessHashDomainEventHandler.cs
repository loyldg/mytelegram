using MyTelegram.Domain.Aggregates.Photo;
using MyTelegram.Messenger.Services.Caching;

namespace MyTelegram.Messenger.CommandServer.DomainEventHandlers;

public class AccessHashDomainEventHandler(IAccessHashHelper accessHashHelper) :
    //ISubscribeSynchronousTo<UserAggregate, UserId, UserCreatedEvent>,
    //ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelCreatedEvent>,
    ISubscribeSynchronousTo<PhotoAggregate, PhotoId, PhotoCreatedEvent>
{
    //public Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserCreatedEvent> domainEvent, CancellationToken cancellationToken)
    //{
    //    accessHashHelper.AddAccessHash(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.AccessHash);

    //    return Task.CompletedTask;
    //}

    //public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelCreatedEvent> domainEvent, CancellationToken cancellationToken)
    //{
    //    accessHashHelper.AddAccessHash(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.AccessHash);

    //    return Task.CompletedTask;
    //}

    public Task HandleAsync(IDomainEvent<PhotoAggregate, PhotoId, PhotoCreatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        accessHashHelper.AddAccessHash(domainEvent.AggregateEvent.Photo.Id, domainEvent.AggregateEvent.Photo.AccessHash);

        return Task.CompletedTask;
    }
}