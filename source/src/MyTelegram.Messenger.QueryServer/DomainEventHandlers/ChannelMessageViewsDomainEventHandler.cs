using MyTelegram.Messenger.Services.Interfaces;

namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class ChannelMessageViewsDomainEventHandler(IChannelMessageViewsAppService channelMessageViewsAppService)
    : ISubscribeSynchronousTo<SendMessageSaga, SendMessageSagaId, SendOutboxMessageCompletedSagaEvent>
{
    public async Task HandleAsync(IDomainEvent<SendMessageSaga, SendMessageSagaId, SendOutboxMessageCompletedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var item = domainEvent.AggregateEvent.MessageItem;
        if (item.ToPeer.PeerType == PeerType.Channel && item.FwdHeader == null && item.Views > 0)
        {
            await channelMessageViewsAppService
                .IncrementViewsIfNotIncrementedAsync(domainEvent.AggregateEvent.RequestInfo.UserId,
                    item.ToPeer.PeerId,
                    item.MessageId);
        }
    }
}