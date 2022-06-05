namespace MyTelegram.MessengerServer.DomainEventHandlers;

public class ChannelMessageViewsDomainEventHandler : ISubscribeSynchronousTo<MessageSaga, MessageSagaId, SendOutboxMessageCompletedEvent>
{
    private readonly IChannelMessageViewsAppService _channelMessageViewsAppService;

    public ChannelMessageViewsDomainEventHandler(IChannelMessageViewsAppService channelMessageViewsAppService)
    {
        _channelMessageViewsAppService = channelMessageViewsAppService;
    }

    public async Task HandleAsync(IDomainEvent<MessageSaga, MessageSagaId, SendOutboxMessageCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var item = domainEvent.AggregateEvent.MessageItem;
        if (item.ToPeer.PeerType == PeerType.Channel && item.FwdHeader == null && item.Views > 0)
        {
            await _channelMessageViewsAppService
                .IncrementViewsIfNotIncrementedAsync(domainEvent.AggregateEvent.Request.UserId,
                    domainEvent.AggregateEvent.Request.PermAuthKeyId,
                    item.ToPeer.PeerId,
                    item.MessageId).ConfigureAwait(false);
        }
    }
}