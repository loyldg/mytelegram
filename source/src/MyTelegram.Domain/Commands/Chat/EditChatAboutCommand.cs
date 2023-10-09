namespace MyTelegram.Domain.Commands.Chat;

public class EditChatAboutCommand : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>
{
    public EditChatAboutCommand(ChatId aggregateId,
        RequestInfo requestInfo,
        long selfUserId,
        string? about) : base(aggregateId, requestInfo)
    {
        SelfUserId = selfUserId;
        About = about;
    }

    public string? About { get; }
    public long SelfUserId { get; }
}