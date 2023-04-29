namespace MyTelegram.Domain.Commands.Messaging;

public class CreateOutboxMessageCommand : RequestCommand<MessageAggregate, MessageId, IExecutionResult>
{
    public CreateOutboxMessageCommand(MessageId aggregateId,
        long reqMsgId,
        MessageItem outboxMessageItem,
        bool clearDraft,
        int groupItemCount,
        long? linkedChannelId,
        Guid correlationId
    ) : base(aggregateId, reqMsgId)
    {
        OutboxMessageItem = outboxMessageItem;
        ClearDraft = clearDraft;
        GroupItemCount = groupItemCount;
        LinkedChannelId = linkedChannelId;
        CorrelationId = correlationId;
    }

    //public long ReqMsgId { get; }
    public MessageItem OutboxMessageItem { get; }
    public bool ClearDraft { get; }
    public int GroupItemCount { get; }
    public long? LinkedChannelId { get; }
    public Guid CorrelationId { get; }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(ReqMsgId);
        yield return BitConverter.GetBytes(OutboxMessageItem.RandomId);
    }
}
