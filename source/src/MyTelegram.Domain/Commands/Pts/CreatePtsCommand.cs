namespace MyTelegram.Domain.Commands.Pts;

public class CreatePtsCommand : RequestCommand2<PtsAggregate, PtsId, IExecutionResult>
{
    public CreatePtsCommand(PtsId aggregateId,
        RequestInfo requestInfo,
        long peerId,
        int pts,
        int qts,
        int unreadCount,
        int date) : base(aggregateId, requestInfo)
    {
        PeerId = peerId;
        Pts = pts;
        Qts = qts;
        UnreadCount = unreadCount;
        Date = date;
    }

    public int Date { get; }
    public long PeerId { get; }
    public int Pts { get; }
    public int Qts { get; }
    public int UnreadCount { get; }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(PeerId);
    }
}
