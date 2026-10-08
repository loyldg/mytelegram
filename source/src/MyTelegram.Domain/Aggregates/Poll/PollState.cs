using System.Collections.Concurrent;

namespace MyTelegram.Domain.Aggregates.Poll;

public class PollState : AggregateState<PollAggregate, PollId, PollState>, IApply<PollCreatedEvent>,
    IApply<VoteSucceededEvent>,
    IApply<VoteAnswerCreatedEvent>,
    IApply<VoteAnswerDeletedEvent>,
    IApply<PollClosedEvent>,
    IApply<PollCreatedV2Event>,
    IApply<PollAnswerAddedEvent>,
    IApply<PollAnswerDeletedEvent>,
    IApply<PollVotersUpdatedEvent>
{
    public long PollId { get; private set; }
    public long CreatorUid { get; private set; }
    public bool Closed { get; private set; }
    public bool PublicVoters { get; private set; }
    public bool MultipleChoice { get; private set; }
    public bool Quiz { get; private set; }
    public string Question { get; private set; } = default!;
    public string Solution { get; private set; } = default!;
    public byte[] SolutionEntities { get; private set; } = default!;
    public int CreationTime { get; private set; }
    public int CloseDate { get; private set; }
    public int ClosePeriod { get; private set; }
    public Peer ToPeer { get; private set; } = default!;

    public ConcurrentDictionary<string, List<long>> OptionsToVoterUsers { get; } = [];
    public List<string> Options { get; private set; } = [];
    public IReadOnlyCollection<string>? CorrectAnswers { get; private set; }
    public IReadOnlyCollection<PollAnswer> Answers { get; private set; } = default!;
    public List<PollAnswerVoter> AnswerVoters { get; private set; } = new List<PollAnswerVoter>();
    public HashSet<long> VotedPeerIds { get; private set; } = new();
    private readonly ConcurrentDictionary<string, HashSet<long>> _optionToVoterPeers = [];

    public bool OpenAnswers { get; private set; }
    public bool RevotingDisabled { get; private set; }
    public List<IPollAnswer> Answers2 { get; private set; } = [];

    public Dictionary<string, List<long>> RecentVoters { get; private set; } = [];

    public void Apply(PollCreatedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
        PollId = aggregateEvent.PollId;
        Options = aggregateEvent.Answers.Select(p => p.Option).ToList();
        Answers = aggregateEvent.Answers;
        CorrectAnswers = aggregateEvent.CorrectAnswers;
        ToPeer = aggregateEvent.ToPeer;
        Quiz = aggregateEvent.Quiz;
        MultipleChoice = aggregateEvent.MultipleChoice;

        var answerVoters = new List<PollAnswerVoter>();
        foreach (var answer in Answers)
        {
            var correct = CorrectAnswers?.Contains(answer.Option) ?? false;
            var voter = new PollAnswerVoter(correct, answer.Option, 0, []);
            answerVoters.Add(voter);
        }
        AnswerVoters = answerVoters;
        Answers2 = aggregateEvent.Answers.Select(IPollAnswer (p) => new TPollAnswer
        {
            Option = p.Option,
            Text = new TTextWithEntities
            {
                Text = p.Text,
                Entities = []
            }
        }).ToList();
    }

    public void Apply(VoteSucceededEvent aggregateEvent)
    {
        VotedPeerIds.Add(aggregateEvent.VoteUserPeerId);

        AnswerVoters = aggregateEvent.AnswerVoters;

        foreach (var option in aggregateEvent.Options)
        {
            if (!_optionToVoterPeers.TryGetValue(option, out var voterPeers))
            {
                voterPeers = new HashSet<long>();
                _optionToVoterPeers.TryAdd(option, voterPeers);
            }
            voterPeers.Add(aggregateEvent.VoteUserPeerId);
        }
    }

    public void Apply(VoteAnswerCreatedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }

    public List<string> GetVoteOptionsByUserId(long userId)
    {
        var options = new List<string>();
        foreach (var kv in _optionToVoterPeers)
        {
            if (kv.Value.Contains(userId))
            {
                options.Add(kv.Key);
            }
        }

        return options;
    }

    public void Apply(VoteAnswerDeletedEvent aggregateEvent)
    {
        VotedPeerIds.Remove(aggregateEvent.VoterPeerId);
        //throw new NotImplementedException();
    }

    public void Apply(PollClosedEvent aggregateEvent)
    {
        Closed = true;
        CloseDate = aggregateEvent.CloseDate;
    }

    public void Apply(PollCreatedV2Event aggregateEvent)
    {
        PollId = aggregateEvent.PollId;
        Answers = aggregateEvent.Answers;
        CorrectAnswers = aggregateEvent.CorrectAnswers;
        ToPeer = aggregateEvent.ToPeer;
        Quiz = aggregateEvent.Quiz;
        MultipleChoice = aggregateEvent.MultipleChoice;

        var answerVoters = new List<PollAnswerVoter>();
        foreach (var answer in Answers2)
        {
            if (answer is TPollAnswer pollAnswer)
            {
                var correct = CorrectAnswers?.Contains(pollAnswer.Option) ?? false;
                var voter = new PollAnswerVoter(correct, pollAnswer.Option, 0, []);
                answerVoters.Add(voter);
            }
        }
        AnswerVoters = answerVoters;
        Options = aggregateEvent.Options;

        OpenAnswers = aggregateEvent.OpenAnswers;
        RevotingDisabled = aggregateEvent.RevotingDisabled;
        Answers2 = aggregateEvent.Answers2;
    }

    public void Apply(PollAnswerAddedEvent aggregateEvent)
    {
        Answers2 = aggregateEvent.Answers;
        if (aggregateEvent.PollAnswer is TPollAnswer pollAnswer)
        {
            Options.Add(pollAnswer.Option);
            AnswerVoters.Add(new PollAnswerVoter(false, pollAnswer.Option, 0, []));
        }
    }

    public void Apply(PollAnswerDeletedEvent aggregateEvent)
    {
        Answers2 = aggregateEvent.Answers;
        Options.Remove(aggregateEvent.Option);
        AnswerVoters.RemoveAll(p => p.Option == aggregateEvent.Option);
    }

    public void LoadSnapshot(PollSnapshot snapshot)
    {
        PollId = snapshot.PollId;
        Options = snapshot.Options;
        Answers2 = snapshot.Answers2;
        Closed = snapshot.Closed;
        Quiz = snapshot.Quiz;
        MultipleChoice = snapshot.MultipleChoice;
        VotedPeerIds = snapshot.VotedPeerIds;
        ToPeer = snapshot.ToPeer;
    }

    public void Apply(PollVotersUpdatedEvent aggregateEvent)
    {
        AnswerVoters = aggregateEvent.AnswerVoters;
    }
}