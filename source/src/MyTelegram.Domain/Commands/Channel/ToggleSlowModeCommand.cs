namespace MyTelegram.Domain.Commands.Channel;

public class ToggleSlowModeCommand : RequestCommand<ChannelAggregate, ChannelId, IExecutionResult>
{
    public ToggleSlowModeCommand(ChannelId aggregateId,
        long reqMsgId,
        int seconds,
        long selfUserId
    ) : base(aggregateId, reqMsgId)
    {
        Seconds = seconds;
        SelfUserId = selfUserId;
    }

    public int Seconds { get; }
    public long SelfUserId { get; }
}
