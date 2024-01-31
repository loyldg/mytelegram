namespace MyTelegram.Domain.Sagas.States;

public class ReadHistoryState : AggregateState<ReadHistorySaga, ReadHistorySagaId, ReadHistoryState>,
    //IApply<ReadInboxMessage2Event>,
    IApply<ReadHistoryStartedEvent>,
    IApply<ReadHistoryInboxHasReadEvent>,
    IApply<ReadHistoryOutboxHasReadEvent>,
    IApply<ReadHistoryPtsIncrementEvent>,
    //IApply<ReadHistoryCompletedEvent>,
    IApply<ReadHistoryReadLatestNoneBotOutboxMessageEvent>
{
    public RequestInfo RequestInfo { get; private set; } = default!;

    public Guid CorrelationId { get; private set; }
    public bool InboxPtsIncremented { get; private set; }
    public bool IsOut { get; private set; }
    public bool LatestNoneBotOutboxHasRead { get; private set; }

    public bool NeedReadLatestNoneBotOutboxMessage { get; private set; }
    //private bool _outboxPtsIncremented;
    //private bool _inboxPtsIncremented;
    //private bool _latestNoneBotOutboxHasRead;

    public bool OutboxPtsIncremented { get; private set; }
    public int ReaderMessageId { get; private set; }
    public int ReaderPts { get; private set; }
    public Peer ReaderToPeer { get; private set; } = default!;
    public long ReaderUserId { get; private set; }

    //private bool _outboxHasRead;
    //internal bool ReadHistoryCompleted => Out || _outboxHasRead;

    public bool ReadHistoryCompleted { get; private set; }

    public bool SenderIsBot { get; private set; }

    public int SenderMessageId { get; private set; }
    public int SenderPts { get; private set; }

    public long SenderPeerId { get; private set; }
    public string SourceCommandId { get; private set; } = null!;

    public void Apply(ReadHistoryInboxHasReadEvent aggregateEvent)
    {
        //// 如果读取的是自己发送的消息或者发送方是机器人，读取历史消息流程直接结束
        //if (aggregateEvent.SenderIsBot || aggregateEvent.IsOut)
        //{
        //    ReadHistoryCompleted = true;
        //}

        IsOut = aggregateEvent.IsOut;
        SenderIsBot = aggregateEvent.SenderIsBot;
        NeedReadLatestNoneBotOutboxMessage = aggregateEvent.NeedReadLatestNoneBotOutboxMessage;
        SetReadHistoryCompleted();
    }

    public void Apply(ReadHistoryOutboxHasReadEvent aggregateEvent)
    {
        SenderPeerId = aggregateEvent.SenderPeerId;
        SenderMessageId = aggregateEvent.SenderMessageId;

        SetReadHistoryCompleted();
    }

    //public void Apply(StartReadHistoryEvent aggregateEvent)
    //{
    //    ReaderUid = aggregateEvent.ReaderUid;
    //    ReaderToPeerType = aggregateEvent.ToPeerType;
    //    ReaderToPeerId = aggregateEvent.ToPeerId;
    //    //ReqMsgId=aggregateEvent
    //}

    public void Apply(ReadHistoryPtsIncrementEvent aggregateEvent)
    {
        if (aggregateEvent.Reason == PtsChangeReason.ReadInboxMessage)
        {
            ReaderPts = aggregateEvent.Pts;
            //_inboxPtsIncremented = true;
            InboxPtsIncremented = true;
        }
        else if (aggregateEvent.Reason == PtsChangeReason.OutboxMessageHasRead)
        {
            SenderPts = aggregateEvent.Pts;
            //_outboxPtsIncremented = true;
            OutboxPtsIncremented = true;
        }

        SetReadHistoryCompleted();
    }

    //public void Apply(ReadHistoryCompletedEvent aggregateEvent)
    //{
    //    //throw new NotImplementedException();
    //}

    public void Apply(ReadHistoryReadLatestNoneBotOutboxMessageEvent aggregateEvent)
    {
        SenderPeerId = aggregateEvent.SenderPeerId;
        //NeedReadLatestNoneBotOutboxMessage = true;
        SetLatestNoneBotOutboxAsRead();
    }

    public void Apply(ReadHistoryStartedEvent aggregateEvent)
    {
        RequestInfo = aggregateEvent.RequestInfo;
        ReaderUserId = aggregateEvent.ReaderUid;
        ReaderMessageId = aggregateEvent.ReaderMessageId;
        ReaderToPeer = aggregateEvent.ToPeer;
        SourceCommandId = aggregateEvent.SourceCommandId;
    }

    public void LoadSnapshot(ReadHistorySagaSnapshot snapshot)
    {
        RequestInfo = snapshot.RequestInfo;
        ReaderUserId = snapshot.ReaderUid;
        ReaderMessageId = snapshot.ReaderMessageId;
        ReaderPts = snapshot.ReaderPts;
        SenderIsBot = snapshot.SenderIsBot;
        SenderPeerId = snapshot.SenderPeerId;
        SenderPts = snapshot.SenderPts;
        SenderMessageId = snapshot.SenderMessageId;
        ReaderToPeer = snapshot.ReaderToPeer;
        IsOut = snapshot.IsOut;
        ReadHistoryCompleted = snapshot.ReadHistoryCompleted;
        OutboxPtsIncremented = snapshot.OutboxPtsIncremented;
        InboxPtsIncremented = snapshot.InboxPtsIncremented;
        LatestNoneBotOutboxHasRead = snapshot.LatestNoneBotOutboxHasRead;
        NeedReadLatestNoneBotOutboxMessage = snapshot.NeedReadLatestNoneBotOutboxMessage;
        SourceCommandId = snapshot.SourceCommandId;
        CorrelationId = snapshot.CorrelationId;
    }

    public void SetLatestNoneBotOutboxAsRead()
    {
        SenderIsBot = false;
        //_latestNoneBotOutboxHasRead = true;
        LatestNoneBotOutboxHasRead = true;
        SetReadHistoryCompleted();
    }

    private void SetReadHistoryCompleted()
    {
        //if (ReadHistoryCompleted)
        //{
        //    return;
        //}

        if (InboxPtsIncremented)
        {
            if (IsOut)
            {
                ReadHistoryCompleted = true;
            }
            else
            {
                if (OutboxPtsIncremented)
                {
                    if (ReaderToPeer.PeerType == PeerType.Chat)
                    {
                        if (NeedReadLatestNoneBotOutboxMessage && LatestNoneBotOutboxHasRead)
                        {
                            ReadHistoryCompleted = true;
                        }
                        else
                        {
                            ReadHistoryCompleted = true;
                        }
                    }
                    else
                    {
                        ReadHistoryCompleted = true;
                    }
                }

                //if (ReaderToPeerType == PeerType.Chat)
                //{
                //    if (NeedReadLatestNoneBotOutboxMessage)
                //    {

                //    }
                //    else
                //    {

                //    }

                //    if (LatestNoneBotOutboxHasRead && OutboxPtsIncremented)
                //    {
                //        ReadHistoryCompleted = true;
                //    }
                //}
                //else
                //{
                //    if (OutboxPtsIncremented)
                //    {
                //        ReadHistoryCompleted = true;
                //    }
                //}
            }
        }
    }
}
