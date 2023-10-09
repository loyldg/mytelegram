namespace MyTelegram.Domain.Commands.Channel;

public class
    CreateChannelCreatorMemberCommand : RequestCommand2<ChannelMemberAggregate, ChannelMemberId, IExecutionResult>
{
    public CreateChannelCreatorMemberCommand(ChannelMemberId aggregateId,
        RequestInfo requestInfo,
        long channelId,
        long userId,
        int date) : base(aggregateId, requestInfo)
    {
        ChannelId = channelId;
        UserId = userId;
        Date = date;
    }

    public long ChannelId { get; }

    //public int InviterId { get; }
    public int Date { get; }
    public long UserId { get; }
}