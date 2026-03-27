namespace MyTelegram.Domain.Sagas;

public class MessageReplyCreatedSagaEvent(long postChannelId, int postMessageId, long channelId, int messageId)
    : AggregateEvent<ForwardMessageSaga, ForwardMessageSagaId>
{
    public long PostChannelId { get; } = postChannelId;
    public int PostMessageId { get; } = postMessageId;
    public long ChannelId { get; } = channelId;
    public int MessageId { get; } = messageId;
}

public class ForwardMessageSaga : MyInMemoryAggregateSaga<ForwardMessageSaga, ForwardMessageSagaId, ForwardMessageSagaLocator>,
        ISagaIsStartedBy<TempAggregate, TempId, ForwardMessagesStartedEvent>,
        ISagaHandles<MessageAggregate, MessageId, MessageForwardedEvent>
{
    private readonly IDataEncryptionHelper2 _dataEncryptionHelper2;
    private readonly IIdGenerator _idGenerator;
    private readonly ForwardMessageState _state = new();

    public ForwardMessageSaga(ForwardMessageSagaId id, IEventStore eventStore,
        IDataEncryptionHelper2 dataEncryptionHelper2,
        IIdGenerator idGenerator) : base(id, eventStore)
    {
        _dataEncryptionHelper2 = dataEncryptionHelper2;
        _idGenerator = idGenerator;
        Register(_state);
    }

    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, MessageForwardedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var outMessageId = await SendMessageToTargetPeerAsync(domainEvent.AggregateEvent);
        Emit(new ForwardSingleMessageSuccessSagaEvent());

        if (_state.ForwardFromLinkedChannel)
        {
            Emit(new MessageReplyCreatedSagaEvent(domainEvent.AggregateEvent.OriginalMessageItem.ToPeer.PeerId, domainEvent.AggregateEvent.OriginalMessageItem.MessageId, _state.ToPeer.PeerId, outMessageId));
        }

        await HandleForwardCompletedAsync();
    }


    public Task HandleAsync(IDomainEvent<TempAggregate, TempId, ForwardMessagesStartedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        Emit(new ForwardMessageSagaStartedSagaEvent(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.FromPeer,
            domainEvent.AggregateEvent.ToPeer,
            domainEvent.AggregateEvent.MessageIds,
            domainEvent.AggregateEvent.RandomIds,
            domainEvent.AggregateEvent.ForwardFromLinkedChannel,
            domainEvent.AggregateEvent.Post,
            domainEvent.AggregateEvent.TtlPeriod,
            domainEvent.AggregateEvent.FromNames,
            domainEvent.AggregateEvent.SendAs,
            domainEvent.AggregateEvent.DropAuthor,
            domainEvent.AggregateEvent.DropMediaCaptions
        ));
        ForwardMessage(domainEvent.AggregateEvent);
        return Task.CompletedTask;
    }

    private void ForwardMessage(ForwardMessagesStartedEvent aggregateEvent)
    {
        var ownerPeerId = _state.FromPeer.PeerType == PeerType.Channel
            ? _state.FromPeer.PeerId
            : _state.RequestInfo.UserId;
        var index = 0;
        foreach (var messageId in aggregateEvent.MessageIds)
        {
            var randomId = aggregateEvent.RandomIds[index];
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

    private async Task<int> SendMessageToTargetPeerAsync(MessageForwardedEvent aggregateEvent)
    {
        var item = aggregateEvent.OriginalMessageItem;
        var isForwardToSavedMessages = _state.ToPeer.PeerId == _state.RequestInfo.UserId;
        long? postChannelId = null;
        int? postMessageId = null;
        Peer? savedPeerId;
        Peer? sendAs = _state.SendAs;
        var isOut = true;
        string? fromName = null;
        _state.FromNames?.TryGetValue(item.SenderUserId, out fromName);

        var fwd = new MessageFwdHeader
        {
            Imported = false,
            SavedOut = false,
            FromName = fromName,
            SavedDate = null,
            PsaType = null,
            Date = item.Date
        };

        if (_state.ForwardFromLinkedChannel)
        {
            fwd.ChannelPost = item.MessageId;
            fwd.PostAuthor = item.PostAuthor;
            fwd.SavedFromPeer = item.ToPeer;
            fwd.SavedFromMsgId = item.MessageId;
            fwd.FromId = item.ToPeer;

            fwd.ForwardFromLinkedChannel = true;

            postChannelId = item.ToPeer.PeerId;
            postMessageId = item.MessageId;
            savedPeerId = _state.FromPeer;
            isOut = false;
        }
        else
        {
            switch (item.ToPeer.PeerType)
            {
                case PeerType.Channel:
                    fwd.FromId = item.SenderPeer;
                    fwd.SavedFromMsgId = item.MessageId;
                    fwd.SavedFromPeer = item.OwnerPeer;
                    fwd.PostAuthor = item.PostAuthor;
                    if (item.Post)
                    {
                        fwd.FromId = item.ToPeer;
                        fwd.ChannelPost = item.MessageId;
                    }

                    if (item.SendAs != null)
                    {
                        // When item.SendAs in a message is not null, it indicates that the message comes from a channel post or a discussion group admin.
                        // In this case, fwd.FromId should be set to the channel's ID or the discussion group's ID.
                        fwd.FromId = item.ToPeer;
                    }

                    if (item.IsForwardFromChannelPost)
                    {
                        fwd.FromId = item.ToPeer;
                        fwd.ChannelPost = item.FwdHeader?.ChannelPost;

                        if (item.FwdHeader != null)
                        {
                            fwd.FromId = item.FwdHeader.FromId;
                            fwd.ChannelPost = item.FwdHeader.ChannelPost;
                            fwd.SavedFromMsgId = item.FwdHeader.SavedFromMsgId;
                            fwd.SavedFromPeer = item.OwnerPeer;
                        }
                    }

                    if (item.FwdHeader != null)
                    {
                        fwd.ChannelPost = item.FwdHeader.ChannelPost;
                    }

                    break;

                case PeerType.User:
                    fwd.FromId = item.SenderPeer;
                    break;
            }

            if (isForwardToSavedMessages)
            {
                fwd.SavedOut = item.SenderUserId == aggregateEvent.RequestInfo.UserId;
                fwd.SavedFromMsgId = item.MessageId;
                fwd.SavedFromPeer = _state.FromPeer;

                sendAs = aggregateEvent.OriginalMessageItem.SendAs;
            }
            if (!string.IsNullOrEmpty(fwd.FromName))
            {
                fwd.FromId = null;
                savedPeerId = MyTelegramConsts.AnonymousUserId.ToUserPeer();
            }
            else
            {
                savedPeerId = null;
            }
        }

        var selfUserId = _state.RequestInfo.UserId;
        var ownerPeerId = _state.ToPeer.PeerType == PeerType.Channel
            ? _state.ToPeer.PeerId
            : selfUserId;
        var senderPeer = new Peer(PeerType.User, _state.RequestInfo.UserId);
        if (_state.ForwardFromLinkedChannel)
        {
            senderPeer = item.ToPeer;
        }
        var outMessageId = await _idGenerator.NextIdAsync(IdType.MessageId, ownerPeerId);

        var ownerPeer = _state.ToPeer.PeerType == PeerType.Channel
            ? _state.ToPeer
            : senderPeer;
        var toPeer = _state.ToPeer;
        string message = item.Message;

        if (_state.DropAuthor)
        {
            fwd = null;
        }

        if (_state.DropMediaCaptions)
        {
            message = string.Empty;
        }

        Memory<byte>? encryptedMessageData = null;
        Memory<byte>? encryptedInboxMessageData = null;
        if (item.EncryptedData?.Length > 0)
        {
            // 4:keyId 12:nonce 16:tag messageKey:32 8:salt
            var length = item.EncryptedData.Value.Length + 72;
            encryptedMessageData = new byte[length];
            var decryptedMessage = _dataEncryptionHelper2.DecryptMessage(item.OwnerPeer.PeerId, item.EncryptedData.Value);
            var count = _dataEncryptionHelper2.Encrypt(ownerPeer.PeerId, decryptedMessage, encryptedMessageData.Value.Span);
            encryptedMessageData = encryptedMessageData.Value[..count];

            if (toPeer.PeerType != PeerType.Channel)
            {
                encryptedInboxMessageData = new byte[length];
                var count2 =
                    _dataEncryptionHelper2.Encrypt(toPeer.PeerId, decryptedMessage, encryptedInboxMessageData.Value.Span);
                encryptedInboxMessageData = encryptedInboxMessageData.Value[..count2];
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(message))
            {
                var length = message.Length * 4 + 72;
                encryptedMessageData = new byte[length];
                var count = _dataEncryptionHelper2.Encrypt(ownerPeer.PeerId, message, encryptedMessageData.Value.Span);
                encryptedMessageData = encryptedMessageData.Value[..count];

                if (toPeer.PeerType != PeerType.Channel)
                {
                    encryptedInboxMessageData = new byte[length];
                    var count2 =
                        _dataEncryptionHelper2.Encrypt(toPeer.PeerId, message, encryptedInboxMessageData.Value.Span);
                    encryptedInboxMessageData = encryptedInboxMessageData.Value[..count2];
                }
            }
        }

        var messageItem = new MessageItem(
            ownerPeer,
            toPeer,
            senderPeer,
            aggregateEvent.OriginalMessageItem.SenderUserId,
            outMessageId,
            message,
            DateTime.UtcNow.ToTimestamp(),
            aggregateEvent.RandomId,
            isOut,
            SendMessageType.Text,
            MessageType.Text,
            MessageSubType.ForwardMessage,
            //InputReplyTo: item.InputReplyTo,
            Entities: item.Entities,
            Media: item.Media,
            FwdHeader: fwd,
            Views: item.Views,
            PollId: item.PollId,
            EditHide: _state.ForwardFromLinkedChannel,
            Reply: _state.ForwardFromLinkedChannel ? item.Reply : null,
            IsForwardFromChannelPost: _state.ForwardFromLinkedChannel,
            PostChannelId: postChannelId,
            PostMessageId: postMessageId,
            Post: _state.Post,
            SendAs: sendAs,
            TtlPeriod: _state.TtlPeriod,
            Pinned: _state.ForwardFromLinkedChannel,
            Silent: aggregateEvent.OriginalMessageItem.Silent,
            SavedPeerId: savedPeerId,
            PostAuthor: aggregateEvent.OriginalMessageItem.PostAuthor,
             EncryptedData: encryptedMessageData,
            InboxMessageEncryptedData: encryptedInboxMessageData
        );

        var reqMsgId = _state.ForwardFromLinkedChannel ? 0 : _state.RequestInfo.ReqMsgId;

        // Only the first message uses reqMsgId to reply to the client's RPC request
        // Other messages are sent to the client via online push
        // If reqMsgId==0, a push message will be sent to the target peer
        if (_state.ForwardCount > 0)
        {
            reqMsgId = 0;
        }

        var command = new StartSendMessageCommand(TempId.New, aggregateEvent.RequestInfo with
        {
            RequestId = Guid.NewGuid(),
            ReqMsgId = reqMsgId
        },
            [new SendMessageItem(messageItem)]);

        Publish(command);

        return outMessageId;
    }
}
