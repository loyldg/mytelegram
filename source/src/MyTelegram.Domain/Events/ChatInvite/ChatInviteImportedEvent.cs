namespace MyTelegram.Domain.Events.ChatInvite;

public class ChatInviteImportedEvent : RequestAggregateEvent2<ChatInviteAggregate, ChatInviteId>
{
    public long ChannelId { get; }
    public long InviteId { get; }
    public ChatInviteRequestState ChatInviteRequestState { get; }
    public int? Requested { get; }
    public int? Usage { get; }
    public string Hash { get; }
    public int Date { get; }

    public ChatInviteImportedEvent(RequestInfo requestInfo, long channelId, long inviteId, ChatInviteRequestState chatInviteRequestState, int? requested, int? usage, string hash, int date) : base(requestInfo)
    {
        ChannelId = channelId;
        InviteId = inviteId;
        ChatInviteRequestState = chatInviteRequestState;
        Requested = requested;
        Usage = usage;
        Hash = hash;
        Date = date;
    }
}