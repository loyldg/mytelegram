namespace MyTelegram.Domain.Aggregates.Poll;

public record Poll(long PeerId,
    long PollId,
    bool MultipleChoice,
    bool Quiz,
    string Question,
    IReadOnlyCollection<PollAnswer> Answers,
    IReadOnlyCollection<string>? CorrectAnswers,
    string? Solution,
    byte[]? SolutionEntities,
    bool Closed,
    int CloseDate,
    int ClosePeriod
);
