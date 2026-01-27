namespace MyTelegram.ReadModel.Interfaces;

public interface IPollAnswerVoterReadModel : IReadModel
{
    long PollId { get; }
    long VoterPeerId { get; }
    string Option { get; }
    int Date { get; }
}
