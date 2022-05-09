namespace MyTelegram.Domain.Commands.Channel;

public class StartSendChannelMessageCommand : Command<ChannelAggregate, ChannelId, IExecutionResult>, IHasCorrelationId
{
    public StartSendChannelMessageCommand(ChannelId aggregateId,
        //long reqMsgId,
        long senderPeerId,
        bool senderIsBot,
        int messageId,
        MessageSubType messageSubType,
        Guid correlationId) : base(aggregateId)
    {
        //ReqMsgId = reqMsgId;
        SenderPeerId = senderPeerId;
        SenderIsBot = senderIsBot;
        MessageId = messageId;
        SubType = messageSubType;
        CorrelationId = correlationId;
    }

    public int MessageId { get; }

    public bool SenderIsBot { get; }

    //public long ReqMsgId { get; }
    public long SenderPeerId { get; }
    public MessageSubType SubType { get; }

    public Guid CorrelationId { get; }
}
