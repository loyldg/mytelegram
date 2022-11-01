namespace MyTelegram.Domain.Commands.Dialog;

public class ReadInboxMessageCommand2 : RequestCommand2<DialogAggregate, DialogId, IExecutionResult>, IHasCorrelationId
{
    public ReadInboxMessageCommand2(DialogId aggregateId,
        RequestInfo requestInfo,
        long readerUid,
        long ownerPeerId,
        int maxMessageId,
        Peer toPeer,
        Guid correlationId) : base(aggregateId, requestInfo)
    {
        ReaderUid = readerUid;
        OwnerPeerId = ownerPeerId;
        MaxMessageId = maxMessageId;
        ToPeer = toPeer;
        CorrelationId = correlationId;
    }

    public int MaxMessageId { get; }
    public long OwnerPeerId { get; }
    public long ReaderUid { get; }

    public Peer ToPeer { get; }
    public Guid CorrelationId { get; }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(ReaderUid);
        yield return BitConverter.GetBytes(ToPeer.PeerId);
        yield return BitConverter.GetBytes(MaxMessageId);
        // Console.WriteLine($"GetSourceIdComponents:ReaderUid {ReaderUid} ToPeerId{ToPeerId} MaxMessageId {MaxMessageId}");
    }
}
