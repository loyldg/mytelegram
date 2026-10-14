namespace MyTelegram.Messenger.Converters.ConverterServices;

public interface IUpdatesConverterService
{
    IUpdates ToChannelMessageUpdates(long selfUserId, SendOutboxMessageCompletedSagaEvent aggregateEvent, bool mentioned = false);

    //IUpdates ToCreateChannelUpdates(ChannelCreatedEvent channelCreatedEvent,
    //    SendOutboxMessageCompletedSagaEvent aggregateEvent);

    Task<IUpdates> ToCreateChannelUpdatesAsync(ChannelCreatedEvent eventData,
        SendOutboxMessageCompletedSagaEvent aggregateEvent, bool createUpdatesForSelf);

    IUpdates ToDeleteMessagesUpdates(PeerType toPeerType,
        DeletedBoxItem item,
        int date);

    IUpdates ToInboxForwardMessageUpdates(ReceiveInboxMessageCompletedSagaEvent aggregateEvent);

    //IUpdates ToInviteToChannelUpdates(SendOutboxMessageCompletedSagaEvent aggregateEvent,
    //    StartInviteToChannelEvent startInviteToChannelEvent,
    //    IChannelReadModel channelReadModel,
    //    bool createUpdatesForSelf);


    //IUpdates ToSelfUpdatePinnedMessageUpdates(UpdatePinnedMessageCompletedSagaEvent aggregateEvent);
    IUpdates ToUpdatePinnedMessageServiceUpdates(long selfUserId, SendOutboxMessageCompletedSagaEvent aggregateEvent);

    //IUpdates ToUpdatePinnedMessageUpdates(UpdatePinnedMessageCompletedSagaEvent aggregateEvent);

    IUpdates ToUpdatePinnedMessageUpdates(SendOutboxMessageCompletedSagaEvent aggregateEvent);

    IUpdates ToUpdatePinnedMessageUpdates(ReceiveInboxMessageCompletedSagaEvent aggregateEvent);

    IUpdates ToDraftsUpdates(IReadOnlyCollection<IDraftReadModel> draftReadModels);

    IUpdates ToChannelUpdates(IRequestWithAccessHashKeyId request, IChannelReadModel channelReadModel, IPhotoReadModel? photoReadModel);
}