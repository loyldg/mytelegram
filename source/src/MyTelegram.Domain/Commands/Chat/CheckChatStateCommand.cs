namespace MyTelegram.Domain.Commands.Chat;

public class CheckChatStateCommand : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>
{
    public CheckChatStateCommand(ChatId aggregateId, RequestInfo requestInfo) : base(aggregateId, requestInfo)
    {

    }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return RequestInfo.RequestId.ToByteArray();
    }
}