namespace MyTelegram.Domain.Sagas.States;

public class SignInSagaState : AggregateState<SignInSaga, SignInSagaId, SignInSagaState>,
        IApply<SignInStartedEvent>,
        IApply<SignUpRequiredEvent>
{
    public RequestInfo Request { get; private set; } = default!;

    public void Apply(SignInStartedEvent aggregateEvent)
    {
        Request=aggregateEvent.Request;
    }
    public void Apply(SignUpRequiredEvent aggregateEvent)
    {
    }
}
