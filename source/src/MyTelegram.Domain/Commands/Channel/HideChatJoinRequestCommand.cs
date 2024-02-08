namespace MyTelegram.Domain.Commands.Channel;

public class HideChatJoinRequestCommand : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>
{
    public long UserId { get; }
    public bool Approved { get; }
    
    public HideChatJoinRequestCommand(ChannelId aggregateId, RequestInfo requestInfo, long userId, bool approved) : base(aggregateId, requestInfo)
    {
        UserId = userId;
        Approved = approved;
    }
}