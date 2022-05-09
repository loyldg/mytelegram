namespace MyTelegram.Domain.Sagas.States;

public class SignInSagaState : AggregateState<SignInSaga, SignInSagaId, SignInSagaState>,
        IApply<SignInStartedEvent>
{
    public RequestInfo Request { get; private set; } = default!;

    public void Apply(SignInStartedEvent aggregateEvent)
    {
        Request=aggregateEvent.Request;
    }
}
