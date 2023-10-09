namespace MyTelegram.Domain.Commands.Dialog;

public class CreateDialogCommand : RequestCommand2<DialogAggregate, DialogId, IExecutionResult>
{
    public CreateDialogCommand(DialogId aggregateId,
        RequestInfo requestInfo,
        long ownerId,
        Peer toPeer,
        int channelHistoryMinId,
        int topMessageId) : base(aggregateId, requestInfo)
    {
        OwnerId = ownerId;
        ToPeer = toPeer;
        ChannelHistoryMinId = channelHistoryMinId;
        TopMessageId = topMessageId;
        //TopMessageBoxId = topMessageBoxId; 
    }

    public int ChannelHistoryMinId { get; }

    //public string TopMessageBoxId { get; } 

    public long OwnerId { get; }
    public Peer ToPeer { get; }
    public int TopMessageId { get; }
}