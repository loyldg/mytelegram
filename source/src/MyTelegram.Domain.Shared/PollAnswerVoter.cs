namespace MyTelegram;

public record PollAnswerVoter(bool Correct,
    string Option,
    int Voters,
    List<long> RecentVoters)
{
    public int Voters { get; set; } = Voters;
    public List<long> RecentVoters { get; init; } = RecentVoters;
}