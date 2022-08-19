using MyTelegram.Domain.Aggregates.Poll;

namespace MyTelegram.ReadModel;

public interface IPollReadModel : IReadModel
{
    long ToPeerId { get; }
    long PollId { get; }
    bool MultipleChoice { get; }
    bool Quiz { get; }
    bool PublicVoters { get; }
    string Question { get; }
    IReadOnlyCollection<PollAnswer> Answers { get; }
    IReadOnlyCollection<string>? CorrectAnswers { get; }
    string? Solution { get; }
    byte[]? SolutionEntities { get; }
    bool Closed { get; }
    int? CloseDate { get; }
    int? ClosePeriod { get; }
    int TotalVoters { get; }
    IReadOnlyCollection<PollAnswerVoter>? AnswerVoters { get; }
}