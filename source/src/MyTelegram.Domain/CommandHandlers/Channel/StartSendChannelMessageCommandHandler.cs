namespace MyTelegram.Domain.CommandHandlers.Channel;

public class
    StartSendChannelMessageCommandHandler : CommandHandler<ChannelAggregate, ChannelId,
        StartSendChannelMessageCommand>
{
    public override Task ExecuteAsync(ChannelAggregate aggregate,
        StartSendChannelMessageCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.StartSendChannelMessage(
            //command.ReqMsgId,
            command.SenderPeerId,
            command.SenderIsBot,
            command.MessageId,
            command.SubType,
            command.CorrelationId);
        return Task.CompletedTask;
    }
}
