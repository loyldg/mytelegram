// namespace MyTelegram.Domain.Events.Message;

// public class ForwardMessageStartedEvent : AggregateEvent<MessageBoxAggregate, MessageBoxId>, IHasCorrelationId
// {
//     public ForwardMessageStartedEvent(
//         long reqMsgId,
//         long selfAuthKeyId,
//         long selfPermAuthKeyId,
//         long selfUserId,
//         Peer fromPeer,
//         Peer toPeer,
//         IReadOnlyList<int> idList,
//         IReadOnlyList<long> randomIdList,
//         Guid correlationId)
//     {
//         ReqMsgId = reqMsgId;
//         SelfAuthKeyId = selfAuthKeyId;
//         SelfPermAuthKeyId = selfPermAuthKeyId;
//         SelfUserId = selfUserId;
//         FromPeer = fromPeer;
//         ToPeer = toPeer;
//         IdList = idList;
//         RandomIdList = randomIdList;
//         CorrelationId = correlationId;
//     }

//     public Peer FromPeer { get; }
//     public IReadOnlyList<int> IdList { get; }
//     public IReadOnlyList<long> RandomIdList { get; }

//     public long ReqMsgId { get; }
//     public long SelfAuthKeyId { get; }
//     public long SelfPermAuthKeyId { get; }
//     public long SelfUserId { get; }
//     public Peer ToPeer { get; }
//     public Guid CorrelationId { get; }
// }
