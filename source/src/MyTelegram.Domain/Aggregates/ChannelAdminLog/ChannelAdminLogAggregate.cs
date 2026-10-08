namespace MyTelegram.Domain.Aggregates.ChannelAdminLog;

public class ChannelAdminLogId(string value) : Identity<ChannelAdminLogId>(value)
{
    public static ChannelAdminLogId Create(long channelId, long logId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands,
            $"channeladminlog-{channelId}-{logId}");
    }
}

public class
    ChannelAdminLogState : AggregateState<ChannelAdminLogAggregate, ChannelAdminLogId, ChannelAdminLogState>,
    IApply<ChannelAdminLogCreatedEvent>
{
    public void Apply(ChannelAdminLogCreatedEvent aggregateEvent)
    {

    }
}

[EnableAutoGeneration]
public class ChannelAdminLogAggregate : AggregateRoot<ChannelAdminLogAggregate, ChannelAdminLogId>, ISkipAggregateEvents
{
    private readonly ChannelAdminLogState _state = new();
    public ChannelAdminLogAggregate(ChannelAdminLogId id) : base(id)
    {
        Register(_state);
    }

    public void CreateChannelAdminLog(long channelId, long logId, long userId, AdminLogEventAction action,
        IChannelAdminLogEventAction channelAdminLogEventAction,
        string? searchData,
        MessageItem? oldMessageItem = null,
        MessageItem? newMessageItem = null,
        long? inviteId = null
        )
    {
        var date = DateTime.UtcNow.ToTimestamp();
        Emit(new ChannelAdminLogCreatedEvent(channelId, logId, userId, action, channelAdminLogEventAction, date, searchData, oldMessageItem, newMessageItem, inviteId));
    }
}
