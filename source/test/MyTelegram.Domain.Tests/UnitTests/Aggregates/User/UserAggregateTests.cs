namespace MyTelegram.Domain.Tests.UnitTests.Aggregates.User;

public class UserAggregateTests : TestsFor<UserAggregate>
{
    public UserAggregateTests()
    {
        Fixture.Customize<UserId>(c => c.FromFactory(() => UserId.Create(1)));
    }

    [Fact]
    public void Create_User_With_Exists_PhoneNumber_Throws_Exception()
    {
        var aggregateEvent = A<UserCreatedEvent>();
        var domainEvent = ADomainEvent<UserAggregate, UserId, UserCreatedEvent>(aggregateEvent, 1);

        Sut.ApplyEvents(new[] { domainEvent });

        Assert.Throws<DomainError>(() => Sut.Create(A<RequestInfo>(),
            1,
            0,
            aggregateEvent.PhoneNumber,
            "0"));
    }

    [Fact]
    public void Create_New_User_Success()
    {
        var phoneNumber = "0";
        var firstName = "0";
        var userId = 1;
        var accessHash = 1;

        Sut.Create(A<RequestInfo>(), userId, accessHash, phoneNumber, firstName);

        Sut.Version.ShouldBe(1);
        Sut.UncommittedEvents.Count().ShouldBe(1);
        var uncommittedEvent = Sut.UncommittedEvents.Single();
        var userCreatedEvent = uncommittedEvent.AggregateEvent.ShouldBeOfType<UserCreatedEvent>();
        userCreatedEvent.PhoneNumber.ShouldBe(phoneNumber);
        userCreatedEvent.UserId.ShouldBe(userId);
        userCreatedEvent.FirstName.ShouldBe(firstName);
        userCreatedEvent.AccessHash.ShouldBe(accessHash);
        userCreatedEvent.Bot.ShouldBeFalse();
    }

    [Fact]
    public void Create_User_With_Empty_FirstName_Throws_Exception()
    {
        Assert.Throws<DomainError>(() => Sut.Create(A<RequestInfo>(),
            1,
            0,
            "0",
            string.Empty));
    }
}