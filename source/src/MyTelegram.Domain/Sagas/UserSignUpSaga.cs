namespace MyTelegram.Domain.Sagas;

public class UserSignUpSaga : AggregateSaga<UserSignUpSaga, UserSignUpSagaId, UserSignUpSagaLocator>,
    ISagaIsStartedBy<AppCodeAggregate, AppCodeId, CheckSignUpCodeCompletedEvent>,
    IApply<UserSignUpSuccessEvent>
{
    private readonly IIdGenerator _idGenerator;

    public UserSignUpSaga(UserSignUpSagaId id,
        IIdGenerator idGenerator) : base(id)
    {
        _idGenerator = idGenerator;
    }

    public void Apply(UserSignUpSuccessEvent aggregateEvent)
    {
    }

    public async Task HandleAsync(IDomainEvent<AppCodeAggregate, AppCodeId, CheckSignUpCodeCompletedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var userId = domainEvent.AggregateEvent.UserId;
        if (userId == 0)
        {
            userId = await _idGenerator.NextLongIdAsync(IdType.UserId, cancellationToken: cancellationToken)
                ;

            var createUserCommand = new CreateUserCommand(UserId.Create(userId),
                domainEvent.AggregateEvent.RequestInfo,
                userId,
                domainEvent.AggregateEvent.AccessHash,
                domainEvent.AggregateEvent.PhoneNumber,
                domainEvent.AggregateEvent.FirstName,
                domainEvent.AggregateEvent.LastName
            );
            Publish(createUserCommand);

            Emit(new UserSignUpSuccessEvent(domainEvent.AggregateEvent.RequestInfo,
                userId,
                domainEvent.AggregateEvent.PhoneNumber));
            Complete();
        }
        else
        {
            Complete();
        }
    }
}
