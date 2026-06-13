namespace MyTelegram.ReadModel.Interfaces;

public interface IPollReadModel : IReadModel
{
    string Id { get; }
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
    IList<IMessageEntity>? SolutionEntities2 { get; }
    bool Closed { get; }
    int? CloseDate { get; }
    int? ClosePeriod { get; }
    int TotalVoters { get; }
    IReadOnlyCollection<PollAnswerVoter>? AnswerVoters { get; }
    byte[]? QuestionEntities { get; }
    IList<IMessageEntity>? QuestionEntities2 { get; }
    long? CreatorUserId { get; }
}