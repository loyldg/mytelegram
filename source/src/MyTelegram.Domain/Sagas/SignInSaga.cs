using EventFlow.Exceptions;

namespace MyTelegram.Domain.Sagas;

public class SignInSaga :
    MyInMemoryAggregateSaga<SignInSaga, SignInSagaId, SignInSagaLocator>,
    ISagaIsStartedBy<AppCodeAggregate, AppCodeId, CheckSignInCodeCompletedEvent>,
    ISagaHandles<UserAggregate, UserId, CheckUserStatusCompletedEvent>,
    IApply<SignInSuccessEvent>,
    IApply<SignUpRequiredEvent>
{
    private readonly SignInSagaState _state = new();

    public SignInSaga(SignInSagaId id, IEventStore eventStore) : base(id, eventStore)
    {
        Register(_state);
    }

    public void Apply(SignInSuccessEvent aggregateEvent)
    {
        CompleteAsync();
    }

    public Task HandleAsync(IDomainEvent<UserAggregate, UserId, CheckUserStatusCompletedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new SignInSuccessEvent(_state.RequestInfo.ReqMsgId,
            _state.RequestInfo.AuthKeyId,
            _state.RequestInfo.PermAuthKeyId,
            domainEvent.AggregateEvent.UserId,
            domainEvent.AggregateEvent.UserId == 0,
            domainEvent.AggregateEvent.PhoneNumber,
            domainEvent.AggregateEvent.FirstName,
            domainEvent.AggregateEvent.LastName,
            domainEvent.AggregateEvent.HasPassword));

        return Task.CompletedTask;
    }

    public async Task HandleAsync(IDomainEvent<AppCodeAggregate, AppCodeId, CheckSignInCodeCompletedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        if (!domainEvent.AggregateEvent.IsCodeValid)
        {
            await CompleteAsync(cancellationToken);
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.PhoneCodeInvalid);
        }

        if (domainEvent.AggregateEvent.UserId == 0)
        {
            Emit(new SignUpRequiredEvent(domainEvent.AggregateEvent.RequestInfo));
            return;
        }
        Emit(new SignInStartedEvent(domainEvent.AggregateEvent.RequestInfo));
        var checkUserStatusCommand = new CheckUserStatusCommand(UserId.Create(domainEvent.AggregateEvent.UserId),
            domainEvent.AggregateEvent.RequestInfo.ReqMsgId,
            domainEvent.AggregateEvent.CorrelationId);
        Publish(checkUserStatusCommand);
    }
    public void Apply(SignUpRequiredEvent aggregateEvent)
    {
        CompleteAsync();
    }
}
