namespace MyTelegram.Domain.Commands.ChatInvite;

public class DeleteExportedInviteCommand : RequestCommand2<ChatInviteAggregate, ChatInviteId, IExecutionResult>
{
    public DeleteExportedInviteCommand(ChatInviteId aggregateId, RequestInfo requestInfo) : base(aggregateId, requestInfo)
    {
    }
}