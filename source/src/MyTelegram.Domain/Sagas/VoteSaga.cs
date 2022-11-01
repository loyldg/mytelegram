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
            var command = new CreateVoteAnswerCommand(domainEvent.AggregateIdentity,
                domainEvent.AggregateEvent.PollId,
                domainEvent.AggregateEvent.VoteUserPeerId,
                option,
                correct ?? false);
            Publish(command);
        }

        foreach (var _ in domainEvent.AggregateEvent.RetractVoteOptions ?? Array.Empty<string>())
        {
            var command = new DeleteVoteAnswerCommand(
                domainEvent.AggregateIdentity,
                domainEvent.AggregateEvent.PollId,
                domainEvent.AggregateEvent.VoteUserPeerId);
            Publish(command);
        }

        Emit(new VoteSagaCompletedEvent(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.PollId,
            domainEvent.AggregateEvent.VoteUserPeerId,
            domainEvent.AggregateEvent.Options,
            domainEvent.AggregateEvent.ToPeer));
        return Task.CompletedTask;
    }
}
