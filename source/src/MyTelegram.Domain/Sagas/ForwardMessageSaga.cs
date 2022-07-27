namespace MyTelegram.Domain.Sagas;

public class ForwardMessageSaga : MyInMemoryAggregateSaga<ForwardMessageSaga, ForwardMessageSagaId, ForwardMessageSagaLocator>,
        ISagaIsStartedBy<MessageAggregate, MessageId, ForwardMessageStartedEvent>,
        ISagaHandles<MessageAggregate, MessageId, MessageForwardedEvent>
{
    private readonly ForwardMessageState _state = new();

    public ForwardMessageSaga(ForwardMessageSagaId id, IEventStore eventStore) : base(id, eventStore)
    {
        Register(_state);
    }

    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, MessageForwardedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        await SendMessageToTargetPeerAsync(domainEvent.AggregateEvent).ConfigureAwait(false);
        Emit(new ForwardSingleMessageSuccessEvent());
        await HandleForwardCompletedAsync().ConfigureAwait(false);
    }

    public Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, ForwardMessageStartedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new ForwardMessageSagaStartedEvent(domainEvent.AggregateEvent.Request,
            domainEvent.AggregateEvent.FromPeer,
            domainEvent.AggregateEvent.ToPeer,
            domainEvent.AggregateEvent.IdList,
            domainEvent.AggregateEvent.RandomIdList,
            domainEvent.AggregateEvent.ForwardFromLinkedChannel,
            domainEvent.AggregateEvent.CorrelationId
        ));
        ForwardMessage(domainEvent.AggregateEvent);
        return Task.CompletedTask;
    }

    private void ForwardMessage(ForwardMessageStartedEvent aggregateEvent)
    {
        var ownerPeerId = _state.FromPeer.PeerType == PeerType.Channel
            ? _state.FromPeer.PeerId
            : _state.Request.UserId;
        var index = 0;
        foreach (var messageId in aggregateEvent.IdList)
        {
            var randomId = aggregateEvent.RandomIdList[index];
            var command = new ForwardMessageCommand(MessageId.Create(ownerPeerId, messageId),
                aggregateEvent.Request,
                randomId,
                aggregateEvent.CorrelationId);
            Publish(command);
            index++;
        }
    }

    private Task HandleForwardCompletedAsync()
    {
        if (_state.IsCompleted)
        {
            return CompleteAsync();
        }

        return Task.CompletedTask;
    }

    private Task SendMessageToTargetPeerAsync(MessageForwardedEvent aggregateEvent)
    {
        var selfUserId = _state.Request.UserId;
        var ownerPeerId = _state.ToPeer.PeerType == PeerType.Channel
            ? _state.ToPeer.PeerId
            : selfUserId;

        var outMessageId = 0;
        var fromId = _state.FromPeer;
        var channelPost = _state.FromPeer.PeerType == PeerType.Channel ? aggregateEvent.OriginalMessageItem.MessageId : 0;

        var savedFromPeer = _state.ToPeer.PeerId == selfUserId ? _state.FromPeer : null;

        var savedFromMsgId = _state.ToPeer.PeerId == selfUserId ? aggregateEvent.OriginalMessageItem.MessageId : 0;
        if (_state.ForwardFromLinkedChannel)
        {
            savedFromPeer = _state.FromPeer;
            savedFromMsgId = aggregateEvent.OriginalMessageItem.MessageId;
        }
        // TODO:Set fromName
        var fwdHeader = new MessageFwdHeader(fromId,
            null,
            channelPost,
            //aggregateEvent.PostAuthor,
            string.Empty,
            //aggregateEvent.Date,
            DateTime.UtcNow.ToTimestamp(),
            savedFromPeer,
            savedFromMsgId);

        var aggregateId = MessageId.CreateWithRandomId(ownerPeerId, aggregateEvent.RandomId);
        var senderPeer = new Peer(PeerType.User, _state.Request.UserId);
        var ownerPeer = _state.ToPeer.PeerType == PeerType.Channel
            ? _state.ToPeer
            : senderPeer;
        var toPeer = _state.ToPeer;
        var item = aggregateEvent.OriginalMessageItem;

        var command = new StartSendMessageCommand(aggregateId, aggregateEvent.Request,
            new MessageItem(
                ownerPeer,
                toPeer,
                senderPeer,
                outMessageId,
                item.Message,
                DateTime.UtcNow.ToTimestamp(),
                aggregateEvent.RandomId,
                true,
                SendMessageType.Text,
                MessageType.Text,
                MessageSubType.ForwardMessage,
                replyToMsgId: item.ReplyToMsgId,
                entities: item.Entities,
                media: item.Media,
                fwdHeader: fwdHeader,
                views: item.Views
            ),
            false,
            1,
            aggregateEvent.CorrelationId,
            _state.ForwardFromLinkedChannel
        );

        Publish(command);

        return Task.CompletedTask;
    }
}
