namespace MyTelegram.Domain.Sagas;

public class VoteSaga : MyInMemoryAggregateSaga<VoteSaga, VoteSagaId, VoteSagaLocator>,
    ISagaIsStartedBy<PollAggregate, PollId, VoteSucceededEvent>
{
    private readonly VoteState _state = new();

    public VoteSaga(VoteSagaId id,
        IEventStore eventStore) : base(id, eventStore)
    {
        Register(_state);
    }

    public Task HandleAsync(IDomainEvent<PollAggregate, PollId, VoteSucceededEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        foreach (var option in domainEvent.AggregateEvent.Options)
        {
            var correct = domainEvent.AggregateEvent.CorrectAnswers?.Contains(option);
            var command = new CreatePollVoterCommand(PollVoterId.Create(domainEvent.AggregateEvent.PollId, option, domainEvent.AggregateEvent.VoteUserPeerId),
                domainEvent.AggregateEvent.PollId,
                option,
                domainEvent.AggregateEvent.VoteUserPeerId,
                correct ?? false);
            Publish(command);
        }

        if (domainEvent.AggregateEvent.RetractVoteOptions?.Count > 0)
        {
            foreach (var option in domainEvent.AggregateEvent.RetractVoteOptions)
            {
                var command = new DeletePollVoterCommand(PollVoterId.Create(domainEvent.AggregateEvent.PollId, option,
                    domainEvent.AggregateEvent.VoteUserPeerId));
                Publish(command);
            }
        }

        Emit(new VoteSagaCompletedSagaEvent(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.PollId,
            domainEvent.AggregateEvent.VoteUserPeerId,
            domainEvent.AggregateEvent.Options,
            domainEvent.AggregateEvent.ToPeer));

        return CompleteAsync(cancellationToken);
    }
}
