namespace MyTelegram.Domain.Events.Dialog;

public class MentionReadEvent(
        RequestInfo requestInfo,
        long ownerUserId,
        Peer toPeer,
        int messageId,
        int unreadMentionsCount)
    : RequestAggregateEvent2<DialogAggregate, DialogId>(requestInfo)
{
    public long OwnerUserId { get; } = ownerUserId;
    public Peer ToPeer { get; } = toPeer;
    public int MessageId { get; } = messageId;
    public int UnreadMentionsCount { get; } = unreadMentionsCount;
}