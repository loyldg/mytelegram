namespace MyTelegram.Domain.Events.Channel;

public class ChannelMemberCreatedEvent : AggregateEvent<ChannelMemberAggregate, ChannelMemberId>, IHasCorrelationId
{
    public ChannelMemberCreatedEvent( //long reqMsgId,
        long channelId,
        long userId,
        long inviterId,
        int date,
        bool isRejoin,
        ChatBannedRights? bannedRights,
        bool isBot,
        Guid correlationId) //: base(reqMsgId)
    {
        ChannelId = channelId;
        UserId = userId;
        InviterId = inviterId;
        Date = date;
        IsRejoin = isRejoin;
        BannedRights = bannedRights;
        IsBot = isBot;
        CorrelationId = correlationId;
    }

    public ChatBannedRights? BannedRights { get; }
    public long ChannelId { get; }
    public int Date { get; }
    public long InviterId { get; }
    public bool IsBot { get; }
    public bool IsRejoin { get; }
    public long UserId { get; }

    public Guid CorrelationId { get; }
}
