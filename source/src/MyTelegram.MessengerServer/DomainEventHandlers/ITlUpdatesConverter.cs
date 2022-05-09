namespace MyTelegram.MessengerServer.DomainEventHandlers;

public interface ITlUpdatesConverter
{
    IUpdates ToSelfUpdates(SendOutboxMessageCompletedEvent aggregateEvent);
    IUpdates ToSelfOtherDeviceUpdates(SendOutboxMessageCompletedEvent aggregateEvent);
    IUpdates ToUpdates(ReceiveInboxMessageCompletedEvent aggregateEvent);
    IUpdates ToChannelMessageUpdates(SendOutboxMessageCompletedEvent aggregateEvent);
    IUpdates ToInboxForwardMessageUpdates(ReceiveInboxMessageCompletedEvent aggregateEvent);

    IUpdates ToEditUpdates(OutboxMessageEditCompletedEvent aggregateEvent, long selfUserId);
    IUpdates ToEditUpdates(InboxMessageEditCompletedEvent aggregateEvent);
    IUpdates ToUpdatePinnedMessageUpdates(UpdatePinnedMessageCompletedEvent aggregateEvent);
    IUpdates ToUpdatePinnedMessageServiceUpdates(SendOutboxMessageCompletedEvent aggregateEvent);
    IUpdates ToUpdatePinnedMessageUpdates(SendOutboxMessageCompletedEvent aggregateEvent);
    IUpdates ToUpdatePinnedMessageUpdates(ReceiveInboxMessageCompletedEvent aggregateEvent);

    IUpdates ToCreateChatUpdates(ChatCreatedEvent chatCreatedAggregateEvent,
        SendOutboxMessageCompletedEvent aggregateEvent);
    IUpdates ToCreateChatUpdates(ChatCreatedEvent eventData,
        ReceiveInboxMessageCompletedEvent aggregateEvent);
    IUpdates ToCreateChannelUpdates(ChannelCreatedEvent channelCreatedEvent,
        SendOutboxMessageCompletedEvent aggregateEvent);
    IUpdates ToInviteToChannelUpdates(SendOutboxMessageCompletedEvent aggregateEvent,
        StartInviteToChannelEvent startInviteToChannelEvent,
        IChannelReadModel channelReadModel,
        bool createUpdatesForSelf);

    IUpdates ToInviteToChannelUpdates(IChannelReadModel channelReadModel,
        IUserReadModel senderUserReadModel,
        int date);
}