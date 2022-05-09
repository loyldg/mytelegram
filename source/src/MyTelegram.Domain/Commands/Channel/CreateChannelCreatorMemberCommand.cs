namespace MyTelegram.Domain.Commands.Channel;

public class
    CreateChannelCreatorMemberCommand : RequestCommand<ChannelMemberAggregate, ChannelMemberId, IExecutionResult>
{
    public CreateChannelCreatorMemberCommand(ChannelMemberId aggregateId,
        long reqMsgId,
        long channelId,
        long userId,
        int date) : base(aggregateId, reqMsgId)
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
