using Shouldly;

namespace MyTelegram.Domain.Tests.UnitTests.Aggregates.AppCode;

public class AppCodeAggregateTests : TestsFor<AppCodeAggregate>
{
    private readonly string _inValidPhoneCodeHash = "2";
    private readonly int _maxFailedCount = 5;
    private readonly string _phoneNumber = "0";
    private readonly string _validPhoneCodeHash = "1";
    public AppCodeAggregateTests()
    {
        Fixture.Customize<AppCodeId>(c => c.FromFactory(() => AppCodeId.Create(_phoneNumber, _validPhoneCodeHash)));
    }

    [Fact]
    public void Cancel_AppCode_Success()
    {
        CreateAppCodeAggregate();

        Sut.CancelCode(A<RequestInfo>(), _phoneNumber, _validPhoneCodeHash);

        Sut.UncommittedEvents.Single().AggregateEvent.ShouldBeOfType<AppCodeCanceledEvent>();
    }

    [Fact]
    public void CheckSignInCode_Exceeded_Max_Failed_Count_Throws_Exception()
    {
        CreateAppCodeAggregate();
        var aggregateEvent = new CheckSignInCodeCompletedEvent(A<RequestInfo>(), false, 1);
        var aggregateEvents = Enumerable.Repeat(aggregateEvent, _maxFailedCount + 1);
        var domainEvents = aggregateEvents.Select((p,
                index) => ADomainEvent<AppCodeAggregate, AppCodeId, CheckSignInCodeCompletedEvent>(p, Sut.Version + index + 1))
            .ToList();
        Sut.ApplyEvents(domainEvents);

        var exception = Assert.Throws<UserFriendlyException>(() => Sut.CheckSignInCode(A<RequestInfo>(), _validPhoneCodeHash, A<long>()));

        exception.Message.ShouldBe(RpcErrors.RpcErrors400.PhoneCodeInvalid.Message);
    }

    [Fact]
    public void CheckSignInCode_With_Canceled_Code_Throws_Exception()
    {
        CreateAppCodeAggregate();
        var domainEvent = ADomainEvent<AppCodeAggregate, AppCodeId, AppCodeCanceledEvent>(Sut.Version + 1);
        Sut.ApplyEvents(new IDomainEvent[] { domainEvent });

        var exception = Assert.Throws<UserFriendlyException>(() => Sut.CheckSignInCode(A<RequestInfo>(), _validPhoneCodeHash, A<long>()));

        exception.Message.ShouldBe(RpcErrors.RpcErrors400.PhoneCodeExpired.Message);
    }

    [Fact]
    public void CheckSignInCode_With_Correct_PhoneCode()
    {
        CreateAppCodeAggregate();

        Sut.CheckSignInCode(A<RequestInfo>(), _validPhoneCodeHash, A<long>());

        Sut.UncommittedEvents.Single().AggregateEvent.ShouldBeOfType<CheckSignInCodeCompletedEvent>();
    }

    [Fact]
    public void CheckSignInCode_With_Empty_PhoneCode_Throws_Exception()
    {
        CreateAppCodeAggregate(expireMinutes: -5);

        var exception = Assert.Throws<UserFriendlyException>(() => Sut.CheckSignInCode(A<RequestInfo>(), string.Empty, A<long>()));

        exception.Message.ShouldBe(RpcErrors.RpcErrors400.PhoneCodeEmpty.Message);
    }

    [Fact]
    public void CheckSignInCode_With_Expire_PhoneCode_Throws_Exception()
    {
        CreateAppCodeAggregate(expireMinutes: -5);

        var exception = Assert.Throws<UserFriendlyException>(() => Sut.CheckSignInCode(A<RequestInfo>(), _validPhoneCodeHash, A<long>()));

        exception.Message.ShouldBe(RpcErrors.RpcErrors400.PhoneCodeExpired.Message);
    }

    [Fact]
    public void CheckSignInCode_With_Invalid_PhoneCode()
    {
        CreateAppCodeAggregate();

        Sut.CheckSignInCode(A<RequestInfo>(), _inValidPhoneCodeHash, A<long>());

        var checkSignUpCodeCompletedEvent = Sut.UncommittedEvents.Single().AggregateEvent.ShouldBeOfType<CheckSignInCodeCompletedEvent>();
        checkSignUpCodeCompletedEvent.IsCodeValid.ShouldBeFalse();
    }

    //[Fact]
    //public void CheckSignUpCode_Exceeded_Max_Failed_Count_Throws_Exception()
    //{
    //    CreateAppCodeAggregate();
    //    var aggregateEvent = new CheckSignUpCodeCompletedEvent(A<RequestInfo>(), false, 1, 0, "0", "0", null, Guid.Empty);
    //    var aggregateEvents = Enumerable.Repeat(aggregateEvent, _maxFailedCount + 1);
    //    var domainEvents = aggregateEvents.Select((p,
    //            index) => ADomainEvent<AppCodeAggregate, AppCodeId, CheckSignUpCodeCompletedEvent>(p, Sut.Version + index + 1))
    //        .ToList();
    //    Sut.ApplyEvents(domainEvents);

    //    var exception = Assert.Throws<UserFriendlyException>(() => Sut.CheckSignUpCode(A<RequestInfo>(),
    //        0,
    //        _validPhoneCodeHash,
    //        0,
    //        _phoneNumber,
    //        "0",
    //        null,
    //        Guid.Empty));

    //    exception.Message.ShouldBe(RpcErrorMessages.PhoneCodeInvalid);
    //}

    //[Fact]
    //public void CheckSignUpCode_With_Canceled_Code_Throws_Exception()
    //{
    //    CreateAppCodeAggregate();
    //    var domainEvent = ADomainEvent<AppCodeAggregate, AppCodeId, AppCodeCanceledEvent>(Sut.Version + 1);
    //    Sut.ApplyEvents(new IDomainEvent[] { domainEvent });

    //    var exception = Assert.Throws<UserFriendlyException>(() => Sut.CheckSignUpCode(A<RequestInfo>(),
    //        0,
    //        _validPhoneCodeHash,
    //        0,
    //        _phoneNumber,
    //        "0",
    //        null,
    //        Guid.Empty));

    //    exception.Message.ShouldBe(RpcErrorMessages.PhoneCodeExpired);
    //}

    [Fact]
    public void CheckSignUpCode_With_Correct_PhoneCode_For_New_User()
    {
        CreateAppCodeAggregate();

        Sut.CheckSignUpCode(
            A<RequestInfo>(),
            0, _validPhoneCodeHash, 0, _phoneNumber, "0", null);

        //Sut.UncommittedEvents.Single().AggregateEvent.ShouldBeOfType<SignUpRequiredEvent>();
        var uncommittedEvent = Sut.UncommittedEvents.Single().AggregateEvent.ShouldBeOfType<CheckSignUpCodeCompletedEvent>();
        uncommittedEvent.IsCodeValid.ShouldBeTrue();
    }

    [Fact]
    public void CheckSignUpCode_With_Correct_PhoneCode_For_Old_User()
    {
        var oldUid = 1;
        CreateAppCodeAggregate(oldUid);

        Sut.CheckSignUpCode(A<RequestInfo>(), oldUid, _validPhoneCodeHash, 0, _phoneNumber, "0", null);

        var checkSignUpCodeCompletedEvent = Sut.UncommittedEvents.Single().AggregateEvent.ShouldBeOfType<CheckSignUpCodeCompletedEvent>();
        checkSignUpCodeCompletedEvent.IsCodeValid.ShouldBeTrue();
        checkSignUpCodeCompletedEvent.UserId.ShouldBe(oldUid);
        checkSignUpCodeCompletedEvent.PhoneNumber.ShouldBe(_phoneNumber);
    }

    //[Fact]
    //public void CheckSignUpCode_With_Empty_PhoneCode_Throws_Exception()
    //{
    //    CreateAppCodeAggregate(expireMinutes: -5);

    //    var exception = Assert.Throws<UserFriendlyException>(() => Sut.CheckSignUpCode(A<RequestInfo>(),
    //        0,
    //        string.Empty,
    //        0,
    //        _phoneNumber,
    //        "0",
    //        null,
    //        Guid.Empty));

    //    exception.Message.ShouldBe(RpcErrorMessages.PhoneCodeEmpty);
    //}

    //[Fact]
    //public void CheckSignUpCode_With_Expire_PhoneCode_Throws_Exception()
    //{
    //    CreateAppCodeAggregate(expireMinutes: -5);

    //    var exception = Assert.Throws<UserFriendlyException>(() => Sut.CheckSignUpCode(A<RequestInfo>(),
    //        0,
    //        _validPhoneCodeHash,
    //        0,
    //        _phoneNumber,
    //        "0",
    //        null,
    //        Guid.Empty));

    //    exception.Message.ShouldBe(RpcErrorMessages.PhoneCodeExpired);
    //}

    //[Fact]
    //public void CheckSignUpCode_With_Invalid_PhoneCode()
    //{
    //    CreateAppCodeAggregate();

    //    Sut.CheckSignUpCode(A<RequestInfo>(), 0, _inValidPhoneCodeHash, 0, _phoneNumber, "0", null, Guid.Empty);

    //    var checkSignUpCodeCompletedEvent = Sut.UncommittedEvents.Single().AggregateEvent.ShouldBeOfType<CheckSignUpCodeCompletedEvent>();
    //    checkSignUpCodeCompletedEvent.IsCodeValid.ShouldBeFalse();
    //}

    [Fact]
    public void CreateAppCode_Success()
    {
        Sut.Create(A<RequestInfo>(), A<long>(), A<string>(), A<string>(), A<int>(), A<string>(), A<long>());

        Sut.UncommittedEvents.Single().AggregateEvent.ShouldBeOfType<AppCodeCreatedEvent>();
    }

    private void CreateAppCodeAggregate(long uid = 0, int expireMinutes = 5)
    {
        var aggregateId = A<AppCodeId>();
        var appCodeCreatedEvent = new AppCodeCreatedEvent(A<RequestInfo>(),
            uid,
            _phoneNumber,
            "0",
            DateTime.UtcNow.AddMinutes(expireMinutes).ToTimestamp(),
            _validPhoneCodeHash,
            0);

        Sut.ApplyEvents(new IDomainEvent[]
        {
            new DomainEvent<AppCodeAggregate, AppCodeId, AppCodeCreatedEvent>(appCodeCreatedEvent,
                Metadata.Empty,
                DateTimeOffset.UtcNow,
                aggregateId,
                1)
        });
    }
}
