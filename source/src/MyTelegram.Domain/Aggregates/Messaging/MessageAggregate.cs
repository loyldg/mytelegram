namespace MyTelegram.Domain.Aggregates.Messaging;

[EnableAutoGeneration]
public class MessageAggregate : SnapshotAggregateRoot<MessageAggregate, MessageId, MessageSnapshot>
{
    private readonly MessageState _state = new();

    public MessageAggregate(MessageId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    [DoNotInheritRequestCommand]
    public void UpdateMessagePinned(RequestInfo requestInfo, bool pinned)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var ownerPeerId = _state.MessageItem.OwnerPeer.PeerId;
        Emit(new MessagePinnedUpdatedEvent(requestInfo, ownerPeerId, _state.MessageItem.MessageId, pinned, _state.MessageItem.ToPeer, _state.MessageItem.Post));
    }

    [DoNotInheritRequestCommand]
    public void UnpinMessage(RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var ownerPeerId = _state.MessageItem.OwnerPeer.PeerId;
        Emit(new MessageUnpinnedEvent(requestInfo, ownerPeerId, _state.MessageItem.MessageId));
    }

    public void AddInboxItemsToOutboxMessage(List<InboxItem> inboxItems)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new InboxItemsAddedToOutboxMessageEvent(inboxItems));
    }

    /// <summary>
    ///     Sender's message id and receiver's message id are independent,add receiver's message id to sender,delete messages
    ///     and pin messages need this
    /// </summary>
    /// <param name="inboxOwnerPeerId"></param>
    /// <param name="inboxMessageId"></param>
    public void AddInboxMessageIdToOutboxMessage(long inboxOwnerPeerId,
        int inboxMessageId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var inboxItem = new InboxItem(inboxOwnerPeerId, inboxMessageId);
        Emit(new InboxMessageIdAddedToOutboxMessageEvent(inboxItem));
    }

    public void CreateInboxMessage(
        RequestInfo requestInfo,
        MessageItem inboxMessageItem,
        int senderMessageId)
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new InboxMessageCreatedEvent(requestInfo, inboxMessageItem, senderMessageId));
    }

    public void CreateOutboxMessage(RequestInfo requestInfo,
        MessageItem outboxMessageItem,
        List<long>? mentionedUserIds = null,
        List<ReplyToMsgItem>? replyToMsgItems = null,
        //IReplyTo? replyTo,
        //IInputReplyTo? inputReplyTo, 
        bool clearDraft = true,
        int groupItemCount = 1,
        long? linkedChannelId = null,
        List<long>? chatMembers = null)
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        if (outboxMessageItem.Post)
        {
            var reply = new MessageReply(linkedChannelId, 0, 0, null, null);
            outboxMessageItem = outboxMessageItem with { Views = 1, Reply = reply };
        }
        if (!outboxMessageItem.BatchId.HasValue)
        {
            outboxMessageItem = outboxMessageItem with { BatchId = SequentialGuid.Create() };
        }

        Emit(new OutboxMessageCreatedEvent(requestInfo,
            outboxMessageItem,
            mentionedUserIds,
            replyToMsgItems,
            clearDraft,
            groupItemCount,
            linkedChannelId,
            chatMembers
        ));
    }

    [DoNotInheritRequestCommand]
    public void DeleteChannelMessage(RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var channelId = _state.MessageItem.OwnerPeer.PeerId;
        var postChannelId = _state.MessageItem.FwdHeader?.SavedFromPeer?.PeerId;
        var postMessageId = _state.MessageItem.FwdHeader?.SavedFromMsgId;
        Emit(new ChannelMessageDeletedEvent(requestInfo,
            channelId,
            _state.MessageItem.MessageId,
            _state.MessageItem.IsForwardFromChannelPost,
            postChannelId,
            postMessageId));
    }

    [DoNotInheritRequestCommand]
    public void DeleteInboxMessage(RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new InboxMessageDeletedEvent(
            requestInfo,
            _state.MessageItem.OwnerPeer.PeerId,
            _state.MessageItem.MessageId,
            _state.SenderMessageId
        ));
    }

    public void DeleteMessage(RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var ownerPeerId = _state.MessageItem.OwnerPeer.PeerId;
        var senderPeerId = _state.MessageItem.SenderPeer.PeerId;
        Emit(new MessageDeleted4Event(
            requestInfo,
            _state.MessageItem.ToPeer,
            ownerPeerId,
            _state.MessageItem.MessageId,
            _state.MessageItem.IsOut,
            senderPeerId,
            _state.SenderMessageId));
    }

    public void DeleteOtherPartyMessage(RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var ownerPeerId = _state.MessageItem.OwnerPeer.PeerId;
        Emit(new OtherPartyMessageDeletedEvent(
            requestInfo,
            ownerPeerId,
            _state.MessageItem.MessageId));
    }

    public void EditInboxMessage(
        RequestInfo requestInfo,
        int messageId,
        string newMessage,
        int editDate,
        TVector<IMessageEntity>? entities,
        IMessageMedia? media,
        IReplyMarkup? replyMarkup,
        bool invertMedia,
        List<string>? hashtags,
        ReadOnlyMemory<byte>? encryptedData = null)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);

        if (string.IsNullOrEmpty(newMessage))
        {
            newMessage = _state.MessageItem.Message;
        }

        var newEncryptedData = _state.MessageItem.EncryptedData;
        if (encryptedData != null)
        {
            newEncryptedData = encryptedData;
            newMessage = null;
        }

        var oldMessageItem = _state.MessageItem;
        var editHide = oldMessageItem.SenderUserId == MyTelegramConsts.BotFatherUserId && (replyMarkup != null || oldMessageItem.ReplyMarkup != null);
        media ??= oldMessageItem.Media;

        var newMessageItem = oldMessageItem with
        {
            Message = newMessage,
            Entities = entities,
            Media = media,
            ReplyMarkup = replyMarkup,
            EditDate = editDate,
            EditHide = editHide,
            InvertMedia = invertMedia,
            Hashtags = hashtags,
            EncryptedData = newEncryptedData
        };

        Emit(new InboxMessageEditedEventV2(
            requestInfo,
            oldMessageItem,
            newMessageItem));
    }

    public void EditOutboxMessage(RequestInfo requestInfo,
        int messageId,
        string newMessage,
        int editDate,
        TVector<IMessageEntity>? entities,
        IMessageMedia? media,
        IReplyMarkup? replyMarkup,
        bool invertMedia,
        List<string>? hashtags,
        ReadOnlyMemory<byte>? encryptedData = null,
        ReadOnlyMemory<byte>? inboxMessageEncryptedData = null
        )
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (_state.MessageItem.Date + MyTelegramConsts.EditTimeLimit < DateTime.UtcNow.ToTimestamp())
        {
            RpcErrors.RpcErrors400.MessageEditTimeExpired.ThrowRpcError();
        }

        if (!_state.MessageItem.IsOut)
        {
            RpcErrors.RpcErrors403.MessageAuthorRequired.ThrowRpcError();
        }

        if (string.IsNullOrEmpty(newMessage))
        {
            newMessage = _state.MessageItem.Message;
        }
        var newEncryptedData = _state.MessageItem.EncryptedData;
        var newInboxMessageEncryptedData = _state.MessageItem.InboxMessageEncryptedData;
        if (encryptedData != null)
        {
            newEncryptedData = encryptedData;
            newMessage = null;
        }

        if (inboxMessageEncryptedData != null)
        {
            newInboxMessageEncryptedData = inboxMessageEncryptedData;
        }

        var oldMessageItem = _state.MessageItem;
        media ??= oldMessageItem.Media;

        var newMessageItem = _state.MessageItem with
        {
            Message = newMessage,
            Entities = entities,
            Media = media,
            ReplyMarkup = replyMarkup,
            EditDate = editDate,
            InvertMedia = invertMedia,
            Hashtags = hashtags,
            EncryptedData = newEncryptedData,
            InboxMessageEncryptedData = newInboxMessageEncryptedData
        };

        Emit(new OutboxMessageEditedEventV2(requestInfo,
            oldMessageItem,
            newMessageItem
        ));
    }

    [DoNotInheritRequestCommand]
    public void ForwardMessage(
        RequestInfo requestInfo,
        long randomId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var originalMessageItem = _state.MessageItem;
        Emit(new MessageForwardedEvent(requestInfo, randomId, originalMessageItem));
    }

    public void IncrementViews()
    {
        //Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (!IsNew)
        {
            Emit(new MessageViewsIncrementedEvent(_state.MessageItem.MessageId, _state.MessageItem.Views ?? 0 + 1));
        }
    }

    [DoNotInheritRequestCommand]
    public void PinChannelMessage(RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var channelId = _state.MessageItem.ToPeer.PeerId;
        Emit(new ChannelMessagePinnedEvent(requestInfo, channelId, _state.MessageItem.MessageId));
    }

    [DoNotInheritRequestCommand]
    public void ReadInboxHistory(RequestInfo requestInfo,
        long readerUid)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var isOut = _state.MessageItem.SenderPeer.PeerId == readerUid;
        Emit(new InboxMessageHasReadEvent(requestInfo,
            readerUid,
            _state.MessageItem.MessageId,
            _state.MessageItem.SenderPeer.PeerId,
            _state.SenderMessageId,
            _state.MessageItem.ToPeer,
            isOut
        ));
    }

    [DoNotInheritRequestCommand]
    public void ReplyToMessage(RequestInfo requestInfo, Peer replierPeer, int repliesPts, int messageId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var reply = _state.MessageItem.Reply ?? new MessageReply(null, 0, repliesPts, messageId, new List<Peer>());
        reply.Replies++;
        var recentRepliers = reply.RecentRepliers ?? new List<Peer>();
        var peer = recentRepliers.FirstOrDefault(p => p.PeerId == replierPeer.PeerId);
        if (peer != null)
        {
            recentRepliers.Remove(peer);
        }

        if (recentRepliers.Count > MyTelegramConsts.MaxRecentRepliersCount)
        {
            recentRepliers.RemoveAt(MyTelegramConsts.MaxRecentRepliersCount - 1);
        }

        recentRepliers.Insert(0, replierPeer);

        long? postChannelId = null;
        int? postMessageId = null;
        if (_state.MessageItem.FwdHeader?.ForwardFromLinkedChannel ?? false)
        {
            postChannelId = _state.MessageItem.PostChannelId;
            postMessageId = _state.MessageItem.PostMessageId;
        }

        reply.RecentRepliers = recentRepliers;
        var channelId = _state.MessageItem.ToPeer.PeerId;
        var replyToMessageId = _state.MessageItem.MessageId;
        Emit(new ReplyChannelMessageCompletedEvent(requestInfo, channelId,
            replyToMessageId, reply, postChannelId, postMessageId));
    }

    [DoNotInheritRequestCommand]
    public void UpdateInboxMessagePinned(
        RequestInfo requestInfo,
        bool pinned,
        bool pmOneSide,
        bool silent,
        int date)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var item = _state.MessageItem;
        var ownerPeerId = item.OwnerPeer.PeerId;
        Emit(new InboxMessagePinnedUpdatedEvent(
            requestInfo,
            ownerPeerId,
            item.MessageId,
            pinned,
            pmOneSide,
            silent,
            date,
            item.ToPeer,
            _state.Pts));
    }

    public void UpdateMessageRely(int pts)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var ownerChannelId = _state.MessageItem.OwnerPeer.PeerId;
        var channelId = MyTelegramConsts.DeletedChannelIdForChannelPost;
        Emit(new MessageReplyUpdatedEvent(ownerChannelId,
            channelId, _state.MessageItem.MessageId, pts));
    }

    [DoNotInheritRequestCommand]
    public void UpdateOutboxMessagePinned(
        RequestInfo requestInfo,
        bool pinned,
        bool pmOneSide,
        bool silent,
        int date)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var item = _state.MessageItem;
        var ownerPeerId = item.OwnerPeer.PeerId;
        var senderPeerId = item.SenderPeer.PeerId;
        Emit(new OutboxMessagePinnedUpdatedEvent(
            requestInfo,
            ownerPeerId,
            item.MessageId,
            pinned,
            pmOneSide,
            silent,
            date,
            _state.InboxItems,
            senderPeerId,
            _state.SenderMessageId,
            item.ToPeer,
            _state.Pts,
            _state.MessageItem.Post
        ));
    }

    protected override Task<MessageSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new MessageSnapshot(_state.MessageItem,
            _state.InboxItems,
            _state.SenderMessageId,
            _state.Pinned,
            _state.EditDate,
            //_state.EditHide,
            _state.Edited,
            _state.Pts,
            _state.IsDeleted
        ));
    }

    protected override Task LoadSnapshotAsync(MessageSnapshot snapshot,
      ISnapshotMetadata metadata,
      CancellationToken cancellationToken)
    {
        _state.LoadSnapshot(snapshot);
        return Task.CompletedTask;
    }
}