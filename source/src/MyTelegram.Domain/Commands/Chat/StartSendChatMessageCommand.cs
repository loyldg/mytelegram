namespace MyTelegram.Domain.Commands.Chat;

public class StartSendChatMessageCommand : /*Request*/RequestCommand2<ChatAggregate, ChatId, IExecutionResult>//,
    //IHasCorrelationId
{
    public StartSendChatMessageCommand(ChatId aggregateId,
        //long reqMsgId,
        RequestInfo requestInfo,
        long senderPeerId,
        int senderMessageId,
        bool senderIsBot) : base(aggregateId, requestInfo)
    {
        SenderPeerId = senderPeerId;
        SenderMessageId = senderMessageId;
        SenderIsBot = senderIsBot;
    }

    public bool SenderIsBot { get; }
    public int SenderMessageId { get; }

    public long SenderPeerId { get; }

    //protected override IEnumerable<byte[]> GetSourceIdComponents()
    //{
    //    yield return BitConverter.GetBytes(ReqMsgId);
    //    yield return Encoding.UTF8.GetBytes(AggregateId.Value);
    //}
}