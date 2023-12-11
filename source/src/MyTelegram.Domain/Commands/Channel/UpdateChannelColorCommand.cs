namespace MyTelegram.Domain.Commands.Channel;

public class UpdateChannelColorCommand : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>
{
    public PeerColor Color { get; }

    public UpdateChannelColorCommand(ChannelId aggregateId, RequestInfo requestInfo,PeerColor color) : base(aggregateId, requestInfo)
    {
        Color = color;
    }
}