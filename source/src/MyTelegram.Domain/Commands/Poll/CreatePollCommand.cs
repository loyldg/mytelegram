namespace MyTelegram.Domain.Commands.Poll;

public class CreatePollCommand : Command<PollAggregate, PollId, IExecutionResult>
{
    public CreatePollCommand(PollId aggregateId,
        Peer toPeer,
        long pollId,
        bool multipleChoice,
        bool quiz,
        bool publicVoters,
        string question,
        IReadOnlyCollection<PollAnswer> answers,
        IReadOnlyCollection<string>? correctAnswers,
        string? solution,
        byte[]? solutionEntities) : base(aggregateId)
    {
        ToPeer = toPeer;
        PollId = pollId;
        MultipleChoice = multipleChoice;
        Quiz = quiz;
        PublicVoters = publicVoters;
        Question = question;
        Answers = answers;
        CorrectAnswers = correctAnswers;
        Solution = solution;
        SolutionEntities = solutionEntities;
    }

    public Peer ToPeer { get; }
    public long PollId { get; }
    public bool MultipleChoice { get; }
    public bool Quiz { get; }
    public bool PublicVoters { get; }
    public string Question { get; }
    public IReadOnlyCollection<PollAnswer> Answers { get; }
    public IReadOnlyCollection<string>? CorrectAnswers { get; }
    public string? Solution { get; }
    public byte[]? SolutionEntities { get; }
}
