namespace MyTelegram.Domain.Sagas;

public class ReplyBroadcastChannelCompletedSagaEvent(long channelId, int messageId, MessageReply reply)
    : AggregateEvent<SendMessageSaga, SendMessageSagaId>
{
    public long ChannelId { get; } = channelId;
    public int MessageId { get; } = messageId;
    public MessageReply Reply { get; } = reply;
}

public class PostChannelIdUpdatedSagaEvent(long channelId, int messageId, long postChannelId, int postMessageId)
    : AggregateEvent<SendMessageSaga, SendMessageSagaId>
{
    public long ChannelId { get; } = channelId;
    public int MessageId { get; } = messageId;
    public long PostChannelId { get; } = postChannelId;
    public int PostMessageId { get; } = postMessageId;
}

public class SendMessageSaga : MyInMemoryAggregateSaga<SendMessageSaga, SendMessageSagaId, SendMessageSagaLocator>,
    //ISagaIsStartedBy<MessageAggregate, MessageId, OutboxMessageCreatedEvent>,
    ISagaIsStartedBy<TempAggregate, TempId, SendMessageStartedEvent>,
    ISagaHandles<MessageAggregate, MessageId, OutboxMessageCreatedEvent>,
    ISagaHandles<MessageAggregate, MessageId, InboxMessageCreatedEvent>,
    ISagaHandles<MessageAggregate, MessageId, ReplyChannelMessageCompletedEvent>
{
    private readonly IIdGenerator _idGenerator;
    private readonly SendMessageSagaState _state = new();
    public SendMessageSaga(SendMessageSagaId id, IEventStore eventStore, IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
        Register(_state);
    }

    public Task HandleAsync(IDomainEvent<TempAggregate, TempId, SendMessageStartedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        return StartSendMessageAsync(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.SendMessageItems,
            domainEvent.AggregateEvent.ClearDraft,
            domainEvent.AggregateEvent.IsSendQuickReplyMessages,
            domainEvent.AggregateEvent.IsSendGroupedMessages
        );
    }

    private async Task StartSendMessageAsync(RequestInfo requestInfo, List<SendMessageItem> sendMessageItems,
        bool clearDraft,
        bool isSendQuickReplyMessages,
        bool isSendGroupedMessages
        )
    {
        var newItems = new List<SendMessageItem>();
        foreach (var sendMessageItem in sendMessageItems)
        {
            var messageItem = sendMessageItem.MessageItem;

            var messageId = messageItem.MessageId;

            if (messageId == 0)
            {
                var idType = IdType.MessageId;
                if (messageItem.ScheduleDate != null)
                {
                    idType = IdType.ScheduleMessageId;
                }
                else if (messageItem.QuickReplyItem != null)
                {
                    idType = IdType.QuickReplyMessageId;
                }
                messageId = await _idGenerator.NextIdAsync(idType, messageItem.OwnerPeer.PeerId);
            }

            if (messageItem.Pts == 0)
            {
                var pts = await _idGenerator.NextIdAsync(IdType.Pts, messageItem.OwnerPeer.PeerId);

                messageItem = messageItem with { Pts = pts, MessageId = messageId };
                var newItem = sendMessageItem with { MessageItem = messageItem };
                newItems.Add(newItem);
            }
            else
            {
                messageItem = messageItem with { MessageId = messageId };
                var newItem = sendMessageItem with { MessageItem = messageItem };
                newItems.Add(newItem);
            }
        }

        Emit(new SendMessageStartedSagaEvent(requestInfo,
            newItems,
            [],
            clearDraft,
            isSendQuickReplyMessages,
            isSendGroupedMessages
        ));

        foreach (var item in newItems)
        {
            var messageItem = item.MessageItem;
            var command = new CreateOutboxMessageCommand(
                MessageId.Create(messageItem.OwnerPeer.PeerId,
                    messageItem.MessageId,
                    messageItem.QuickReplyItem != null
                ),
                requestInfo,
                messageItem,
                item.MentionedUserIds,
                messageItem.ReplyToMsgItems,
                item.ClearDraft,
                linkedChannelId: messageItem.LinkedChannelId,
                chatMembers: item.ChatMembers
            );
            Publish(command);
        }
    }

    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, OutboxMessageCreatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        Emit(new OutboxMessageCreatedSagaEvent(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.OutboxMessageItem,
            domainEvent.AggregateEvent.MentionedUserIds,
            domainEvent.AggregateEvent.ReplyToMsgItems,
            domainEvent.AggregateEvent.ChatMembers
            ));
        await HandleSendOutboxMessageCompletedAsync(domainEvent.AggregateEvent.OutboxMessageItem);

        await CreateInboxMessageAsync(domainEvent.AggregateEvent);

        CreateMentions(domainEvent.AggregateEvent.MentionedUserIds, domainEvent.AggregateEvent.OutboxMessageItem.MessageId);
    }

    private void CreateMentions(List<long>? mentionedUserIds, int messageId)
    {
        if (mentionedUserIds?.Count > 0)
        {
            // Only create mention for super group members

            //foreach (var mentionedUserId in mentionedUserIds)
            //{
            //    var command = new CreateMentionCommand(DialogId.Create(mentionedUserId, _state.FirstMessageItem.MessageItem.ToPeer),
            //        mentionedUserId, /*_state.MessageItem.ToPeer.PeerId,*/ messageId);
            //    Publish(command);
            //}
        }
    }

    public Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, InboxMessageCreatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        var item = domainEvent.AggregateEvent.InboxMessageItem;

        var command = new ReceiveInboxMessageCommand(
            DialogId.Create(domainEvent.AggregateEvent.InboxMessageItem.OwnerPeer.PeerId,
                domainEvent.AggregateEvent.InboxMessageItem.ToPeer),
            domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.InboxMessageItem.MessageId,
            domainEvent.AggregateEvent.InboxMessageItem.OwnerPeer.PeerId,
            domainEvent.AggregateEvent.InboxMessageItem.ToPeer);
        Publish(command);

        return HandleReceiveInboxMessageCompletedAsync(item, domainEvent.AggregateEvent.SenderMessageId);
    }

    private async Task CreateInboxMessageAsync(OutboxMessageCreatedEvent aggregateEvent)
    {
        switch (aggregateEvent.OutboxMessageItem.ToPeer.PeerType)
        {
            case PeerType.User:
                await CreateInboxMessageForUserAsync(aggregateEvent.OutboxMessageItem, aggregateEvent.OutboxMessageItem.ToPeer.PeerId);
                break;
            case PeerType.Chat:
                if (aggregateEvent.ChatMembers?.Count > 0)
                {
                    foreach (var chatMemberUserId in aggregateEvent.ChatMembers)
                    {
                        if (chatMemberUserId == aggregateEvent.RequestInfo.UserId)
                        {
                            continue;
                        }

                        await CreateInboxMessageForUserAsync(aggregateEvent.OutboxMessageItem, chatMemberUserId);
                    }
                }

                break;
        }
    }

    private async Task HandleReceiveInboxMessageCompletedAsync(MessageItem inboxMessageItem, int senderMessageId)
    {
        var pts = inboxMessageItem.Pts;// await _idGenerator.NextIdAsync(IdType.Pts, inboxMessageItem.OwnerPeer.PeerId);
        var newInboxMessageItem = inboxMessageItem with { Pts = pts };
        Emit(new InboxMessageCreatedSagaEvent(_state.RequestInfo, newInboxMessageItem));

        _state.UserInboxItems.TryGetValue(inboxMessageItem.BatchId ?? Guid.Empty, out var inboxItems);

        var command = new AddInboxItemsToOutboxMessageCommand(
            MessageId.Create(inboxMessageItem.SenderPeer.PeerId,
                senderMessageId, inboxMessageItem.QuickReplyItem != null),
            //_state.RequestInfo,
            inboxItems ?? []
        );
        Publish(command);

        if (_state.IsCreateInboxMessagesCompleted())
        {
            Emit(new ReceiveInboxMessageCompletedSagaEvent(_state.InboxMessageItems, _state.IsSendQuickReplyMessages, _state.IsSendGroupedMessages));

            await CompleteAsync();
        }
    }

    private async Task HandleSendOutboxMessageCompletedAsync(MessageItem outboxMessageItem)
    {
        var pts = outboxMessageItem.Pts;// await _idGenerator.NextIdAsync(IdType.Pts, _state.MessageItem.OwnerPeer.PeerId);
        //var linkedChannelId = _state.LinkedChannelId;
        //var globalSeqNo = _state.MessageItem.ToPeer.PeerType == PeerType.Channel ? await _idGenerator.NextLongIdAsync(IdType.GlobalSeqNo) : 0;

        if (_state.IsSendOutboxMessageCompleted)
        {
            Emit(new SendOutboxMessageCompletedSagaEvent(_state.RequestInfo,
                _state.SendMessageItems.Select(p => p.MessageItem).ToList(),
                //_state.MentionedUserIds,
                outboxMessageItem.MentionedUserIds,
                _state.IsSendQuickReplyMessages,
                _state.IsSendGroupedMessages,
                []
                ));
        }

        var defaultHistoryTtl = outboxMessageItem.IsTtlFromDefaultSetting ? outboxMessageItem.TtlPeriod : null;
        var command = new UpdateDialogCommand(
            DialogId.Create(outboxMessageItem.SenderUserId, outboxMessageItem.ToPeer),
            _state.RequestInfo with { RequestId = Guid.NewGuid() },
            outboxMessageItem.SenderUserId,
            outboxMessageItem.ToPeer,
            outboxMessageItem.MessageId,
            pts,
            defaultHistoryTtl,
            false
        );
        Publish(command);

        if (outboxMessageItem.ToPeer.PeerType == PeerType.Channel)
        {
            SetChannelPts(outboxMessageItem);

            if (_state.FirstMessageItem.MessageItem.LinkedChannelId.HasValue && outboxMessageItem.SendMessageType != SendMessageType.MessageService)
            {
                ForwardBroadcastMessageToLinkedChannel(outboxMessageItem);
            }

            // handle reply discussion message
            if (!HandleReplyDiscussionMessage(outboxMessageItem))
            {
                await CompleteAsync();
            }
        }
    }


    private bool HandleReplyDiscussionMessage(MessageItem outboxMessageItem)
    {
        if (outboxMessageItem is { InputReplyTo: not null, ToPeer.PeerType: PeerType.Channel })
        {
            switch (outboxMessageItem.InputReplyTo)
            {
                case TInputReplyToMessage inputReplyToMessage:
                    ReplyToMessage(outboxMessageItem, inputReplyToMessage.ReplyToMsgId);
                    return true;
                case TInputReplyToStory inputReplyToStory:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return false;
    }
    private void ForwardBroadcastMessageToLinkedChannel(MessageItem outboxMessageItem)
    {
        if (outboxMessageItem.LinkedChannelId == null)
        {
            return;
        }
        var fromPeer = outboxMessageItem.ToPeer;
        var toPeer = new Peer(PeerType.Channel, outboxMessageItem.LinkedChannelId!.Value);
        var command = new StartForwardMessagesCommand(TempId.New,
            _state.RequestInfo,
            false,
            false,
            false,
            false,
            false,
            false,
            fromPeer,
            toPeer,
            [outboxMessageItem.MessageId],
            [Random.Shared.NextInt64()],
            null,
            outboxMessageItem.SendAs,
            true,
            false
        );
        Publish(command);
    }

    private void ReplyToMessage(MessageItem outboxMessageItem, int replyToMessageId)
    {
        // long ownerPeerId, int messageId, int repliesPts, int maxMessageId
        // ReplyToMessage(outboxMessageItem.ToPeer.PeerId, inputReplyToMessage.ReplyToMsgId, outboxMessageItem.Pts, outboxMessageItem.MessageId);
        var replierPeer = outboxMessageItem.SendAs ?? _state.RequestInfo.UserId.ToUserPeer();
        var command = new ReplyToMessageCommand(MessageId.Create(outboxMessageItem.ToPeer.PeerId, replyToMessageId),
            _state.RequestInfo, replierPeer, outboxMessageItem.Pts, outboxMessageItem.MessageId);
        Publish(command);
    }

    private void SetChannelPts(MessageItem outboxMessageItem)
    {
        //long channelId, int pts, int messageId
        var command = new SetChannelPtsCommand(ChannelId.Create(outboxMessageItem.ToPeer.PeerId),
            outboxMessageItem.SenderPeer.PeerId,
            outboxMessageItem.Pts,
            outboxMessageItem.MessageId,
            outboxMessageItem.Date);
        Publish(command);
    }
    private async Task CreateInboxMessageForUserAsync(MessageItem outboxMessageItem, long inboxOwnerUserId)
    {
        var outMessageItem = outboxMessageItem;
        var toPeer = outMessageItem.ToPeer.PeerType == PeerType.Chat ? outMessageItem.ToPeer : outMessageItem.OwnerPeer;

        var replyTo = outMessageItem.InputReplyTo;
        var replyToMsgItems = outboxMessageItem.ReplyToMsgItems?.ToDictionary(k => k.UserId, v => v.MessageId) ?? new();
        if (replyToMsgItems.TryGetValue(inboxOwnerUserId, out var replyToMsgId))
        {
            switch (replyTo)
            {
                case TInputReplyToMessage inputReplyToMessage:
                    inputReplyToMessage.ReplyToMsgId = replyToMsgId;
                    break;
                case TInputReplyToStory inputReplyToStory:
                    inputReplyToStory.StoryId = replyToMsgId;
                    break;
            }
        }

        var inboxMessageId = await _idGenerator.NextIdAsync(IdType.MessageId, inboxOwnerUserId);
        var pts = await _idGenerator.NextIdAsync(IdType.Pts, inboxOwnerUserId);
        var aggregateId = MessageId.Create(inboxOwnerUserId, inboxMessageId);
        var inboxMessageItem = outMessageItem with
        {
            OwnerPeer = new Peer(PeerType.User, inboxOwnerUserId),
            ToPeer = toPeer,
            MessageId = inboxMessageId,
            IsOut = false,
            InputReplyTo = replyTo,
            Pts = pts
        };

        var command = new CreateInboxMessageCommand(aggregateId, _state.RequestInfo, inboxMessageItem, outMessageItem.MessageId);
        Publish(command);
    }

    public Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, ReplyChannelMessageCompletedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        if (domainEvent.AggregateEvent.PostChannelId.HasValue && domainEvent.AggregateEvent.PostMessageId.HasValue)
        {
            Emit(new ReplyBroadcastChannelCompletedSagaEvent(domainEvent.AggregateEvent.PostChannelId.Value, domainEvent.AggregateEvent.PostMessageId.Value, domainEvent.AggregateEvent.Reply));
            Emit(new PostChannelIdUpdatedSagaEvent(_state.FirstMessageItem.MessageItem.OwnerPeer.PeerId, _state.FirstMessageItem.MessageItem.MessageId, domainEvent.AggregateEvent.PostChannelId.Value, domainEvent.AggregateEvent.PostMessageId.Value));
        }

        return CompleteAsync(cancellationToken);
    }


}