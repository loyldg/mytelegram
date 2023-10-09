namespace MyTelegram.Domain.Commands.Channel;

public class ToggleSlowModeCommand : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>
{
    public ToggleSlowModeCommand(ChannelId aggregateId,
        RequestInfo requestInfo,
        int seconds,
        long selfUserId
    ) : base(aggregateId, requestInfo)
    {
        Seconds = seconds;
        SelfUserId = selfUserId;
    }

    public int Seconds { get; }
    public long SelfUserId { get; }
}