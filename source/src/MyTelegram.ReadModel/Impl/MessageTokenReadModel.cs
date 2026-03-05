namespace MyTelegram.ReadModel.Impl;

public class MessageTokenReadModel : IMessageTokenReadModel,
    IAmReadModelFor<MessageTokenAggregate, MessageTokenId, MessageTokenCreatedEvent>,
    IAmReadModelFor<MessageTokenAggregate, MessageTokenId, MessageTokenDeletedEvent>
{
    public long OwnerPeerId { get; private set; }
    public PeerType ToPeerType { get; private set; }
    public long ToPeerId { get; private set; }
    public int MessageId { get; private set; }
    public long SenderUserId { get; private set; }
    public Peer? SavedPeerId { get; private set; }
    public int? TopMsgId { get; private set; }
    public int? ReplyToMsgId { get; private set; }
    public MessageType MessageType { get; private set; }
    public bool Pinned { get; private set; }
    public bool Post { get; private set; }
    public int Date { get; private set; }
    public List<long> Tokens { get; private set; }
    public bool PublicPosts { get; private set; }
    public List<string> Hashtags { get; private set; } = [];
    public virtual string Id { get; private set; } = null!;
    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<MessageTokenAggregate, MessageTokenId, MessageTokenCreatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;

        OwnerPeerId = domainEvent.AggregateEvent.OwnerPeerId;
        ToPeerType = domainEvent.AggregateEvent.ToPeerType;
        ToPeerId = domainEvent.AggregateEvent.ToPeerId;
        MessageId = domainEvent.AggregateEvent.MessageId;
        SenderUserId = domainEvent.AggregateEvent.SenderUserId;
        SavedPeerId = domainEvent.AggregateEvent.SavedPeerId;
        TopMsgId = domainEvent.AggregateEvent.TopMsgId;
        ReplyToMsgId = domainEvent.AggregateEvent.ReplyToMsgId;
        MessageType = domainEvent.AggregateEvent.MessageType;
        Pinned = domainEvent.AggregateEvent.Pinned;
        Post = domainEvent.AggregateEvent.Post;
        Date = domainEvent.AggregateEvent.Date;
        Hashtags = domainEvent.AggregateEvent.Hashtags;
        PublicPosts = domainEvent.AggregateEvent.PublicPosts;

        Tokens = domainEvent.AggregateEvent.Tokens;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<MessageTokenAggregate, MessageTokenId, MessageTokenDeletedEvent> domainEvent, CancellationToken cancellationToken)
    {
        context.MarkForDeletion();

        return Task.CompletedTask;
    }
}