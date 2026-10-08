namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class ChatAndChannelMemberStateChangedEventHandler(IEventBus eventBus) :
    ISubscribeAsynchronousTo<ChannelAggregate, ChannelId, ChannelCreatedEvent>,
    ISubscribeAsynchronousTo<ChannelMemberAggregate, ChannelMemberId, ChannelMemberCreatedEvent>,
    //ISubscribeAsynchronousTo<ChannelMemberAggregate, ChannelMemberId, ChannelMemberJoinedEvent>,
    ISubscribeAsynchronousTo<ChannelMemberAggregate, ChannelMemberId, ChannelMemberBannedRightsChangedEvent>,
    ISubscribeAsynchronousTo<ChannelMemberAggregate, ChannelMemberId, ChannelMemberLeftEvent>
{
    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return eventBus.PublishAsync(new ChannelMemberChangedEvent(domainEvent.AggregateEvent.ChannelId,
            MemberStateChangeType.Add,
            [domainEvent.AggregateEvent.CreatorId]));
    }

    public Task HandleAsync(
        IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberBannedRightsChangedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        //var memberStateChangeType = MemberStateChangeType.None;
        //if (domainEvent.AggregateEvent.BannedRights.ViewMessages)
        //{
        //    memberStateChangeType = MemberStateChangeType.Remove;
        //}
        //else if (domainEvent.AggregateEvent.RemovedFromKicked)
        //{
        //    memberStateChangeType = MemberStateChangeType.Add;
        //}

        return eventBus.PublishAsync(new ChannelMemberBannedEvent(domainEvent.AggregateEvent.ChannelId,
            domainEvent.AggregateEvent.MemberUserId, domainEvent.AggregateEvent.BannedRights.ToIntValue(),
            domainEvent.AggregateEvent.BannedRights.UntilDate));
        //return eventBus.PublishAsync(new ChannelMemberChangedEvent(domainEvent.AggregateEvent.ChannelId,
        //    memberStateChangeType,
        //    [domainEvent.AggregateEvent.MemberUserId]));
    }

    public Task HandleAsync(
        IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return eventBus.PublishAsync(new ChannelMemberChangedEvent(domainEvent.AggregateEvent.ChannelId,
            MemberStateChangeType.Add,
            new[] { domainEvent.AggregateEvent.UserId }));
    }

    //public Task HandleAsync(IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberJoinedEvent> domainEvent,
    //    CancellationToken cancellationToken)
    //{
    //    return eventBus.PublishAsync(new ChannelMemberChangedEvent(domainEvent.AggregateEvent.ChannelId,
    //        MemberStateChangeType.Add,
    //        new[] { domainEvent.AggregateEvent.MemberUserId }));
    //}

    public Task HandleAsync(IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberLeftEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return eventBus.PublishAsync(new ChannelMemberChangedEvent(domainEvent.AggregateEvent.ChannelId,
            MemberStateChangeType.Remove,
            new[] { domainEvent.AggregateEvent.MemberUserId }));
    }
}
