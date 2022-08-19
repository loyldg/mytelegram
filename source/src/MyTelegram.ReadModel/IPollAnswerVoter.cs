namespace MyTelegram.ReadModel;

public interface IPollAnswerVoterReadModel : IReadModel
{
    long PollId { get; }
    long VoterPeerId { get; }
    string Option { get; }
}
