namespace MyTelegram.Domain.Commands.Chat;

public class EditChatAboutCommand : RequestCommand<ChatAggregate, ChatId, IExecutionResult>
{
    public EditChatAboutCommand(ChatId aggregateId,
        long reqMsgId,
        long selfUserId,
        string? about) : base(aggregateId, reqMsgId)
    {
        SelfUserId = selfUserId;
        About = about;
    }

    public string? About { get; }
    public long SelfUserId { get; }
}
