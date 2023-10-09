namespace MyTelegram.Domain.Commands.Channel;

public class UpdateChannelUserNameCommand : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>//,
    //IHasCorrelationId
{
    public UpdateChannelUserNameCommand(ChannelId aggregateId,
        RequestInfo requestInfo,
        long channelId,
        string userName) : base(aggregateId, requestInfo)
    {
        ChannelId = channelId;
        UserName = userName;
    }

    public long ChannelId { get; }
    public string UserName { get; }
}