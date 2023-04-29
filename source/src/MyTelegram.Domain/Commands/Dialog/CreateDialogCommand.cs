namespace MyTelegram.Domain.Commands.Dialog;

public class CreateDialogCommand : Command<DialogAggregate, DialogId, IExecutionResult>
{
    public CreateDialogCommand(DialogId aggregateId,
        long ownerId,
        Peer toPeer,
        int channelHistoryMinId,
        int topMessageId,
        //string topMessageBoxId,
        Guid correlationId) : base(aggregateId)
    {
        OwnerId = ownerId;
        ToPeer = toPeer;
        ChannelHistoryMinId = channelHistoryMinId;
        TopMessageId = topMessageId;
        //TopMessageBoxId = topMessageBoxId;
        CorrelationId = correlationId;
    }

    public int ChannelHistoryMinId { get; }

    //public string TopMessageBoxId { get; }
    public Guid CorrelationId { get; }

    public long OwnerId { get; }
    public Peer ToPeer { get; }
    public int TopMessageId { get; }
}
