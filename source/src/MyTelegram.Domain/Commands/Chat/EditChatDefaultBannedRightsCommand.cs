namespace MyTelegram.Domain.Commands.Chat;

public class EditChatDefaultBannedRightsCommand : RequestCommand<ChatAggregate, ChatId, IExecutionResult>
{
    public EditChatDefaultBannedRightsCommand(ChatId aggregateId,
        long reqMsgId,
        ChatBannedRights chatBannedRights,
        long selfUserId
    ) : base(aggregateId, reqMsgId)
    {
        ChatBannedRights = chatBannedRights;
        SelfUserId = selfUserId;
    }

    public ChatBannedRights ChatBannedRights { get; }
    public long SelfUserId { get; }
}
