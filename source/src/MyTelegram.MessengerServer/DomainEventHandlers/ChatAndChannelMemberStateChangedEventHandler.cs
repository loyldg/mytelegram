namespace MyTelegram.MessengerServer.DomainEventHandlers;

public class ChatAndChannelMemberStateChangedEventHandler :
    ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelCreatedEvent>,
    ISubscribeSynchronousTo<ChatAggregate, ChatId, ChatCreatedEvent>,
    ISubscribeSynchronousTo<ChatAggregate, ChatId, ChatMemberAddedEvent>,
    ISubscribeSynchronousTo<ChatAggregate, ChatId, ChatMemberDeletedEvent>,
    ISubscribeSynchronousTo<ChannelMemberAggregate, ChannelMemberId, ChannelMemberCreatedEvent>,
    ISubscribeSynchronousTo<ChannelMemberAggregate, ChannelMemberId, ChannelMemberJoinedEvent>,
    ISubscribeSynchronousTo<ChannelMemberAggregate, ChannelMemberId, ChannelMemberBannedRightsChangedEvent>,
    ISubscribeSynchronousTo<ChannelMemberAggregate, ChannelMemberId, ChannelMemberLeftEvent>
{
    private readonly IEventBus _eventBus;

    public ChatAndChannelMemberStateChangedEventHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return _eventBus.PublishAsync(new ChannelMemberChangedEvent(domainEvent.AggregateEvent.ChannelId,
            MemberStateChangeType.Add,
            new[] { domainEvent.AggregateEvent.CreatorId }));
    }

    public Task HandleAsync(
        IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberBannedRightsChangedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var memberStateChangeType = MemberStateChangeType.None;
        if (domainEvent.AggregateEvent.BannedRights.ViewMessages)
            memberStateChangeType = MemberStateChangeType.Add;
        else if (domainEvent.AggregateEvent.NeedRemoveFromKicked) memberStateChangeType = MemberStateChangeType.Remove;

        return _eventBus.PublishAsync(new ChannelMemberChangedEvent(domainEvent.AggregateEvent.ChannelId,
            memberStateChangeType,
            new[] { domainEvent.AggregateEvent.MemberUid }));
    }

    public Task HandleAsync(
        IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return _eventBus.PublishAsync(new ChannelMemberChangedEvent(domainEvent.AggregateEvent.ChannelId,
            MemberStateChangeType.Add,
            new[] { domainEvent.AggregateEvent.UserId }));
    }

    public Task HandleAsync(IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberJoinedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return _eventBus.PublishAsync(new ChannelMemberChangedEvent(domainEvent.AggregateEvent.ChannelId,
            MemberStateChangeType.Add,
            new[] { domainEvent.AggregateEvent.MemberUid }));
    }

    public Task HandleAsync(IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberLeftEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return _eventBus.PublishAsync(new ChannelMemberChangedEvent(domainEvent.AggregateEvent.ChannelId,
            MemberStateChangeType.Remove,
            new[] { domainEvent.AggregateEvent.MemberUid }));
    }

    public Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, ChatCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return _eventBus.PublishAsync(new ChatMemberChangedEvent(domainEvent.AggregateEvent.ChatId,
            MemberStateChangeType.Add,
            domainEvent.AggregateEvent.MemberUidList.Select(p => p.UserId).ToList()));
    }

    public Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, ChatMemberAddedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return _eventBus.PublishAsync(new ChatMemberChangedEvent(domainEvent.AggregateEvent.ChatId,
            MemberStateChangeType.Add,
            new[] { domainEvent.AggregateEvent.ChatMember.UserId }));
    }

    public Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, ChatMemberDeletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return _eventBus.PublishAsync(new ChatMemberChangedEvent(domainEvent.AggregateEvent.ChatId,
            MemberStateChangeType.Remove,
            new[] { domainEvent.AggregateEvent.UserId }));
    }
}