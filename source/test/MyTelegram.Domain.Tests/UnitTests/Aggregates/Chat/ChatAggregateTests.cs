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

        var exception = Assert.Throws<UserFriendlyException>(() => Sut.Create(A<RequestInfo>(),
            A<long>(),
            creatorId,
            A<string>(),
            members,
            A<int>(),
            A<long>(),
            A<string>(),
            A<Guid>()));

        exception.Message.ShouldBe(RpcErrorMessages.UsersTooMuch);
    }

    [Fact]
    public void AddChatUser_Success()
    {
        var aggregateEvent = A<ChatCreatedEvent>();
        var domainEvent = ADomainEvent<ChatAggregate, ChatId, ChatCreatedEvent>(aggregateEvent, 1);
        Sut.ApplyEvents(new IDomainEvent[] { domainEvent });
        var inviterId = aggregateEvent.CreatorUid;
        var userId = A<long>();
        Sut.AddChatUser(A<RequestInfo>(),
            inviterId,
            userId,
            A<int>(),
            A<string>(),
            A<long>(),
            A<Guid>());

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

        var exception = Assert.Throws<UserFriendlyException>(() => Sut.AddChatUser(A<RequestInfo>(),
            inviterId,
            A<int>(),
            A<int>(),
            A<string>(),
            A<long>(),
            A<Guid>()));

        exception.Message.ShouldBe(RpcErrorMessages.ChatAdminRequired);
    }

    [Fact]
    public void AddChatUser_Exceeded_Max_Count_Throws_Exception()
    {
        var aggregateEvent = A<ChatCreatedEvent>();
        var domainEvent = ADomainEvent<ChatAggregate, ChatId, ChatCreatedEvent>(aggregateEvent, 1);
        Sut.ApplyEvents(new IDomainEvent[] { domainEvent });
        var inviterId = aggregateEvent.CreatorUid;
        var members = Enumerable.Range((int)inviterId + 1, MyTelegramServerDomainConsts.ChatMemberMaxCount);
        var aggregateEvents = members.Select(p => new ChatMemberAddedEvent(A<RequestInfo>(),
            aggregateEvent.ChatId,
            new ChatMember(p, aggregateEvent.CreatorUid, A<int>()),
            A<string>(),
            A<long>(),
            A<Guid>()));
        var domainEvents = aggregateEvents.Select((p,
            index) => ADomainEvent<ChatAggregate, ChatId, ChatMemberAddedEvent>(p, Sut.Version + index + 1)).ToList();
        Sut.ApplyEvents(domainEvents);

        var exception = Assert.Throws<UserFriendlyException>(() => Sut.AddChatUser(A<RequestInfo>(),
            inviterId,
            A<int>(),
            A<int>(),
            A<string>(),
            A<long>(),
            A<Guid>()));

        exception.Message.ShouldBe(RpcErrorMessages.UsersTooMuch);
    }

    [Fact]
    public void AddChatUser_For_Exists_Member_Throws_Exception()
    {
        var aggregateEvent = A<ChatCreatedEvent>();
        var domainEvent = ADomainEvent<ChatAggregate, ChatId, ChatCreatedEvent>(aggregateEvent, 1);
        Sut.ApplyEvents(new IDomainEvent[] { domainEvent });
        var memberAddedAggregateEvent = new ChatMemberAddedEvent(A<RequestInfo>(),
            aggregateEvent.ChatId,
            new ChatMember(A<long>(), aggregateEvent.CreatorUid, A<int>()),
            A<string>(),
            A<long>(),
            A<Guid>());
        var memberAddedDomainEvent =
            ADomainEvent<ChatAggregate, ChatId, ChatMemberAddedEvent>(memberAddedAggregateEvent, Sut.Version + 1);
        Sut.ApplyEvents(new IDomainEvent[] { memberAddedDomainEvent });

        var exception = Assert.Throws<UserFriendlyException>(() => Sut.AddChatUser(A<RequestInfo>(),
             aggregateEvent.CreatorUid,
             memberAddedAggregateEvent.ChatMember.UserId,
             A<int>(),
             A<string>(),
             A<long>(),
             A<Guid>()));

        exception.Message.ShouldBe(RpcErrorMessages.UserAlreadyParticipant);
    }
}