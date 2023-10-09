namespace MyTelegram.Domain.Commands.Chat;

public class EditChatDefaultBannedRightsCommand : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>
{
    public EditChatDefaultBannedRightsCommand(ChatId aggregateId,
        RequestInfo requestInfo,
        ChatBannedRights chatBannedRights,
        long selfUserId
    ) : base(aggregateId, requestInfo)
    {
        ChatBannedRights = chatBannedRights;
        SelfUserId = selfUserId;
    }

    public ChatBannedRights ChatBannedRights { get; }
    public long SelfUserId { get; }
}