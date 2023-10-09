namespace MyTelegram.Domain.Commands.Dialog;

public class ReadChannelInboxMessageCommand : RequestCommand2<DialogAggregate, DialogId, IExecutionResult>
{
    public ReadChannelInboxMessageCommand(DialogId aggregateId,
        RequestInfo requestInfo,
        long readerUserId,
        long channelId,
        int maxId,
        long senderUserId,
        int? topMsgId) : base(aggregateId, requestInfo)
    {
        ReaderUserId = readerUserId;
        ChannelId = channelId;
        MaxId = maxId;
        SenderUserId = senderUserId;
        TopMsgId = topMsgId;
    }

    public long ChannelId { get; }
    public int MaxId { get; }
    public long SenderUserId { get; }
    public int? TopMsgId { get; }

    public long ReaderUserId { get; }
}