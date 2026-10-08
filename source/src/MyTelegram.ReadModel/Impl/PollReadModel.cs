namespace MyTelegram.ReadModel.Impl;

public class PollReadModel : ReadModelBase, IPollReadModel,
    IAmReadModelFor<PollAggregate, PollId, PollCreatedEvent>,
    IAmReadModelFor<PollAggregate, PollId, VoteSucceededEvent>,
    IAmReadModelFor<PollAggregate, PollId, PollClosedEvent>,
    IAmReadModelFor<PollAggregate, PollId, PollCreatedV2Event>,
    IAmReadModelFor<PollAggregate, PollId, PollAnswerAddedEvent>,
    IAmReadModelFor<PollAggregate, PollId, PollAnswerDeletedEvent>
{
    public IReadOnlyCollection<PollAnswer>? Answers { get; private set; } = default!;
    public IReadOnlyCollection<PollAnswerVoter>? AnswerVoters { get; private set; }
    public IList<IMessageEntity>? SolutionEntities2 { get; private set; }
    public bool Closed { get; private set; }
    public int? CloseDate { get; private set; }
    public int? ClosePeriod { get; private set; }
    public IReadOnlyCollection<string>? CorrectAnswers { get; private set; }
    public virtual string Id { get; private set; } = null!;
    public bool MultipleChoice { get; private set; }
    public long PollId { get; private set; }
    public bool PublicVoters { get; private set; }
    public string Question { get; private set; } = default!;
    public byte[]? QuestionEntities { get; private set; }
    public IList<IMessageEntity>? QuestionEntities2 { get; private set; }
    public bool Quiz { get; private set; }
    public string? Solution { get; private set; }
    public byte[]? SolutionEntities { get; private set; }
    public long ToPeerId { get; private set; }
    public int TotalVoters { get; private set; }
    public virtual long? Version { get; set; }

    public long? CreatorUserId { get; private set; }
    public bool OpenAnswers { get; private set; }
    public bool RevotingDisabled { get; private set; }
    public bool ShuffleAnswers { get; private set; }
    public bool HideResultsUntilClose { get; private set; }
    public IMessageMedia? AttachedMedia { get; private set; }
    public IMessageMedia? SolutionMedia { get; private set; }
    public IReadOnlyCollection<IPollAnswer>? Answers2 { get; private set; }
    public int Date { get; private set; }

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
        SolutionEntities2 = domainEvent.AggregateEvent.SolutionEntities;
        QuestionEntities2 = domainEvent.AggregateEvent.QuestionEntities;
        CreatorUserId = domainEvent.AggregateEvent.CreatorUserId;

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

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<PollAggregate, PollId, PollClosedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Closed = true;
        CloseDate = domainEvent.AggregateEvent.CloseDate;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<PollAggregate, PollId, PollCreatedV2Event> domainEvent, CancellationToken cancellationToken)
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
        SolutionEntities2 = domainEvent.AggregateEvent.SolutionEntities;
        QuestionEntities2 = domainEvent.AggregateEvent.QuestionEntities;
        CreatorUserId = domainEvent.AggregateEvent.CreatorUserId;
        OpenAnswers = domainEvent.AggregateEvent.OpenAnswers;
        RevotingDisabled = domainEvent.AggregateEvent.RevotingDisabled;
        ShuffleAnswers = domainEvent.AggregateEvent.ShuffleAnswers;
        HideResultsUntilClose = domainEvent.AggregateEvent.HideResultsUntilClose;
        AttachedMedia = domainEvent.AggregateEvent.AttachedMedia;
        SolutionMedia = domainEvent.AggregateEvent.SolutionMedia;
        Answers2 = domainEvent.AggregateEvent.Answers2;
        CloseDate = domainEvent.AggregateEvent.CloseDate;
        ClosePeriod = domainEvent.AggregateEvent.ClosePeriod;
        Date = domainEvent.AggregateEvent.Date;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<PollAggregate, PollId, PollAnswerAddedEvent> domainEvent, CancellationToken cancellationToken)
    {
        Answers2 = domainEvent.AggregateEvent.Answers;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<PollAggregate, PollId, PollAnswerDeletedEvent> domainEvent, CancellationToken cancellationToken)
    {
        Answers2 = domainEvent.AggregateEvent.Answers;

        return Task.CompletedTask;
    }
}
