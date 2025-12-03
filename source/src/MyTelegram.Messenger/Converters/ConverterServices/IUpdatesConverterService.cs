namespace MyTelegram.Messenger.Converters.ConverterServices;

public interface IUpdatesConverterService
{
    IUpdates ToChannelMessageUpdates(long selfUserId, SendOutboxMessageCompletedSagaEvent aggregateEvent, int layer, bool mentioned = false);

    //IUpdates ToCreateChannelUpdates(ChannelCreatedEvent channelCreatedEvent,
    //    SendOutboxMessageCompletedSagaEvent aggregateEvent);

    Task<IUpdates> ToCreateChannelUpdatesAsync(ChannelCreatedEvent eventData,
        SendOutboxMessageCompletedSagaEvent aggregateEvent, bool createUpdatesForSelf, int layer);

    IUpdates ToDeleteMessagesUpdates(PeerType toPeerType,
        DeletedBoxItem item,
        int date);

    IUpdates ToInboxForwardMessageUpdates(ReceiveInboxMessageCompletedSagaEvent aggregateEvent);

    //IUpdates ToInviteToChannelUpdates(SendOutboxMessageCompletedSagaEvent aggregateEvent,
    //    StartInviteToChannelEvent startInviteToChannelEvent,
    //    IChannelReadModel channelReadModel,
    //    bool createUpdatesForSelf, int layer);


    //IUpdates ToSelfUpdatePinnedMessageUpdates(UpdatePinnedMessageCompletedSagaEvent aggregateEvent);
    IUpdates ToUpdatePinnedMessageServiceUpdates(long selfUserId, SendOutboxMessageCompletedSagaEvent aggregateEvent, int layer);

    //IUpdates ToUpdatePinnedMessageUpdates(UpdatePinnedMessageCompletedSagaEvent aggregateEvent);

    IUpdates ToUpdatePinnedMessageUpdates(SendOutboxMessageCompletedSagaEvent aggregateEvent, int layer);

    IUpdates ToUpdatePinnedMessageUpdates(ReceiveInboxMessageCompletedSagaEvent aggregateEvent);

    IUpdates ToDraftsUpdates(IReadOnlyCollection<IDraftReadModel> draftReadModels, int layer);

    IUpdates ToChannelUpdates(IRequestWithAccessHashKeyId request, IChannelReadModel channelReadModel, IPhotoReadModel? photoReadModel, int layer);
}