namespace MyTelegram.Domain.Sagas;

public class ForwardMessageSaga : MyInMemoryAggregateSaga<ForwardMessageSaga, ForwardMessageSagaId, ForwardMessageSagaLocator>,
        ISagaIsStartedBy<MessageAggregate, MessageId, ForwardMessageStartedEvent>,
        ISagaHandles<MessageAggregate, MessageId, MessageForwardedEvent>
{
    private readonly IIdGenerator _idGenerator;
    private readonly ForwardMessageState _state = new();

    public ForwardMessageSaga(ForwardMessageSagaId id, IEventStore eventStore, IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
        Register(_state);
    }

    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, MessageForwardedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        await SendMessageToTargetPeerAsync(domainEvent.AggregateEvent);
        Emit(new ForwardSingleMessageSuccessEvent());
        await HandleForwardCompletedAsync();
    }

    public Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, ForwardMessageStartedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new ForwardMessageSagaStartedEvent(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.FromPeer,
            domainEvent.AggregateEvent.ToPeer,
            domainEvent.AggregateEvent.IdList,
            domainEvent.AggregateEvent.RandomIdList,
            domainEvent.AggregateEvent.ForwardFromLinkedChannel
        ));
        ForwardMessage(domainEvent.AggregateEvent);
        return Task.CompletedTask;
    }

    private void ForwardMessage(ForwardMessageStartedEvent aggregateEvent)
    {
        var ownerPeerId = _state.FromPeer.PeerType == PeerType.Channel
            ? _state.FromPeer.PeerId
            : _state.RequestInfo.UserId;
        var index = 0;
        foreach (var messageId in aggregateEvent.IdList)
        {
            var randomId = aggregateEvent.RandomIdList[index];
            var command = new ForwardMessageCommand(MessageId.Create(ownerPeerId, messageId),
                aggregateEvent.RequestInfo,
                randomId);
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

    private async Task SendMessageToTargetPeerAsync(MessageForwardedEvent aggregateEvent)
    {
        var selfUserId = _state.RequestInfo.UserId;
        var ownerPeerId = _state.ToPeer.PeerType == PeerType.Channel
            ? _state.ToPeer.PeerId
            : selfUserId;

        var outMessageId = 0;
        var fromId = _state.FromPeer;
        var channelPost = _state.FromPeer.PeerType == PeerType.Channel ? aggregateEvent.OriginalMessageItem.MessageId : 0;
        var senderPeer = new Peer(PeerType.User, _state.RequestInfo.UserId);

        var savedFromPeer = _state.ToPeer.PeerId == selfUserId ? _state.FromPeer : null;
        var savedFromMsgId = _state.ToPeer.PeerId == selfUserId ? aggregateEvent.OriginalMessageItem.MessageId : 0;
        var isOut = true;
        if (_state.ForwardFromLinkedChannel)
        {
            savedFromPeer = _state.FromPeer;
            savedFromMsgId = aggregateEvent.OriginalMessageItem.MessageId;
            senderPeer = _state.FromPeer;
            isOut = false;
            //senderPeer=new Peer(PeerType.Channel,_state.)
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

        outMessageId = await _idGenerator.NextIdAsync(IdType.MessageId, ownerPeerId);
        //var aggregateId = MessageId.CreateWithRandomId(ownerPeerId, aggregateEvent.RandomId);
        var aggregateId = MessageId.Create(ownerPeerId, outMessageId);


        var ownerPeer = _state.ToPeer.PeerType == PeerType.Channel
            ? _state.ToPeer
            : senderPeer;
        var toPeer = _state.ToPeer;
        var item = aggregateEvent.OriginalMessageItem;

        var command = new CreateOutboxMessageCommand(aggregateId, aggregateEvent.RequestInfo,
            new MessageItem(
                ownerPeer,
                toPeer,
                senderPeer,
                outMessageId,
                item.Message,
                DateTime.UtcNow.ToTimestamp(),
                aggregateEvent.RandomId,
                isOut,
                SendMessageType.Text,
                MessageType.Text,
                MessageSubType.ForwardMessage,
                //replyToMsgId: item.ReplyToMsgId,
                inputReplyTo:item.InputReplyTo,
                entities: item.Entities,
                media: item.Media,
                fwdHeader: fwdHeader,
                views: item.Views,
                pollId: item.PollId
            )//,
             //null,
             //false,
             //1//,
             //forwardFromLinkedChannel: _state.ForwardFromLinkedChannel
        );

        Publish(command);

        //return Task.CompletedTask;
    }
}
