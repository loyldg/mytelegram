namespace MyTelegram.Domain.Commands.Channel;

public class UpdateChannelUserNameCommand : RequestCommand<ChannelAggregate, ChannelId, IExecutionResult>,
    IHasCorrelationId
{
    public UpdateChannelUserNameCommand(ChannelId aggregateId,
        long reqMsgId,
        long channelId,
        string userName,
        Guid correlationId) : base(aggregateId, reqMsgId)
    {
        ChannelId = channelId;
        UserName = userName;
        CorrelationId = correlationId;
    }

    public long ChannelId { get; }
    public string UserName { get; }
    public Guid CorrelationId { get; }
}
