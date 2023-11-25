using MyTelegram.Domain.Sagas.Events;

namespace MyTelegram.Domain.Tests.UnitTests.Sagas;

public class UserSignUpSagaTests : TestsFor<UserSignUpSaga>
{
    private readonly Mock<ISagaContext> _sagaContext;

    public UserSignUpSagaTests()
    {
        Fixture.Customize<AppCodeId>(c => c.FromFactory(() => AppCodeId.Create("0", "0")));
        Fixture.Customize<UserSignUpSagaId>(c => c.FromFactory(() => new UserSignUpSagaId($"usersignupsagaid-{Guid.Empty}")));
        _sagaContext = InjectMock<ISagaContext>();
        //var idGenerator = InjectMock<IIdGenerator>();
        //IdGeneratorFactory.SetDefaultIdGenerator(idGenerator.Object);
    }

    [Fact]
    public async Task SignUp_For_New_User_Success()
    {
        var aggregateEvent = new CheckSignUpCodeCompletedEvent(A<RequestInfo>(), true, 0, 0, "0", "0", null);
        var domainEvent = new DomainEvent<AppCodeAggregate, AppCodeId, CheckSignUpCodeCompletedEvent>(aggregateEvent,
            Metadata.Empty,
            A<DateTimeOffset>(),
            A<AppCodeId>(),
            1);

        await Sut.HandleAsync(domainEvent, _sagaContext.Object, CancellationToken.None).ConfigureAwait(false);

        Sut.UncommittedEvents.Single().AggregateEvent.ShouldBeOfType<UserSignUpSuccessEvent>();
    }
}