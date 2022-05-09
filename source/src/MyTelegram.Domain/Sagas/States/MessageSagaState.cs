namespace MyTelegram.Domain.Sagas.States;

public class MessageSagaState : AggregateState<MessageSaga, MessageSagaId, MessageSagaState>,
    IApply<MessageSagaStartedEvent>,
    IApply<SendChatMessageStartedEvent>,
    IApply<ReplyToMessageCompletedEvent>,
    IApply<SendChannelMessageStartedEvent>,
    IApply<SendOutboxMessageCompletedEvent>,
    IApply<ReceiveInboxMessageCompletedEvent>,
    IApply<OutboxMessageIdGeneratedEvent>
{
    public RequestInfo Request { get; private set; } = null!;
    public bool ClearDraft { get; private set; }
    public int GroupItemCount { get; private set; }
    public Guid CorrelationId { get; private set; }
    public MessageItem? MessageItem { get; private set; }
    public string? ChatTitle { get; private set; }
    public IReadOnlyList<long>? ChatMemberUidList { get; private set; }
    public Dictionary<long, int> InboxItems { get; private set; } = new();
    public int SenderMessageId { get; private set; }
    public IReadOnlyList<long>? BotUidList { get; private set; }
    public long? LinkedChannelId { get; private set; }
    //public bool Post { get; private set; }
    //public int? Views { get; private set; }

    public int InboxCount { get; private set; }

    public bool IsSendMessageCompleted()
    {
        if (MessageItem != null)
        {
            switch (MessageItem.ToPeer.PeerType)
            {
                case PeerType.User:
                    return InboxCount == 1;
                case PeerType.Chat:
                    return InboxCount == ChatMemberUidList!.Count;
            }
        }

        return false;
    }

    public void Apply(MessageSagaStartedEvent aggregateEvent)
    {
        Request = aggregateEvent.Request;
        MessageItem = aggregateEvent.MessageItem;
        ClearDraft = aggregateEvent.ClearDraft;
        GroupItemCount = aggregateEvent.GroupItemCount;
        CorrelationId = aggregateEvent.CorrelationId;
        SenderMessageId = aggregateEvent.MessageItem.MessageId;
    }

    public void Apply(SendChatMessageStartedEvent aggregateEvent)
    {
        ChatTitle = aggregateEvent.Title;
        ChatMemberUidList = aggregateEvent.ChatMemberUidList;
    }

    public void Apply(ReplyToMessageCompletedEvent aggregateEvent)
    {
        foreach (var inboxItem in aggregateEvent.InboxItems)
        {
            InboxItems.TryAdd(inboxItem.InboxOwnerPeerId, inboxItem.InboxMessageId);
        }
    }

    public int? GetReplyToMsgId(long inboxOwnerPeerId)
    {
        if (InboxItems.TryGetValue(inboxOwnerPeerId, out var messageId))
        {
            return messageId;
        }

        return null;
    }

    public void LoadSnapshot(MessageSnapshot snapshot)
    {
        Request = snapshot.Request;
        MessageItem = snapshot.MessageItem;
        ClearDraft = snapshot.ClearDraft;
        GroupItemCount = snapshot.GroupItemCount;
        SenderMessageId = snapshot.SenderMessageId;
        CorrelationId = snapshot.CorrelationId;
        ChatTitle = snapshot.ChatTitle;
        ChatMemberUidList = snapshot.ChatMemberUidList;
        InboxItems = snapshot.InboxItems;

        BotUidList = snapshot.BotUidList;
        LinkedChannelId = snapshot.LinkedChannelId;
    }

    public void Apply(SendChannelMessageStartedEvent aggregateEvent)
    {
        BotUidList = aggregateEvent.BotUidList;
        LinkedChannelId = aggregateEvent.LinkedChannelId;
        MessageItem!.Post = aggregateEvent.Post;
        MessageItem.Views = aggregateEvent.Views;
    }

    public void Apply(SendOutboxMessageCompletedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }

    public void Apply(ReceiveInboxMessageCompletedEvent aggregateEvent)
    {
        InboxCount++;
    }

    public void Apply(OutboxMessageIdGeneratedEvent aggregateEvent)
    {
        MessageItem!.MessageId = aggregateEvent.OutboxMessageId;
    }
}