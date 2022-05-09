namespace MyTelegram.Domain.Commands.Dialog;

public class MarkDialogAsUnreadCommand : RequestCommand<DialogAggregate, DialogId, IExecutionResult>
{
    public MarkDialogAsUnreadCommand(DialogId aggregateId,
        long reqMsgId,
        bool unread
    ) : base(aggregateId, reqMsgId)
    {
        Unread = unread;
    }

    public bool Unread { get; }
}

//public class ReadInboxMessageCommand : RequestCommand<DialogAggregate, DialogId, IExecutionResult>,IHasCorrelationId
//{
//    public long OwnerPeerId { get; }
//    public int MaxMessageId { get; }
//    public bool Out { get; }
//    public int SenderMessageId { get; }
//    public long SenderPeerId { get; }
//    public PeerType ToPeerType { get; }
//    public long ToPeerId { get; }

//    public int ReaderPts { get; }
//    public int SenderPts { get; }
//    public bool SenderIsBot { get; }
//    public Guid CorrelationId { get; }

//    public ReadInboxMessageCommand(DialogId aggregateId,
//        long reqMsgId,
//        long ownerPeerId,
//        int maxMessageId,
//        bool @out,
//        int senderMessageId,
//        long senderPeerId,
//        PeerType toPeerType,
//        long toPeerId,
//        int readerPts,
//        int senderPts,
//        bool senderIsBot,
//        Guid correlationId
//        ) : base(aggregateId,reqMsgId)
//    {
//        MaxMessageId = maxMessageId;
//        Out = @out;
//        SenderMessageId = senderMessageId;
//        SenderPeerId = senderPeerId;
//        ToPeerType = toPeerType;
//        ToPeerId = toPeerId;
//        SenderPts = senderPts;
//        SenderIsBot = senderIsBot;
//        ReaderPts = readerPts;
//        CorrelationId = correlationId;
//        OwnerPeerId = ownerPeerId;
//    }

//}
