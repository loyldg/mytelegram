using SendOutboxMessageCompletedEvent = MyTelegram.Domain.Sagas.Events.SendOutboxMessageCompletedEvent;

namespace MyTelegram.Domain.Sagas;

public class SendMessageSaga : MyInMemoryAggregateSaga<SendMessageSaga, SendMessageSagaId, SendMessageSagaLocator>,
    ISagaIsStartedBy<MessageAggregate, MessageId, OutboxMessageCreatedEvent>,
    ISagaHandles<MessageAggregate, MessageId, InboxMessageCreatedEvent>
{
    private readonly IIdGenerator _idGenerator;
    private readonly SendMessageSagaState _state = new();
    public SendMessageSaga(SendMessageSagaId id, IEventStore eventStore, IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
        Register(_state);
    }

    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, OutboxMessageCreatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        Emit(new SendMessageSagaStartedEvent(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.OutboxMessageItem,
            domainEvent.AggregateEvent.MentionedUserIds,
            domainEvent.AggregateEvent.ReplyToMsgItems,
            domainEvent.AggregateEvent.ClearDraft,
            domainEvent.AggregateEvent.GroupItemCount,
            domainEvent.AggregateEvent.LinkedChannelId,
            domainEvent.AggregateEvent.ChatMembers
            ));

        await HandleSendOutboxMessageCompletedAsync();

        await CreateInboxMessageAsync(domainEvent.AggregateEvent);

        CreateMentions(domainEvent.AggregateEvent.MentionedUserIds, domainEvent.AggregateEvent.OutboxMessageItem.MessageId);
    }

    private void CreateMentions(List<long>? mentionedUserIds, int messageId)
    {
        //if (mentionedUserIds?.Count > 0)
        //{
        //    foreach (var mentionedUserId in mentionedUserIds)
        //    {
        //        var command = new CreateMentionCommand(DialogId.Create(mentionedUserId, _state.MessageItem.ToPeer),
        //            mentionedUserId, /*_state.MessageItem.ToPeer.PeerId,*/ messageId);
        //        Publish(command);
        //    }
        //}
    }

    public Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, InboxMessageCreatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        var item = domainEvent.AggregateEvent.InboxMessageItem;

        //var command = new AddInboxMessageIdToOutboxMessageCommand(
        //    MessageId.Create(item.SenderPeer.PeerId,
        //        domainEvent.AggregateEvent.SenderMessageId),
        //    _state.RequestInfo,
        //    item.OwnerPeer.PeerId,
        //    item.MessageId);
        //Publish(command);

        var command = new ReceiveInboxMessageCommand(
            DialogId.Create(domainEvent.AggregateEvent.InboxMessageItem.OwnerPeer.PeerId,
                domainEvent.AggregateEvent.InboxMessageItem.ToPeer),
            domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.InboxMessageItem.MessageId,
            domainEvent.AggregateEvent.InboxMessageItem.OwnerPeer.PeerId,
            domainEvent.AggregateEvent.InboxMessageItem.ToPeer);
        Publish(command);

        return HandleReceiveInboxMessageCompletedAsync(item);
    }

    private async Task CreateInboxMessageAsync(OutboxMessageCreatedEvent outbox)
    {
        switch (outbox.OutboxMessageItem.ToPeer.PeerType)
        {
            case PeerType.User:
                await CreateInboxMessageForUserAsync(outbox.OutboxMessageItem.ToPeer.PeerId);
                break;
            case PeerType.Chat:
                if (outbox.ChatMembers?.Count > 0)
                {
                    foreach (var chatMemberUserId in outbox.ChatMembers)
                    {
                        if (chatMemberUserId == outbox.RequestInfo.UserId)
                        {
                            continue;
                        }

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                        CreateInboxMessageForUserAsync(chatMemberUserId);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    }
                }

                break;
        }
    }

    private async Task HandleReceiveInboxMessageCompletedAsync(MessageItem inboxMessageItem)
    {
        var pts = await _idGenerator.NextIdAsync(IdType.Pts, inboxMessageItem.OwnerPeer.PeerId);
        Emit(new ReceiveInboxMessageCompletedEvent(inboxMessageItem, pts, string.Empty));

        if (_state.IsCreateInboxMessagesCompleted())
        {
            var item = _state.MessageItem;
            var command = new AddInboxItemsToOutboxMessageCommand(
                MessageId.Create(item.SenderPeer.PeerId,
                    item.MessageId),
                _state.RequestInfo,
                _state.InboxItems
                );
            Publish(command);

            await CompleteAsync();
        }
    }

    private async Task HandleSendOutboxMessageCompletedAsync()
    {
        var pts = await _idGenerator.NextIdAsync(IdType.Pts, _state.MessageItem.OwnerPeer.PeerId);
        var linkedChannelId = _state.LinkedChannelId;
        //var globalSeqNo = _state.MessageItem.ToPeer.PeerType == PeerType.Channel ? await _idGenerator.NextLongIdAsync(IdType.GlobalSeqNo) : 0;

        Emit(new SendOutboxMessageCompletedEvent(_state.RequestInfo,
            _state.MessageItem,
            _state.MentionedUserIds,
            pts,
            _state.GroupItemCount,
            linkedChannelId,
            null//,
            //globalSeqNo
            /*_state.BotUserIds*/));

        if (_state.MessageItem.ToPeer.PeerType == PeerType.Channel)
        {
            SetChannelPts(_state.MessageItem.ToPeer.PeerId, pts, _state.MessageItem.MessageId);

            if (_state.LinkedChannelId.HasValue && _state.MessageItem.SendMessageType != SendMessageType.MessageService)
            {
                ForwardBroadcastMessageToLinkedChannel(_state.LinkedChannelId.Value, _state.MessageItem.MessageId);
            }

            // handle reply discussion message
            HandleReplyDiscussionMessage();

            await CompleteAsync();
        }
    }
    private void HandleReplyDiscussionMessage()
    {
        //if (_state.ReplyToMessageSavedFromPeerId != 0)
        //{
        //    var savedFromPeerId = _state.ReplyToMessageSavedFromPeerId;
        //    Emit(new ReplyToChannelMessageCompletedEvent2(_state.ReplyToMsgId,
        //        _state.MessageItem!.ToPeer.PeerId,
        //        _state.Pts,
        //        _state.MessageItem.MessageId,
        //        savedFromPeerId,
        //        _state.ReplyToMessageSavedFromMsgId,
        //        _state.RecentRepliers!));
        //}
    }
    private void ForwardBroadcastMessageToLinkedChannel(long linkedChannelId, int messageId)
    {
        var aggregateId = MessageId.Create(_state.MessageItem!.OwnerPeer.PeerId, messageId);
        var fromPeer = _state.MessageItem!.ToPeer;
        var toPeer = new Peer(PeerType.Channel, linkedChannelId);
        var randomBytes = new byte[8];
        Random.Shared.NextBytes(randomBytes);
        var command = new StartForwardMessageCommand(aggregateId,
            _state.RequestInfo,
            fromPeer,
            toPeer,
            new List<int> { messageId },
            new List<long> { BitConverter.ToInt64(randomBytes) },
            true,
            Guid.NewGuid()
        );
        Publish(command);
    }

    //private void ReplyToMessage(long ownerPeerId, int messageId)
    //{
    //    var aggregateId = MessageId.Create(ownerPeerId, messageId);
    //    var command = new ReplyToMessageCommand(aggregateId, _state.RequestInfo, messageId);
    //    Publish(command);
    //}

    //private void StartReplyToMessage(long ownerPeerId, Peer replierPeer, int replyToMsgId)
    //{
    //    var command =
    //        new StartReplyToMessageCommand(MessageId.Create(ownerPeerId, replyToMsgId), _state.RequestInfo, replierPeer, replyToMsgId);
    //    Publish(command);
    //}


    private void SetChannelPts(long channelId, int pts, int messageId)
    {
        var command = new SetChannelPtsCommand(ChannelId.Create(channelId), _state.MessageItem!.SenderPeer.PeerId, pts, messageId, _state.MessageItem.Date);
        Publish(command);
    }
    private async Task CreateInboxMessageForUserAsync(long inboxOwnerUserId)
    {
        var outMessageItem = _state.MessageItem!;
        var toPeer = outMessageItem.ToPeer.PeerType == PeerType.Chat ? outMessageItem.ToPeer : outMessageItem.OwnerPeer;

        var replyTo = outMessageItem.InputReplyTo.ToBytes().ToTObject<IInputReplyTo>();

        if (_state.ReplyToMsgItems.TryGetValue(inboxOwnerUserId, out var replyToMsgId))
        {
            switch (replyTo)
            {
                case TInputReplyToMessage inputReplyToMessage:
                    inputReplyToMessage.ReplyToMsgId = replyToMsgId;
                    break;
                case TInputReplyToStory inputReplyToStory:
                    inputReplyToStory.StoryId=replyToMsgId;
                    break;
            }
        }

        // Channel only create outbox message,
        // Use IdType.MessageId and IdType.ChannelMessageId will not be used
        var inboxMessageId = await _idGenerator.NextIdAsync(IdType.MessageId, inboxOwnerUserId);
        var aggregateId = MessageId.Create(inboxOwnerUserId, inboxMessageId);
        var inboxMessageItem = new MessageItem(
            new Peer(PeerType.User, inboxOwnerUserId),
            toPeer,
            outMessageItem.SenderPeer,
            inboxMessageId,
            outMessageItem.Message,
            outMessageItem.Date,
            outMessageItem.RandomId,
            false,
            outMessageItem.SendMessageType,
            outMessageItem.MessageType,
            outMessageItem.MessageSubType,
            replyTo,
            outMessageItem.MessageActionData,
            outMessageItem.MessageActionType,
            outMessageItem.Entities,
            outMessageItem.Media,
            outMessageItem.GroupId,
            outMessageItem.Post,
            outMessageItem.FwdHeader,
            outMessageItem.Views,
            outMessageItem.PollId,
            outMessageItem.ReplyMarkup
        );

        var command = new CreateInboxMessageCommand(aggregateId, _state.RequestInfo, inboxMessageItem, outMessageItem.MessageId);
        Publish(command);
    }
}