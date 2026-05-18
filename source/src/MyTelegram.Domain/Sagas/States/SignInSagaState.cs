namespace MyTelegram.Domain.Sagas.States;

public class SignInSagaState : AggregateState<SignInSaga, SignInSagaId, SignInSagaState>,
        IApply<SignInStartedSagaEvent>,
        IApply<SignUpRequiredSagaEvent>,
        IApply<SignInSuccessSagaEvent>
{
    public RequestInfo RequestInfo { get; private set; } = default!;

    public void Apply(SignInStartedSagaEvent aggregateEvent)
    {
        RequestInfo = aggregateEvent.RequestInfo;
    }

    public void Apply(SignUpRequiredSagaEvent aggregateEvent)
    {
    }

    public void Apply(SignInSuccessSagaEvent aggregateEvent)
    {
        
    }
}
