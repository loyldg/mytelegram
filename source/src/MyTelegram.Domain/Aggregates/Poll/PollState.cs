using System.Collections.Concurrent;
using MyTelegram.Domain.Events.Poll;

namespace MyTelegram.Domain.Aggregates.Poll;

public class PollState : AggregateState<PollAggregate, PollId, PollState>, IApply<PollCreatedEvent>,
    IApply<VoteSucceededEvent>,
    IApply<VoteAnswerCreatedEvent>,
    IApply<VoteAnswerDeletedEvent>,
    IApply<PollClosedEvent>
{
    public long PollId { get; private set; }
    //public long CreatorUid { get; private set; }
    public bool Closed { get; private set; }
    //public bool PublicVoters { get; private set; }
    public bool MultipleChoice { get; private set; }
    public bool Quiz { get; private set; }
    public string Question { get; private set; } = null!;
    //public string? Solution { get; private set; }
    //public byte[]? SolutionEntities { get; private set; }
    //public int CreationTime { get; private set; }
    public int CloseDate { get; private set; }
    //public int ClosePeriod { get; private set; }
    public Peer ToPeer { get; private set; } = null!;

    public ConcurrentDictionary<string, List<long>> OptionsToVoterUsers { get; } = new();
    public List<string> Options { get; private set; } = new();
    public IReadOnlyCollection<string>? CorrectAnswers { get; private set; }
    public IReadOnlyCollection<PollAnswer> Answers { get; private set; } = null!;
    public IReadOnlyCollection<PollAnswerVoter> AnswerVoters { get; private set; } = new List<PollAnswerVoter>();
    public HashSet<long> VotedPeerIds { get; private set; } = new();
    private readonly ConcurrentDictionary<string, HashSet<long>> _optionToVoterPeers = new();
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
            var voter = new PollAnswerVoter(correct, answer.Option, 0);
            answerVoters.Add(voter);
        }
        AnswerVoters = answerVoters;
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
}