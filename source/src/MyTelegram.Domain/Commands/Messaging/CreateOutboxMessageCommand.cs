namespace MyTelegram.Domain.Commands.Messaging;

public class CreateOutboxMessageCommand : RequestCommand<MessageAggregate, MessageId, IExecutionResult>
{
    //public long ReqMsgId { get; }
    public MessageItem OutboxMessageItem { get; }
    public bool ClearDraft { get; }
    public int GroupItemCount { get; }
    public Guid CorrelationId { get; }

    public CreateOutboxMessageCommand(MessageId aggregateId,
        long reqMsgId, MessageItem outboxMessageItem, bool clearDraft, int groupItemCount, Guid correlationId
    ) : base(aggregateId, reqMsgId)
    {
        OutboxMessageItem = outboxMessageItem;
        ClearDraft = clearDraft;
        GroupItemCount = groupItemCount;
        CorrelationId = correlationId;
    }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(ReqMsgId);
        yield return BitConverter.GetBytes(OutboxMessageItem.RandomId);
    }
}