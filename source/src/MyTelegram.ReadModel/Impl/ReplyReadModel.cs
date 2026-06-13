namespace MyTelegram.ReadModel.Impl
{
    public class ReplyReadModel : ReadModelBase, IReplyReadModel,
    //IAmReadModelFor<SendMessageSaga, SendMessageSagaId, ReplyToChannelMessageCompletedEvent2>
    IAmReadModelFor<ForwardMessageSaga, ForwardMessageSagaId, MessageReplyCreatedSagaEvent>,
        IAmReadModelFor<MessageAggregate, MessageId, ReplyChannelMessageCompletedEvent>,
    IAmReadModelFor<SendMessageSaga, SendMessageSagaId, ReplyBroadcastChannelCompletedSagaEvent>,
    IAmReadModelFor<MessageAggregate, MessageId, MessageReplyUpdatedEvent>
    {
        public string Id { get; private set; } = default!;
        public virtual long? Version { get; set; }

        public long ChannelId { get; private set; }
        public int MessageId { get; private set; }
        //public long PostChannelId { get; private set; }
        //public int? PostMessageId { get; private set; }
        public int Replies { get; private set; }
        public int RepliesPts { get; private set; }
        public IReadOnlyCollection<Peer>? RecentRepliers { get; private set; }
        public int? MaxId { get; private set; }
        public long? CommentChannelId { get; private set; }

        public Task ApplyAsync(IReadModelContext context, IDomainEvent<ForwardMessageSaga, ForwardMessageSagaId, MessageReplyCreatedSagaEvent> domainEvent, CancellationToken cancellationToken)
        {
            Id = ReplyId.Create(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.MessageId).Value;

            ChannelId = domainEvent.AggregateEvent.ChannelId;
            CommentChannelId = domainEvent.AggregateEvent.ChannelId;
            MessageId = domainEvent.AggregateEvent.MessageId;
            //PostChannelId = domainEvent.AggregateEvent.PostChannelId;
            //PostMessageId = domainEvent.AggregateEvent.PostMessageId;

            return Task.CompletedTask;
        }

        public Task ApplyAsync(IReadModelContext context, IDomainEvent<MessageAggregate, MessageId, ReplyChannelMessageCompletedEvent> domainEvent, CancellationToken cancellationToken)
        {
            Id = ReplyId.Create(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.ReplyToMessageId).Value;
            ChannelId = domainEvent.AggregateEvent.ChannelId;
            if (CommentChannelId == 0)
            {
                CommentChannelId = domainEvent.AggregateEvent.ChannelId;
            }
            Replies = domainEvent.AggregateEvent.Reply.Replies;
            RepliesPts = domainEvent.AggregateEvent.Reply.RepliesPts;
            MaxId = domainEvent.AggregateEvent.Reply.MaxId;
            RecentRepliers = domainEvent.AggregateEvent.Reply.RecentRepliers;

            return Task.CompletedTask;
        }

        public Task ApplyAsync(IReadModelContext context, IDomainEvent<SendMessageSaga, SendMessageSagaId, ReplyBroadcastChannelCompletedSagaEvent> domainEvent, CancellationToken cancellationToken)
        {
            Id = ReplyId.Create(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.MessageId).Value;
            ChannelId = domainEvent.AggregateEvent.ChannelId;

            if (CommentChannelId == 0)
            {
                CommentChannelId = domainEvent.AggregateEvent.ChannelId;
            }
            MessageId = domainEvent.AggregateEvent.MessageId;

            Replies = domainEvent.AggregateEvent.Reply.Replies;
            RepliesPts = domainEvent.AggregateEvent.Reply.RepliesPts;
            MaxId = domainEvent.AggregateEvent.Reply.MaxId;
            RecentRepliers = domainEvent.AggregateEvent.Reply.RecentRepliers;

            return Task.CompletedTask;
        }

        public Task ApplyAsync(IReadModelContext context, IDomainEvent<MessageAggregate, MessageId, MessageReplyUpdatedEvent> domainEvent, CancellationToken cancellationToken)
        {
            Id = ReplyId.Create(domainEvent.AggregateEvent.OwnerChannelId, domainEvent.AggregateEvent.MessageId).Value;

            if (ChannelId == 0)
            {
                ChannelId = domainEvent.AggregateEvent.OwnerChannelId;
            }
            CommentChannelId = domainEvent.AggregateEvent.ChannelId;
            MessageId = domainEvent.AggregateEvent.MessageId;

            return Task.CompletedTask;
        }
    }
}
