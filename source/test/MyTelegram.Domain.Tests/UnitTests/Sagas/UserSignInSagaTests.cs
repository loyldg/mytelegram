using MyTelegram.Domain.Sagas.Events;

namespace MyTelegram.Domain.Tests.UnitTests.Sagas;

public class UserSignInSagaTests : TestsFor<SignInSaga>
{
    private readonly Mock<ISagaContext> _sagaContext;

    public UserSignInSagaTests()
    {
        Fixture.Customize<AppCodeId>(c => c.FromFactory(() => AppCodeId.Create("0", "0")));
        Fixture.Customize<SignInSagaId>(c => c.FromFactory(() => new SignInSagaId($"signinsagaid-{Guid.Empty}")));
        _sagaContext = InjectMock<ISagaContext>();
        //var idGenerator = InjectMock<IIdGenerator>();
        //IdGeneratorFactory.SetDefaultIdGenerator(idGenerator.Object);
    }

    [Fact]
    public async Task SignIn_With_Invalid_PhoneCode_Throws_Exception()
    {
        var aggregateEvent = new CheckSignInCodeCompletedEvent(A<RequestInfo>(), false, 1, Guid.Empty);
        var domainEvent =
            ADomainEvent<AppCodeAggregate, AppCodeId, CheckSignInCodeCompletedEvent>(aggregateEvent, A<AppCodeId>(), 1);

        var exception = await Assert.ThrowsAsync<DomainError>(async () => await Sut.HandleAsync(domainEvent, _sagaContext.Object, CancellationToken.None).ConfigureAwait(false));

        exception.Message.ShouldBe(RpcErrorMessages.PhoneCodeInvalid);
    }

    [Fact]
    public async Task SignIn_With_Correct_PhoneCode_Success()
    {
        var aggregateEvent = new CheckSignInCodeCompletedEvent(A<RequestInfo>(), true, 1, Guid.Empty);
        var domainEvent =
            ADomainEvent<AppCodeAggregate, AppCodeId, CheckSignInCodeCompletedEvent>(aggregateEvent, A<AppCodeId>(), 1);

        await Sut.HandleAsync(domainEvent, _sagaContext.Object, CancellationToken.None).ConfigureAwait(false);

        Sut.UncommittedEvents.Single().AggregateEvent.ShouldBeOfType<SignInStartedEvent>();
    }
}