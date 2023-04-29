//using System.Collections.Concurrent;

//namespace MyTelegram.Domain.Sagas;

//public class MessageState : AggregateState<MessageSaga, MessageSagaId, MessageState>,
//        //IApply<SendMessageStartedEvent>,
//        IApply<StartCreateOutboxEvent>,
//        IApply<StartCreateInboxEvent>,
//        //IApply<SendMessagePtsIncrementEvent>,
//        IApply<SendOutboxMessageSuccessEvent>,
//        IApply<ReceiveInboxMessageSuccessEvent>,
//        IApply<SendChatMessageStartedEvent>,
//        IApply<SendChannelMessageSuccessEvent>,
//        IApply<SendChannelMessageStartedEvent>,
//        IApply<ReplyToMessageCompletedEvent>,
//        IApply<SetOutboxTopMessageEvent>,
//        IApply<SendInboxMessageOkEvent>

//    //IApply<StartSendChatMessageEvent>//,
//    //IApply<SendInboxMessageOkEvent>
//{
//    public MessageState()
//    {
//        //PtsDict = new();
//        BoxDict = new ConcurrentDictionary<long, int>();
//        BotUidList = new List<long>();
//        InboxItems = new Dictionary<long, InboxItem>();
//        ChatMemberUidList = new List<long>();
//    }

//    public IReadOnlyList<long> BotUidList { get; private set; }

//    //public ConcurrentDictionary<int, int> PtsDict { get; private set; }
//    public ConcurrentDictionary<long, int> BoxDict { get; private set; }
//    public IReadOnlyList<long> ChatMemberUidList { get; private set; }
//    public long DeletedChatUserId { get; private set; }
//    public int InboxCount { get; private set; }
//    public Dictionary<long, InboxItem> InboxItems { get; private set; }

//    public Peer InboxToPeer { get; private set; } = null!;
//    public long LinkedChannelId { get; private set; }
//    public bool NeedWaitForReplyToMsgId { get; private set; }
//    public bool NeedWaitForStartChannelMessage { get; private set; }
//    public OutboxCreatedEvent Outbox { get; private set; } = null!;
//    public bool Post { get; private set; }
//    public bool ReceivedReplyToMsgId { get; private set; }
//    public bool ReceivedStartChannelMessage { get; private set; }
//    public int ReceiverCount { get; private set; }
//    public string Title { get; private set; } = null!;
//    public int? Views { get; private set; }

//    public void Apply(ReceiveInboxMessageSuccessEvent aggregateEvent)
//    {
//        RemoveCompletedBox(aggregateEvent.OwnerPeerId);
//        //throw new NotImplementedException();
//    }

//    public void Apply(ReplyToMessageCompletedEvent aggregateEvent)
//    {
//        foreach (var inboxItem in aggregateEvent.InboxItems)
//            if (!InboxItems.ContainsKey(inboxItem.InboxOwnerPeerId))
//            {
//                InboxItems.TryAdd(inboxItem.InboxOwnerPeerId, inboxItem);
//            }

//        ReceivedReplyToMsgId = true;
//    }

//    public void Apply(SendChannelMessageStartedEvent aggregateEvent)
//    {
//        ReceivedStartChannelMessage = true;
//        Views = aggregateEvent.Views;
//        Post = aggregateEvent.Post;
//        BotUidList = aggregateEvent.BotUidList;
//        LinkedChannelId = aggregateEvent.LinkedChannelId;
//    }

//    public void Apply(SendChannelMessageSuccessEvent aggregateEvent)
//    {
//        //throw new NotImplementedException();
//    }

//    //public void Apply(SendMessagePtsIncrementEvent aggregateEvent)
//    //{
//    //    PtsDict.TryAdd(aggregateEvent.PeerId, aggregateEvent.Pts);
//    //    //if (aggregateEvent.Reason == PtsChangeReason.OutboxCreated)
//    //    //{
//    //    //    OutboxPts = aggregateEvent.Pts;
//    //    //}
//    //    //else if (aggregateEvent.Reason == PtsChangeReason.InboxCreated)
//    //    //{
//    //    //    //InboxPts = aggregateEvent.Pts;
//    //    //    _ptsDict.TryAdd(aggregateEvent.PeerId, aggregateEvent.Pts);
//    //    //}
//    //}
//    public void Apply(SendChatMessageStartedEvent aggregateEvent)
//    {
//        ReceiverCount = aggregateEvent.ChatMemberUidList.Count - 1;
//        ChatMemberUidList = aggregateEvent.ChatMemberUidList;
//        Title = aggregateEvent.Title;
//        DeletedChatUserId = aggregateEvent.LatestDeletedUserId;
//        if (DeletedChatUserId > 0)
//        {
//            ReceiverCount++;
//        }
//        //if (Outbox.SubType == MessageBoxSubType.DeleteChatUser)
//        //{
//        //    //DeletedChatUserId=Outbox.MessageActionData.to
//        //}
//    }

//    public void Apply(SendInboxMessageOkEvent aggregateEvent)
//    {
//        //throw new NotImplementedException();
//    }

//    //public void Apply(SendMessageSuccessEvent aggregateEvent)
//    //{
//    //    //throw new NotImplementedException();
//    //    _sw.Stop();
//    //    //Console.WriteLine($"send message ok.{_sw.Elapsed}");
//    //}

//    public void Apply(SendOutboxMessageSuccessEvent aggregateEvent)
//    {
//        if (Outbox.ToPeerType == PeerType.Channel)
//        {
//            InboxCount++;
//        }

//        RemoveCompletedBox(aggregateEvent.OwnerPeerId);
//        //throw new NotImplementedException();
//    }

//    public void Apply(SetOutboxTopMessageEvent aggregateEvent)
//    {
//        //throw new NotImplementedException();
//    }

//    public void Apply(StartCreateInboxEvent aggregateEvent)
//    {
//        //InboxMessageId = aggregateEvent.Inbox.MessageId;
//        //InboxPts = aggregateEvent.Inbox.Pts;
//        //InboxToPeer = new Peer(aggregateEvent.Inbox.ToPeerType, aggregateEvent.Inbox.ToPeerId);
//        //InboxOwnerPeerId = aggregateEvent.Inbox.OwnerPeerId;
//        //InboxOwnerPeer = new Peer(aggregateEvent.Inbox.ToPeerType, aggregateEvent.Inbox.OwnerPeerId);
//        //_inboxCount++;
//        InboxCount++;

//        BoxDict.TryAdd(aggregateEvent.OwnerPeerId, aggregateEvent.MessageId);
//    }

//    public void Apply(StartCreateOutboxEvent aggregateEvent)
//    {
//        ////throw new NotImplementedException();
//        //OutboxOwnerPeerId = aggregateEvent.Outbox.OwnerPeerId;
//        //Message = aggregateEvent.Outbox.MessageData?.Message;
//        //OutboxMessageId = aggregateEvent.Outbox.MessageId;
//        ////OutboxPts = aggregateEvent.Outbox.Pts;
//        //Date = aggregateEvent.Outbox.Date;
//        //ReqMsgId = aggregateEvent.Outbox.ReqMsgId;
//        //OutboxReplyToMsgId = aggregateEvent.Outbox.ReplyToMsgId;
//        //OutboxToPeer = new Peer(aggregateEvent.Outbox.ToPeerType, aggregateEvent.Outbox.ToPeerId);
//        //SenderIsBot = aggregateEvent.Outbox.OwnerIsBot;
//        //ReceiverOwnerIsBot = aggregateEvent.Outbox.ReceiverOwnerIsBot;
//        //RandomId = aggregateEvent.Outbox.RandomId;
//        if (aggregateEvent.Outbox.ToPeerType == PeerType.User || aggregateEvent.Outbox.ToPeerType == PeerType.Channel)
//        {
//            ReceiverCount = 1;
//        }

//        if (aggregateEvent.Outbox.ToPeerType == PeerType.Channel)
//        {
//            NeedWaitForStartChannelMessage = true;
//        }

//        Views = aggregateEvent.Outbox.Views;
//        InboxToPeer = GetInboxPeer(aggregateEvent.Outbox);

//        Outbox = aggregateEvent.Outbox;
//        NeedWaitForReplyToMsgId = aggregateEvent.NeedWaitForReplyToMsgId;

//        BoxDict.TryAdd(aggregateEvent.Outbox.OwnerPeerId, aggregateEvent.Outbox.MessageId);
//    }

//    private static Peer GetInboxPeer(OutboxCreatedEvent aggregateEvent)
//    {
//        if (aggregateEvent.ToPeerType == PeerType.User)
//        {
//            return new Peer(PeerType.User, aggregateEvent.SenderPeerId);
//        }

//        return new Peer(aggregateEvent.ToPeerType, aggregateEvent.ToPeerId);
//    }

//    //public int GetPts(long inboxOwnerPeerId)
//    //{
//    //    PtsDict.TryGetValue(inboxOwnerPeerId, out var pts);
//    //    return pts;
//    //}

//    public int GetMessageId(long inboxOwnerPeerId)
//    {
//        BoxDict.TryGetValue(inboxOwnerPeerId, out var messageId);
//        return messageId;
//    }

//    public int? GetReplyToMsgId(long inboxOwnerPeerId)
//    {
//        if (Outbox.ToPeerType == PeerType.Channel)
//        {
//            return Outbox.ReplyToMsgId;
//        }

//        //// should get reply to msg id by outbox.OwnerPeerId
//        //if (ReceivedReplyToMsgId)
//        //{
//        //    return Outbox.ReplyToMsgId;
//        //    //if (InboxItems.TryGetValue(Outbox.OwnerPeerId, out var itemForOutbox))
//        //    //{
//        //    //    return itemForOutbox.InboxMessageId;
//        //    //}

//        //    //return 0;
//        //}

//        if (InboxItems.TryGetValue(inboxOwnerPeerId, out var item))
//        {
//            return item.InboxMessageId;
//        }

//        return null;
//    }

//    public bool IsBoxOk(long ownerPeerId)
//    {
//        if ( /*PtsDict.ContainsKey(ownerPeerId) &&*/ BoxDict.ContainsKey(ownerPeerId))
//        {
//            return true;
//        }

//        return false;
//    }

//    public bool IsOutboxOk(long ownerPeerId)
//    {
//        if (Outbox.ToPeerType == PeerType.Channel)
//        {
//            if (ReceivedStartChannelMessage && IsBoxOk(ownerPeerId))
//            {
//                return true;
//            }

//            return false;
//        }

//        return IsBoxOk(ownerPeerId);
//    }

//    public bool IsSendCompleted()
//    {
//        if (Outbox != null! && ReceiverCount != 0 && ReceiverCount == InboxCount)
//        {
//            if (NeedWaitForStartChannelMessage)
//            {
//                if (ReceivedStartChannelMessage)
//                {
//                    return true;
//                }
//            }
//            else
//            {
//                return true;
//            }
//        }

//        return false;
//    }

//    public void LoadSnapshot(MessageSagaSnapshot snapshot)
//    {
//        InboxToPeer = snapshot.InboxToPeer;
//        ReceiverCount = snapshot.ReceiverCount;
//        InboxCount = snapshot.InboxCount;
//        Views = snapshot.Views;
//        Post = snapshot.Post;
//        //PtsDict = snapshot.PtsDict;
//        BoxDict = snapshot.BoxDict;
//        Outbox = snapshot.Outbox;
//        Title = snapshot.Tile;
//        DeletedChatUserId = snapshot.DeletedChatUserId;
//        NeedWaitForReplyToMsgId = snapshot.NeedWaitForReplyToMsgId;
//        NeedWaitForStartChannelMessage = snapshot.NeedWaitForStartChannelMessage;
//        ReceivedStartChannelMessage = snapshot.ReceivedStartChannelMessage;
//        BotUidList = snapshot.BotUidList;
//        InboxItems = snapshot.InboxItems;
//        ChatMemberUidList = snapshot.ChatMemberUidList;
//        ReceivedReplyToMsgId = snapshot.ReceivedReplyToMsgId;
//        LinkedChannelId = snapshot.LinkedChannelId;
//    }

//    public void RemoveCompletedBox(long ownerPeerId)
//    {
//        BoxDict.TryRemove(ownerPeerId, out _);
//        //PtsDict.TryRemove(ownerPeerId, out _);
//    }
//}


