namespace MyTelegram.Domain.Sagas;

public class VoteSagaCompletedEvent : RequestAggregateEvent2<VoteSaga, VoteSagaId>
{
    public long PollId { get; }
    public long VoterPeerId { get; }
    public IReadOnlyCollection<string> ChosenOptions { get; }
    public Peer ToPeer { get; }

    public VoteSagaCompletedEvent(RequestInfo request, long pollId, long voterPeerId, IReadOnlyCollection<string> chosenOptions, Peer toPeer) : base(request)
    {
        PollId = pollId;
        VoterPeerId = voterPeerId;
        ChosenOptions = chosenOptions;
        ToPeer = toPeer;
    }
}