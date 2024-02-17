using MyTelegram.Domain.Aggregates.ChatInvite;

namespace MyTelegram.Domain.Commands.ChatInvite;

public class ImportChatInviteCommand : RequestCommand2<ChatInviteAggregate, ChatInviteId, IExecutionResult>
{
    public ChatInviteRequestState ChatInviteRequestState { get; }
    public int Date { get; }

    public ImportChatInviteCommand(ChatInviteId aggregateId, RequestInfo requestInfo, ChatInviteRequestState chatInviteRequestState, int date) : base(aggregateId, requestInfo)
    {
        ChatInviteRequestState = chatInviteRequestState;
        Date = date;
    }
}