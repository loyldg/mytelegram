namespace MyTelegram.ReadModel.Impl;

public class PollAnswerVoterReadModel : IPollAnswerVoterReadModel,
    IAmReadModelFor<PollAggregate, PollId, VoteAnswerCreatedEvent>,
    IAmReadModelFor<PollAggregate, PollId, VoteAnswerDeletedEvent>
{
    public virtual string Id { get; private set; } = null!;
    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<PollAggregate, PollId, VoteAnswerCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(Id))
        {
            Id = $"{domainEvent.AggregateIdentity}_{domainEvent.AggregateEvent.VoterPeerId}";
        }

        PollId = domainEvent.AggregateEvent.PollId;
        Option = domainEvent.AggregateEvent.Option;
        VoterPeerId = domainEvent.AggregateEvent.VoterPeerId;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<PollAggregate, PollId, VoteAnswerDeletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        context.MarkForDeletion();

        return Task.CompletedTask;
    }

    public long PollId { get; private set; }
    public long VoterPeerId { get; private set; }
    public string Option { get; private set; } = null!;
}
