namespace MyTelegram.Domain.Commands.Chat;

public class StartSendChatMessageCommand : /*RequestInfo*/Command<ChatAggregate, ChatId, IExecutionResult>,
    IHasCorrelationId
{
    public StartSendChatMessageCommand(ChatId aggregateId,
        //long reqMsgId,
        long senderPeerId,
        int senderMessageId,
        bool senderIsBot,
        //int deletedUid,
        Guid correlationId) : base(aggregateId /*, reqMsgId*/)
    {
        SenderPeerId = senderPeerId;
        SenderMessageId = senderMessageId;
        SenderIsBot = senderIsBot;
        CorrelationId = correlationId;
    }

    public bool SenderIsBot { get; }
    public int SenderMessageId { get; }

    public long SenderPeerId { get; }
    public Guid CorrelationId { get; }

    //protected override IEnumerable<byte[]> GetSourceIdComponents()
    //{
    //    yield return BitConverter.GetBytes(ReqMsgId);
    //    yield return Encoding.UTF8.GetBytes(AggregateId.Value);
    //}
}
