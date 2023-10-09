namespace MyTelegram.Domain.Events.Messaging;

public class OutboxMessageEditedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>
{
    public OutboxMessageEditedEvent(RequestInfo requestInfo,
        IReadOnlyCollection<InboxItem>? inboxItems,
        MessageItem oldMessageItem,
        int messageId,
        string newMessage,
        int editDate,
        byte[]? entities,
        byte[]? media,
        List<ReactionCount>? reactions,
        List<Reaction>? recentReactions) : base(requestInfo)
    {
        InboxItems = inboxItems;
        OldMessageItem = oldMessageItem;
        MessageId = messageId;
        NewMessage = newMessage;
        Entities = entities;
        Media = media;
        Reactions = reactions;
        RecentReactions = recentReactions;
        //ChatMembers = chatMembers;
        EditDate = editDate;

    }


    public IReadOnlyCollection<InboxItem>? InboxItems { get; }
    public MessageItem OldMessageItem { get; }
    public int MessageId { get; }
    public string NewMessage { get; }
    public byte[]? Entities { get; }
    public byte[]? Media { get; }
    public List<ReactionCount>? Reactions { get; }
    public List<Reaction>? RecentReactions { get; }
    //public List<long>? ChatMembers { get; }
    public int EditDate { get; }
}