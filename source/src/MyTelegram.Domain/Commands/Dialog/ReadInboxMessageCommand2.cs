namespace MyTelegram.Domain.Commands.Dialog;

public class ReadInboxMessageCommand2 : RequestCommand2<DialogAggregate, DialogId, IExecutionResult>
{
    public ReadInboxMessageCommand2(DialogId aggregateId,
        RequestInfo requestInfo,
        long readerUid,
        long ownerPeerId,
        int maxMessageId,
        int unreadCount,
        Peer toPeer,int date) : base(aggregateId, requestInfo)
    {
        ReaderUid = readerUid;
        OwnerPeerId = ownerPeerId;
        MaxMessageId = maxMessageId;
        UnreadCount = unreadCount;
        ToPeer = toPeer;
        Date = date;
    }

    public int MaxMessageId { get; }
    public int UnreadCount { get; }
    public long OwnerPeerId { get; }
    public long ReaderUid { get; }

    public Peer ToPeer { get; }
    public int Date { get; }

    //protected override IEnumerable<byte[]> GetSourceIdComponents()
    //{
    //    yield return BitConverter.GetBytes(ReaderUid);
    //    yield return BitConverter.GetBytes(ToPeer.PeerId);
    //    yield return BitConverter.GetBytes(MaxMessageId);
    //}
}