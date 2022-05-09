// namespace MyTelegram.Domain.Events.Message;

// public class InboxMessageHasReadEvent : AggregateEvent<MessageBoxAggregate, MessageBoxId>, IHasCorrelationId
// {
//     public InboxMessageHasReadEvent(
//         long reqMsgId,
//         long readerUid,
//         int maxMessageId,
//         int senderMessageId,
//         long senderPeerId,
//         PeerType toPeerType,
//         long toPeerId,
//         bool isOut,
//         bool senderIsBot,
//         string sourceCommandId,
//         Guid correlationId
//     )
//     {
//         ReqMsgId = reqMsgId;
//         ReaderUid = readerUid;
//         MaxMessageId = maxMessageId;
//         SenderMessageId = senderMessageId;
//         SenderPeerId = senderPeerId;
//         ToPeerType = toPeerType;
//         ToPeerId = toPeerId;
//         IsOut = isOut;
//         SenderIsBot = senderIsBot;
//         SourceCommandId = sourceCommandId;
//         CorrelationId = correlationId;
//     }

//     public bool IsOut { get; }
//     public int MaxMessageId { get; }
//     public long ReaderUid { get; }

//     public long ReqMsgId { get; }
//     public bool SenderIsBot { get; }
//     public int SenderMessageId { get; }
//     public long SenderPeerId { get; }
//     public string SourceCommandId { get; }
//     public long ToPeerId { get; }
//     public PeerType ToPeerType { get; }

//     public Guid CorrelationId { get; }
// }
