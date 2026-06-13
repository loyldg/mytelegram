namespace MyTelegram.Domain.Aggregates.Messaging;

public class MessageTokenId(string value) : Identity<MessageTokenId>(value)
{
    public static MessageTokenId Create(long ownerPeerId, int messageId, bool quickReplyMessage = false)
    {
        var name = $"messagetoken_{ownerPeerId}_{messageId}";
        if (quickReplyMessage)
        {
            name = $"quick_reply_messagetoken_{ownerPeerId}_{messageId}";
        }

        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands, name);
    }
}

public class MessageTokenState : AggregateState<MessageTokenAggregate, MessageTokenId, MessageTokenState>,
    IApply<MessageTokenCreatedEvent>,
    IApply<MessageTokenDeletedEvent>
{
    public void Apply(MessageTokenCreatedEvent aggregateEvent)
    {
    }

    public void Apply(MessageTokenDeletedEvent aggregateEvent)
    {
    }
}

[EnableAutoGeneration]
public class MessageTokenAggregate : AggregateRoot<MessageTokenAggregate, MessageTokenId>
{
    private readonly MessageTokenState _state = new();
    public MessageTokenAggregate(MessageTokenId id) : base(id)
    {
        Register(_state);
    }

    public void CreateMessageToken(long ownerPeerId,
        int messageId,
        PeerType toPeerType,
        long toPeerId,
        long senderUserId,
        Peer? savedPeerId,
        int? topMsgId,
        int? replyToMsgId,
        MessageType messageType,
        bool pinned,
        bool post,
        int date,
        bool publicPosts,
        List<string>? hashtags,
        List<long> tokens)
    {
        Emit(new MessageTokenCreatedEvent(ownerPeerId, messageId,
            toPeerType,
            toPeerId,
            senderUserId,
            savedPeerId,
            topMsgId,
            replyToMsgId,
            messageType,
            pinned,
            post,
            date,
            publicPosts,
            hashtags,
            tokens));
    }

    public void DeleteMessageToken(/*long ownerPeerId, int messageId*/)
    {
        Emit(new MessageTokenDeletedEvent());
    }
}