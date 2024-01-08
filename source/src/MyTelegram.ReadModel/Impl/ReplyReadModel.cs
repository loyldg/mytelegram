namespace MyTelegram.ReadModel.Impl;

public class ReplyReadModel : IReplyReadModel,
    IAmReadModelFor<SendMessageSaga, SendMessageSagaId, ReplyToChannelMessageCompletedEvent2>
{
    public string Id { get; private set; } = default!;
    public virtual long? Version { get; set; }

    public int Replies { get; private set; }
    public int RepliesPts { get; private set; }
    public long SavedFromPeerId { get; private set; }
    public int SavedFromMsgId { get; private set; }
    public long ChannelId { get; private set; }
    public int MaxId { get; private set; }
    public IReadOnlyCollection<Peer>? RecentRepliers { get; private set; }
    public IInputReplyTo InputReplyTo { get; private set; }
    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<SendMessageSaga, SendMessageSagaId, ReplyToChannelMessageCompletedEvent2> domainEvent,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(Id))
        {
            Id = MessageId.Create(domainEvent.AggregateEvent.ChannelId, GetReplyToMsgId(domainEvent.AggregateEvent.ReplyTo)).Value;
        }

        Replies++;
        RepliesPts = domainEvent.AggregateEvent.RepliesPts;
        ChannelId = domainEvent.AggregateEvent.ChannelId;
        //ReplyToMsgId = domainEvent.AggregateEvent.ReplyToMsgId;
        InputReplyTo = domainEvent.AggregateEvent.ReplyTo;
        MaxId = domainEvent.AggregateEvent.MaxId;
        SavedFromPeerId = domainEvent.AggregateEvent.SavedFromPeerId;
        SavedFromMsgId = domainEvent.AggregateEvent.SavedFromMsgId;
        RecentRepliers = domainEvent.AggregateEvent.RecentRepliers;

        return Task.CompletedTask;
    }

    private int GetReplyToMsgId(IInputReplyTo inputReplyTo)
    {
        switch (inputReplyTo)
        {
            case TInputReplyToMessage inputReplyToMessage:
                return inputReplyToMessage.ReplyToMsgId;
            case TInputReplyToStory inputReplyToStory:
                return inputReplyToStory.StoryId;
        }

        return 0;
    }
}

