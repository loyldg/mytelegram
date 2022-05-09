namespace MyTelegram.Domain.CommandHandlers.Chat;

public class StartSendChatMessageCommandHandler : CommandHandler<ChatAggregate, ChatId, StartSendChatMessageCommand>
{
    public override Task ExecuteAsync(ChatAggregate aggregate,
        StartSendChatMessageCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.StartSendChatMessage( /*command.ReqMsgId,*/ command.SenderPeerId,
            command.SenderMessageId,
            command.SenderIsBot,
            command.CorrelationId);
        return Task.CompletedTask;
    }
}
