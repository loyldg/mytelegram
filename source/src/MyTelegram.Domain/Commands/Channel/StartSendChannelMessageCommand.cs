namespace MyTelegram.Domain.Commands.Channel;

public class StartSendChannelMessageCommand : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>//, IHasCorrelationId
{
    public StartSendChannelMessageCommand(ChannelId aggregateId,
        //long reqMsgId,
        RequestInfo requestInfo,
        long senderPeerId,
        bool senderIsBot,
        int messageId,
        MessageSubType messageSubType) : base(aggregateId, requestInfo)
    {
        //ReqMsgId = reqMsgId;
        SenderPeerId = senderPeerId;
        SenderIsBot = senderIsBot;
        MessageId = messageId;
        SubType = messageSubType;
    }

    public int MessageId { get; }

    public bool SenderIsBot { get; }

    //public long ReqMsgId { get; }
    public long SenderPeerId { get; }
    public MessageSubType SubType { get; }
}