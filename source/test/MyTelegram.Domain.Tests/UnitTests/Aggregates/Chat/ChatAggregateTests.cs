namespace MyTelegram.Domain.Tests.UnitTests.Aggregates.Chat;

public class ChatAggregateTests : TestsFor<ChatAggregate>
{
    public ChatAggregateTests()
    {
        Fixture.Customize<ChatId>(x => x.FromFactory(() => ChatId.Create(MyTelegramServerDomainConsts.ChatIdInitId + 1)));
    }

    [Fact]
    public void CreateChat_With_TooMany_Members_Throws_Exception()
    {
        var members = Enumerable.Range(1, MyTelegramServerDomainConsts.ChatMemberMaxCount + 1).Select(p => (long)p).ToList();
        var creatorId = MyTelegramServerDomainConsts.ChatMemberMaxCount + 1;

        var exception = Assert.Throws<RpcException>(() => Sut.Create(A<RequestInfo>(),
            A<long>(),
            creatorId,
            A<string>(),
            members,
            A<int>(),
            A<long>(),
            A<string>(), 0));

        exception.Message.ShouldBe(RpcErrors.RpcErrors400.UsersTooMuch.Message);
    }

    [Fact]
    public void AddChatUser_Success()
    {
        var aggregateEvent = A<ChatCreatedEvent>();
        var domainEvent = ADomainEvent<ChatAggregate, ChatId, ChatCreatedEvent>(aggregateEvent, 1);
        Sut.ApplyEvents(new IDomainEvent[] { domainEvent });
        var inviterId = aggregateEvent.CreatorUid;
        var userId = A<long>();
        Sut.AddChatUser(A<RequestInfo>() with { UserId = inviterId },
            inviterId,
            userId,
            A<int>(),
            A<string>(),
            A<long>());

        var uncommittedEvent = Sut.UncommittedEvents.Single().AggregateEvent.ShouldBeOfType<ChatMemberAddedEvent>();
        uncommittedEvent.ChatMember.UserId.ShouldBe(userId);
    }

    [Fact]
    public void Non_Admin_AddChatUser_Throws_Exception()
    {
        var aggregateEvent = A<ChatCreatedEvent>();
        var domainEvent = ADomainEvent<ChatAggregate, ChatId, ChatCreatedEvent>(aggregateEvent, 1);
        Sut.ApplyEvents(new IDomainEvent[] { domainEvent });
        var inviterId = aggregateEvent.CreatorUid + 1;
        var requestInfo = A<RequestInfo>() with { UserId = inviterId };

        var exception = Assert.Throws<RpcException>(() => Sut.AddChatUser(requestInfo,
            inviterId,
            A<int>(),
            A<int>(),
            A<string>(),
            A<long>()));

        exception.RpcError.ShouldBe(RpcErrors.RpcErrors400.ChatAdminRequired);
    }

    [Fact]
    public void AddChatUser_Exceeded_Max_Count_Throws_Exception()
    {
        var aggregateEvent = A<ChatCreatedEvent>();
        var domainEvent = ADomainEvent<ChatAggregate, ChatId, ChatCreatedEvent>(aggregateEvent, 1);
        Sut.ApplyEvents(new IDomainEvent[] { domainEvent });
        var inviterId = aggregateEvent.CreatorUid;
        var requestInfo = A<RequestInfo>() with { UserId = aggregateEvent.CreatorUid };
        var members = Enumerable.Range((int)inviterId + 1, MyTelegramServerDomainConsts.ChatMemberMaxCount);
        var aggregateEvents = members.Select(p => new ChatMemberAddedEvent(
            requestInfo,
            aggregateEvent.ChatId,
            new ChatMember(p, aggregateEvent.CreatorUid, A<int>()),
            A<string>(),
            A<long>(), new List<long>()));
        var domainEvents = aggregateEvents.Select((p,
            index) => ADomainEvent<ChatAggregate, ChatId, ChatMemberAddedEvent>(p, Sut.Version + index + 1)).ToList();
        Sut.ApplyEvents(domainEvents);

        var exception = Assert.Throws<RpcException>(() => Sut.AddChatUser(requestInfo,
            inviterId,
            A<int>(),
            A<int>(),
            A<string>(),
            A<long>()));

        exception.Message.ShouldBe(RpcErrors.RpcErrors400.UsersTooMuch.Message);
    }

    [Fact]
    public void AddChatUser_For_Exists_Member_Throws_Exception()
    {
        var aggregateEvent = A<ChatCreatedEvent>();
        var domainEvent = ADomainEvent<ChatAggregate, ChatId, ChatCreatedEvent>(aggregateEvent, 1);
        Sut.ApplyEvents(new IDomainEvent[] { domainEvent });
        var memberAddedAggregateEvent = new ChatMemberAddedEvent(A<RequestInfo>(),
            aggregateEvent.ChatId,
            new ChatMember(A<long>(),
                aggregateEvent.CreatorUid,
                A<int>()),
            A<string>(),
            A<long>(), Many<long>());
        var memberAddedDomainEvent =
            ADomainEvent<ChatAggregate, ChatId, ChatMemberAddedEvent>(memberAddedAggregateEvent, Sut.Version + 1);
        Sut.ApplyEvents(new IDomainEvent[] { memberAddedDomainEvent });
        var requestInfo = A<RequestInfo>() with { UserId = aggregateEvent.CreatorUid };

        var exception = Assert.Throws<RpcException>(() => Sut.AddChatUser(requestInfo,
             aggregateEvent.CreatorUid,
             memberAddedAggregateEvent.ChatMember.UserId,
             A<int>(),
             A<string>(),
             A<long>()));

        exception.Message.ShouldBe(RpcErrors.RpcErrors400.UserAlreadyParticipant.Message);
    }
}