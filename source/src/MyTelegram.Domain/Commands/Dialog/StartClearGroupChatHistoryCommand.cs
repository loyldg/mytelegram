namespace MyTelegram.Domain.Commands.Dialog;

public class StartClearGroupChatHistoryCommand : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>,
    IHasCorrelationId
{
    public StartClearGroupChatHistoryCommand(ChatId aggregateId,
        RequestInfo requestInfo) : base(aggregateId, requestInfo)
    {
    }

}