namespace MyTelegram.Domain.Sagas.States;

public class VoteState : AggregateState<VoteSaga, VoteSagaId, VoteState>,
    IApply<VoteSagaCompletedEvent>
{
    public void Apply(VoteSagaCompletedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }
}