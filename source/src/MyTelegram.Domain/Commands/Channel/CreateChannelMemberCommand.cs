namespace MyTelegram.Domain.Commands.Channel;

public class CreateChannelMemberCommand : /*RequestInfo*/
    Command<ChannelMemberAggregate, ChannelMemberId, IExecutionResult>,
    IHasCorrelationId
{
    public CreateChannelMemberCommand(ChannelMemberId aggregateId,
        //long reqMsgId,
        long channelId,
        long userId,
        long inviterId,
        int date,
        bool isBot,
        Guid correlationId) : base(aggregateId /*, reqMsgId*/)
    {
        ChannelId = channelId;
        UserId = userId;
        InviterId = inviterId;
        Date = date;
        IsBot = isBot;
        CorrelationId = correlationId;
    }

    public long ChannelId { get; }
    public int Date { get; }
    public long InviterId { get; }
    public bool IsBot { get; }
    public long UserId { get; }

    public Guid CorrelationId { get; }
}
