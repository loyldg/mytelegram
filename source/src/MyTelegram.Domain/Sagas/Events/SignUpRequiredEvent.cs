namespace MyTelegram.Domain.Sagas.Events;

public class SignUpRequiredEvent : RequestAggregateEvent2<SignInSaga, SignInSagaId>
{
    public SignUpRequiredEvent(RequestInfo requestInfo) : base(requestInfo)
    {
    }
}
