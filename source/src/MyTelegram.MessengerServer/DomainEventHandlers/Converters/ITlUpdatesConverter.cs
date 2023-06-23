namespace MyTelegram.MessengerServer.DomainEventHandlers.Converters;

public interface ITlUpdatesConverter
{
    IUpdates ToChannelMessageUpdates(SendOutboxMessageCompletedEvent aggregateEvent);

    IUpdates ToCreateChannelUpdates(ChannelCreatedEvent channelCreatedEvent,
        SendOutboxMessageCompletedEvent aggregateEvent);

    IUpdates ToCreateChatUpdates(ChatCreatedEvent chatCreatedAggregateEvent,
        SendOutboxMessageCompletedEvent aggregateEvent);

    IUpdates ToCreateChatUpdates(ChatCreatedEvent eventData,
        ReceiveInboxMessageCompletedEvent aggregateEvent);

    IUpdates ToDeleteMessagesUpdates(PeerType toPeerType,
        DeletedBoxItem item,
        int date);

    IUpdates ToEditUpdates(OutboxMessageEditCompletedEvent aggregateEvent,
        long selfUserId);

    IUpdates ToEditUpdates(InboxMessageEditCompletedEvent aggregateEvent);

    IUpdates ToInboxForwardMessageUpdates(ReceiveInboxMessageCompletedEvent aggregateEvent);

    IUpdates ToInviteToChannelUpdates(SendOutboxMessageCompletedEvent aggregateEvent,
        StartInviteToChannelEvent startInviteToChannelEvent,
        IChannelReadModel channelReadModel,
        bool createUpdatesForSelf);

    IUpdates ToInviteToChannelUpdates(IChannelReadModel channelReadModel,
        IUserReadModel senderUserReadModel,
        int date);

    IUpdates ToReadHistoryUpdates(ReadHistoryCompletedEvent eventData);

    IUpdates ToSelfOtherDeviceUpdates(SendOutboxMessageCompletedEvent aggregateEvent);

    IUpdates ToSelfUpdatePinnedMessageUpdates(UpdatePinnedMessageCompletedEvent aggregateEvent);
    IUpdates ToSelfUpdates(SendOutboxMessageCompletedEvent aggregateEvent);
    IUpdates ToUpdatePinnedMessageServiceUpdates(SendOutboxMessageCompletedEvent aggregateEvent);
    IUpdates ToUpdatePinnedMessageUpdates(UpdatePinnedMessageCompletedEvent aggregateEvent);
    IUpdates ToUpdatePinnedMessageUpdates(SendOutboxMessageCompletedEvent aggregateEvent);
    IUpdates ToUpdatePinnedMessageUpdates(ReceiveInboxMessageCompletedEvent aggregateEvent);
    IUpdates ToUpdates(ReceiveInboxMessageCompletedEvent aggregateEvent);
}