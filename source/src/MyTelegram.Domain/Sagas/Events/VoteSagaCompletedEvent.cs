namespace MyTelegram.Domain.Sagas;

public class VoteSagaCompletedEvent : RequestAggregateEvent2<VoteSaga, VoteSagaId>
{
    public VoteSagaCompletedEvent(RequestInfo requestInfo,
        long pollId,
        long voterPeerId,
        IReadOnlyCollection<string> chosenOptions,
        Peer toPeer) : base(requestInfo)
    {
        PollId = pollId;
        VoterPeerId = voterPeerId;
        ChosenOptions = chosenOptions;
        ToPeer = toPeer;
    }

    public long PollId { get; }
    public long VoterPeerId { get; }
    public IReadOnlyCollection<string> ChosenOptions { get; }
    public Peer ToPeer { get; }
}
