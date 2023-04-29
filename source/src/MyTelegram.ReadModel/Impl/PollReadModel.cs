using MyTelegram.Domain.Events.Poll;

namespace MyTelegram.ReadModel.Impl;

public class PollReadModel : IPollReadModel,
    IAmReadModelFor<PollAggregate, PollId, PollCreatedEvent>,
    IAmReadModelFor<PollAggregate, PollId, VoteSucceededEvent>,
    IAmReadModelFor<PollAggregate, PollId, PollClosedEvent>
{
    public virtual string Id { get; private set; } = null!;
    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<PollAggregate, PollId, PollClosedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Closed = true;
        CloseDate = domainEvent.AggregateEvent.CloseDate;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<PollAggregate, PollId, PollCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        ToPeerId = domainEvent.AggregateEvent.ToPeer.PeerId;
        PollId = domainEvent.AggregateEvent.PollId;
        MultipleChoice = domainEvent.AggregateEvent.MultipleChoice;
        Quiz = domainEvent.AggregateEvent.Quiz;
        PublicVoters = domainEvent.AggregateEvent.PublicVoters;
        Answers = domainEvent.AggregateEvent.Answers;
        Question = domainEvent.AggregateEvent.Question;
        CorrectAnswers = domainEvent.AggregateEvent.CorrectAnswers;
        Solution = domainEvent.AggregateEvent.Solution;
        SolutionEntities = domainEvent.AggregateEvent.SolutionEntities;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<PollAggregate, PollId, VoteSucceededEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        AnswerVoters = domainEvent.AggregateEvent.AnswerVoters;
        if (domainEvent.AggregateEvent.Options.Count > 0)
        {
            TotalVoters++;
        }
        else
        {
            TotalVoters--;
        }

        return Task.CompletedTask;
    }

    public long ToPeerId { get; private set; }
    public long PollId { get; private set; }
    public bool MultipleChoice { get; private set; }
    public bool Quiz { get; private set; }
    public bool PublicVoters { get; private set; }
    public string Question { get; private set; } = null!;
    public IReadOnlyCollection<PollAnswer> Answers { get; private set; } = null!;
    public IReadOnlyCollection<string>? CorrectAnswers { get; private set; }
    public string? Solution { get; private set; }
    public byte[]? SolutionEntities { get; private set; }
    public bool Closed { get; private set; }
    public int? CloseDate { get; private set; }
    public int? ClosePeriod { get; } = null!;
    public int TotalVoters { get; private set; }
    public IReadOnlyCollection<PollAnswerVoter>? AnswerVoters { get; private set; }
}
