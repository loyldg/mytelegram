namespace MyTelegram.Domain.Sagas.States;

public class SignInSagaState : AggregateState<SignInSaga, SignInSagaId, SignInSagaState>,
        IApply<SignInStartedEvent>,
        IApply<SignUpRequiredEvent>
{
    public RequestInfo RequestInfo { get; private set; } = default!;

    public void Apply(SignInStartedEvent aggregateEvent)
    {
        RequestInfo =aggregateEvent.RequestInfo;
    }
    public void Apply(SignUpRequiredEvent aggregateEvent)
    {
    }
}
