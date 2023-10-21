namespace MyTelegram.Domain.Sagas.States;

public class SendMessageSagaState : AggregateState<SendMessageSaga, SendMessageSagaId, SendMessageSagaState>,
    IApply<SendMessageSagaStartedEvent>,
    IApply<SendOutboxMessageCompletedEvent2>,
    IApply<ReceiveInboxMessageCompletedEvent2>
{
    public RequestInfo RequestInfo { get; set; } = default!;
    public MessageItem MessageItem { get; set; } = default!;
    public List<long>? MentionedUserIds { get; private set; }
    public int GroupItemCount { get; set; }
    public long? LinkedChannelId { get; set; }
    //public List<long>? BotUserIds { get; set; }
    public long ReplyToMessageSavedFromPeerId { get; private set; }
    //public int ReplyToMsgId { get; set; }
    //public bool ForwardFromLinkedChannel { get; set; }
    //public int Pts { get; private set; }

    //public List<ReplyToMsgItem>? ReplyToMsgItems { get; private set; }
    public List<long>? ChatMembers { get; private set; } = new();
    public List<InboxItem> InboxItems { get; private set; } = new();

    public Dictionary<long, int> ReplyToMsgItems { get; private set; } = new();

    public void Apply(SendMessageSagaStartedEvent aggregateEvent)
    {
        RequestInfo = aggregateEvent.RequestInfo;
        MessageItem = aggregateEvent.MessageItem;
        MentionedUserIds = aggregateEvent.MentionedUserIds;
        GroupItemCount = aggregateEvent.GroupItemCount;
        LinkedChannelId = aggregateEvent.LinkedChannelId;
        ChatMembers = aggregateEvent.ChatMembers;
        //ReplyToMsgItems=aggregateEvent.ReplyToMsgItems;

        if (aggregateEvent.ReplyToMsgItems?.Count > 0)
        {
            ReplyToMsgItems = aggregateEvent.ReplyToMsgItems.ToDictionary(k => k.UserId, v => v.MessageId);
        }
    }

    public void Apply(SendOutboxMessageCompletedEvent2 aggregateEvent)
    {
        //throw new NotImplementedException();
    }

    public void Apply(ReceiveInboxMessageCompletedEvent2 aggregateEvent)
    {
        //throw new NotImplementedException();
        InboxItems.Add(new(aggregateEvent.MessageItem.OwnerPeer.PeerId, aggregateEvent.MessageItem.MessageId));
    }

    public bool IsCreateInboxMessagesCompleted()
    {
        switch (MessageItem.ToPeer.PeerType)
        {
            case PeerType.User:
                return InboxItems.Count == 1;
            case PeerType.Chat:
                return InboxItems.Count == ChatMembers?.Count - 1;
        }

        return false;
    }
}