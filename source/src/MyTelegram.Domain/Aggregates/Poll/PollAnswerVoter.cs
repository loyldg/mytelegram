namespace MyTelegram.Domain.Aggregates.Poll;

public record PollAnswerVoter(bool Correct,
    string Option,
    int Voters)
{
    public int Voters { get; private set; } = Voters;

    public void DecrementVoters()
    {
        Voters--;
        if (Voters < 0)
        {
            Voters = 0;
        }
    }

    public void IncrementVoters()
    {
        Voters++;
    }
}
