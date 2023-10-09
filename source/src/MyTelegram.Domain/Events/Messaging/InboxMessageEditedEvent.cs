namespace MyTelegram.Domain.Events.Messaging;

public class InboxMessageEditedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>
{
    public InboxMessageEditedEvent(
        RequestInfo requestInfo,
        long inboxOwnerPeerId,
        int messageId,
        string newMessage,
        byte[]? entities,
        int editDate,
        Peer toPeer,
        byte[]? media,
        List<ReactionCount>? reactions,
        List<Reaction>? recentReactions) : base(requestInfo)
    {
        InboxOwnerPeerId = inboxOwnerPeerId;
        MessageId = messageId;
        NewMessage = newMessage;
        Entities = entities;
        EditDate = editDate;
        ToPeer = toPeer;
        Media = media;
        Reactions = reactions;
        RecentReactions = recentReactions;

    }

    public Peer ToPeer { get; }
    public byte[]? Entities { get; }
    public int EditDate { get; }
    public long InboxOwnerPeerId { get; }
    public byte[]? Media { get; }
    public List<ReactionCount>? Reactions { get; }
    public List<Reaction>? RecentReactions { get; }

    public int MessageId { get; }
    public string NewMessage { get; }

}