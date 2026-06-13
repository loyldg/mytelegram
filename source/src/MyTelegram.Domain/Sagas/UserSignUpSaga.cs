namespace MyTelegram.Domain.Sagas;

public class UserSignUpSaga(UserSignUpSagaId id, IIdGenerator idGenerator, IEventStore eventStore)
    : MyInMemoryAggregateSaga<UserSignUpSaga, UserSignUpSagaId, UserSignUpSagaLocator>(id, eventStore),
        ISagaIsStartedBy<AppCodeAggregate, AppCodeId, CheckSignUpCodeCompletedEvent>,
        IApply<UserSignUpSuccessSagaEvent>
{
    public void Apply(UserSignUpSuccessSagaEvent aggregateEvent)
    {
    }

    public async Task HandleAsync(IDomainEvent<AppCodeAggregate, AppCodeId, CheckSignUpCodeCompletedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var userId = domainEvent.AggregateEvent.UserId;
        if (userId == 0)
        {
            userId = await idGenerator.NextLongIdAsync(IdType.UserId, cancellationToken: cancellationToken);

            var createUserCommand = new CreateUserCommand(UserId.Create(userId),
                domainEvent.AggregateEvent.RequestInfo,
                userId,
                domainEvent.AggregateEvent.AccessHash,
                domainEvent.AggregateEvent.PhoneNumber,
                domainEvent.AggregateEvent.FirstName,
                domainEvent.AggregateEvent.LastName
            );
            Publish(createUserCommand);
            Emit(new UserSignUpSuccessSagaEvent(domainEvent.AggregateEvent.RequestInfo,
                userId,
                domainEvent.AggregateEvent.PhoneNumber));
        }

        await CompleteAsync(cancellationToken);
    }
}
