namespace MyTelegram.Domain.Commands.Dialog;

public class ReceiveInboxMessageCommand : /*Request*/Command<DialogAggregate, DialogId, IExecutionResult>, IHasRequestInfo
{
    public ReceiveInboxMessageCommand(DialogId aggregateId,
        //long reqMsgId,
        //MessageBoxId messageBoxId,
        RequestInfo requestInfo,
        int messageId,
        long ownerPeerId,
        //int pts,
        Peer toPeer) : base(aggregateId)
    {
        //MessageBoxId = messageBoxId;
        RequestInfo = requestInfo;
        MessageId = messageId;
        OwnerPeerId = ownerPeerId;
        //Pts = pts;
        ToPeer = toPeer;
    }

    //public MessageBoxId MessageBoxId { get; }
    public int MessageId { get; }
    public long OwnerPeerId { get; }

    //public int Pts { get; }
    public Peer ToPeer { get; }
    public RequestInfo RequestInfo { get; }
}