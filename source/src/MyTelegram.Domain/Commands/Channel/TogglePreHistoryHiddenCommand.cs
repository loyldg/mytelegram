namespace MyTelegram.Domain.Commands.Channel;

public class TogglePreHistoryHiddenCommand : RequestCommand<ChannelAggregate, ChannelId, IExecutionResult>
{
    public TogglePreHistoryHiddenCommand(ChannelId aggregateId,
        long reqMsgId,
        bool hidden,
        long selfUserId
    ) : base(aggregateId, reqMsgId)
    {
        Hidden = hidden;
        SelfUserId = selfUserId;
    }

    public bool Hidden { get; }
    public long SelfUserId { get; }
}
