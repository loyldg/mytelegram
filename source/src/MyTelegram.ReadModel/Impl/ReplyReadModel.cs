namespace MyTelegram.ReadModel.Impl;

public class ReplyReadModel : IReplyReadModel,
    IAmReadModelFor<SendMessageSaga, SendMessageSagaId, ReplyToChannelMessageCompletedEvent2>
{
    public string Id { get; private set; } = default!;
    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<SendMessageSaga, SendMessageSagaId, ReplyToChannelMessageCompletedEvent2> domainEvent,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(Id))
        {
            Id = MessageId.Create(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.ReplyToMsgId).Value;
        }

        Replies++;
        RepliesPts = domainEvent.AggregateEvent.RepliesPts;
        ChannelId = domainEvent.AggregateEvent.ChannelId;
        ReplyToMsgId = domainEvent.AggregateEvent.ReplyToMsgId;
        MaxId = domainEvent.AggregateEvent.MaxId;
        SavedFromPeerId = domainEvent.AggregateEvent.SavedFromPeerId;
        SavedFromMsgId = domainEvent.AggregateEvent.SavedFromMsgId;
        RecentRepliers = domainEvent.AggregateEvent.RecentRepliers;

        return Task.CompletedTask;
    }

    public long ChannelId { get; private set; }
    public int MaxId { get; private set; }
    public IReadOnlyCollection<Peer>? RecentRepliers { get; private set; }
    public int Replies { get; private set; }
    public int RepliesPts { get; private set; }
    public int ReplyToMsgId { get; private set; }
    public int SavedFromMsgId { get; private set; }
    public long SavedFromPeerId { get; private set; }
}
