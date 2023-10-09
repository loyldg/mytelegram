namespace MyTelegram.Domain.Commands.Chat;

public class DeleteChatCommand : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>//, IHasCorrelationId
{
    public DeleteChatCommand(ChatId aggregateId,
        RequestInfo requestInfo) : base(aggregateId, requestInfo)
    {
    }
}