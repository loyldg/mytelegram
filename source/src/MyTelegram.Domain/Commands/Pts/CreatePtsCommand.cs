//namespace MyTelegram.Domain.Commands.Pts;

//public class CreatePtsCommand : DistinctCommand<PtsAggregate, PtsId, IExecutionResult>, IHasCorrelationId
//{
//    public CreatePtsCommand(PtsId aggregateId,
//        long peerId,
//        int pts,
//        int qts,
//        int unreadCount,
//        int date,
//        Guid correlationId) : base(aggregateId)
//    {
//        PeerId = peerId;
//        Pts = pts;
//        Qts = qts;
//        UnreadCount = unreadCount;
//        Date = date;
//        CorrelationId = correlationId;
//    }

//    public int Date { get; }
//    public long PeerId { get; }
//    public int Pts { get; }
//    public int Qts { get; }
//    public int UnreadCount { get; }

//    public Guid CorrelationId { get; }

//    protected override IEnumerable<byte[]> GetSourceIdComponents()
//    {
//        yield return BitConverter.GetBytes(PeerId);
//    }
//}


