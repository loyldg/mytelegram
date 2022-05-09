namespace MyTelegram.Domain.Commands.Chat;

public class SetChatPinnedMsgIdCommand : DistinctCommand<ChatAggregate, ChatId, IExecutionResult>
{
    public SetChatPinnedMsgIdCommand(ChatId aggregateId,
        long reqMsgId,
        int pinnedMsgId) : base(aggregateId)
    {
        ReqMsgId = reqMsgId;
        PinnedMsgId = pinnedMsgId;
    }

    public int PinnedMsgId { get; }
    public long ReqMsgId { get; }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(ReqMsgId);
        yield return BitConverter.GetBytes(PinnedMsgId);
    }
}
