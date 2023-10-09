namespace MyTelegram.Domain.Commands.Channel;

public class TogglePreHistoryHiddenCommand : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>
{
    public TogglePreHistoryHiddenCommand(ChannelId aggregateId,
        RequestInfo requestInfo,
        bool hidden,
        long selfUserId
    ) : base(aggregateId, requestInfo)
    {
        Hidden = hidden;
        SelfUserId = selfUserId;
    }

    public bool Hidden { get; }
    public long SelfUserId { get; }
}