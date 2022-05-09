namespace MyTelegram.Domain.Commands.Dialog;

public class ReadChannelInboxMessageCommand : RequestCommand<DialogAggregate, DialogId, IExecutionResult>,
    IHasCorrelationId
{
    public ReadChannelInboxMessageCommand(DialogId aggregateId,
        long reqMsgId,
        long readerUid,
        long channelId,
        int maxId,
        //string messageBoxId,
        Guid correlationId) : base(aggregateId, reqMsgId)
    {
        ReaderUid = readerUid;
        ChannelId = channelId;
        MaxId = maxId;
        //MessageBoxId = messageBoxId;
        CorrelationId = correlationId;
    }

    public long ChannelId { get; }
    public int MaxId { get; }

    public long ReaderUid { get; }

    //public string MessageBoxId { get; }
    public Guid CorrelationId { get; }
}
