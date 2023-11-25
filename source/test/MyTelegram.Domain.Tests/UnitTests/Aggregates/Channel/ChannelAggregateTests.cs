using EventFlow.Aggregates;

namespace MyTelegram.Domain.Tests.UnitTests.Aggregates.Channel;

public class ChannelAggregateTests : TestsFor<ChannelAggregate>
{
    public ChannelAggregateTests()
    {
        Fixture.Customize<ChannelId>(x => x.FromFactory(() => ChannelId.Create(MyTelegramServerDomainConsts.ChannelInitId + 1)));
    }

    [Fact]
    public void EditAbout_For_Not_Exists_Channel_Throws_Exception()
    {
        Assert.Throws<DomainError>(() => Sut.EditAbout(A<RequestInfo>(), 1, "test"));
    }

    [Fact]
    public void EditAbout_Success_Test()
    {
        var about = "test about";
        var aggregateEvent = A<ChannelCreatedEvent>();
        var channelCreatedEvent = ADomainEvent<ChannelAggregate, ChannelId, ChannelCreatedEvent>(aggregateEvent, 1);
        Sut.ApplyEvents(new IDomainEvent[] { channelCreatedEvent });
        var requestInfo = A<RequestInfo>() with { UserId = aggregateEvent.CreatorId };

        Sut.EditAbout(requestInfo, aggregateEvent.CreatorId, about);

        var uncommittedEvent = Sut.UncommittedEvents.Single().AggregateEvent.ShouldBeOfType<ChannelAboutEditedEvent>();
        uncommittedEvent.About.ShouldBe(about);
    }

    [Fact]
    public void EditAbout_With_Text_Length_GreaterThan_ChatAbout_Max_Length_Throws_Exception()
    {
        var longAbout = string.Join("", Enumerable.Repeat("a", MyTelegramServerDomainConsts.ChatAboutMaxLength + 1));
        var aggregateEvent = A<ChannelCreatedEvent>();
        var channelCreatedEvent = ADomainEvent<ChannelAggregate, ChannelId, ChannelCreatedEvent>(aggregateEvent, 1);
        Sut.ApplyEvents(new IDomainEvent[] { channelCreatedEvent });
        var requestInfo = A<RequestInfo>() with { UserId = aggregateEvent.CreatorId };

        var exception = Assert.Throws<RpcException>(() => Sut.EditAbout(requestInfo, aggregateEvent.CreatorId, longAbout));

        exception.Message.ShouldBe(RpcErrors.RpcErrors400.ChatAboutTooLong.Message);
    }

    [Fact]
    public void Non_Admin_EditAbout_Throws_Exception()
    {
        var about = "test about";
        var aggregateEvent = A<ChannelCreatedEvent>();
        var channelCreatedEvent = ADomainEvent<ChannelAggregate, ChannelId, ChannelCreatedEvent>(aggregateEvent, 1);
        Sut.ApplyEvents(new IDomainEvent[] { channelCreatedEvent });

        var exception = Assert.Throws<RpcException>(() => Sut.EditAbout(A<RequestInfo>(), aggregateEvent.CreatorId + 1, about));

        exception.Message.ShouldBe(RpcErrors.RpcErrors400.ChatAdminRequired.Message);
    }

    [Fact]
    public void CheckChannelState_For_Not_Exists_Channel_Throws_Exception()
    {
        Assert.Throws<DomainError>(() => Sut.CheckChannelState(A<RequestInfo>(),
            1,
            1,
            0,
            MessageSubType.Normal));
    }

    [Fact]
    public void CheckChannelState_For_Broadcast_Channel_With_Non_Creator_Throws_Exception()
    {
        //var aggregateEvent = A<ChannelCreatedEvent>();
        //var domainEvent = ADomainEvent<ChannelAggregate, ChannelId, ChannelCreatedEvent>(aggregateEvent, 1);
        //Sut.ApplyEvents(new IDomainEvent[] { domainEvent });
        var creatorId = 1;
        var senderPeerId = 2;
        Sut.Create(A<RequestInfo>(),
            1,
            creatorId,
            true,
            false,
            "test",
            null,
            null,
            0,
            1,
            1,
            "test",
            0, false, null, null, null);

        var exception = Assert.Throws<RpcException>(() =>
            Sut.CheckChannelState(A<RequestInfo>(), senderPeerId,
                1,
                1,
                MessageSubType.Normal));

        exception.RpcError.ShouldBe(RpcErrors.RpcErrors403.ChatWriteForbidden);
    }

    [Fact]
    public void CheckChannelState_For_Banned_SendMessage_Rights_MegaGroup_Throws_Exception()
    {
        var creatorId = 1;
        var senderPeerId = 2;
        var requestInfo = A<RequestInfo>() with { UserId = creatorId };
        Sut.Create(requestInfo,
            1,
            creatorId,
            false,
            true,
            "test",
            null,
            null,
            1,
            1,
            1,
            "test",
            0, false, null, null, null);
        var bannedWriteMessageRights = new ChatBannedRights(false,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true, true, true, true, true, true, true, true,
            int.MaxValue);
        Sut.EditChatDefaultBannedRights(requestInfo, bannedWriteMessageRights, creatorId);

        var exception = Assert.Throws<RpcException>(() =>
            Sut.CheckChannelState(A<RequestInfo>(), senderPeerId,
                1,
                1,
                MessageSubType.Normal));

        exception.RpcError.ShouldBe(RpcErrors.RpcErrors403.ChatWriteForbidden);
    }

    [Fact]
    public void CheckChannelState_For_Enabled_Slow_Mode_Test()
    {
        var creatorId = 1;
        var senderPeerId = 2;
        Sut.Create(A<RequestInfo>(),
            1,
            creatorId,
            false,
            true,
            "test",
            null,
            null,
            1,
            1,
            1,
            "test",
            0,
            false, null, null, null);
        Sut.ToggleSlowMode(A<RequestInfo>(), 60, 1);
        var checkStateCompletedEvent = new CheckChannelStateCompletedEvent(
            A<RequestInfo>(),
            senderPeerId,
            1,
            DateTime.UtcNow.ToTimestamp(),
            false,
            null,
            new List<long>(),
            null);
        var domainEvent = ADomainEvent<ChannelAggregate, ChannelId, CheckChannelStateCompletedEvent>(checkStateCompletedEvent, 3);
        Sut.ApplyEvents(new IDomainEvent[] { domainEvent });

        var exception = Assert.Throws<RpcException>(() => Sut.CheckChannelState(A<RequestInfo>(), senderPeerId, 2, DateTime.UtcNow.ToTimestamp(), MessageSubType.Normal));

        exception.RpcError.Message.ShouldStartWith("SLOWMODE_WAIT_");
    }

    [Fact]
    public void CheckChannelState_Test()
    {
        var creatorId = 1;
        var senderPeerId = 2;
        var aggregateEvent = new ChannelCreatedEvent(A<RequestInfo>(),
            1,
            creatorId,
            "test",
            false,
            true,
            null,
            null,
            1,
            1,
            1,
            "test",
            0, false, null, null, null);
        Sut.ApplyEvents(new IDomainEvent[] { ADomainEvent<ChannelAggregate, ChannelId, ChannelCreatedEvent>(aggregateEvent, 1) });

        Sut.CheckChannelState(A<RequestInfo>(), senderPeerId, 1, DateTime.UtcNow.ToTimestamp(), MessageSubType.Normal);

        Sut.UncommittedEvents.Single().AggregateEvent.ShouldBeOfType<CheckChannelStateCompletedEvent>();
    }
}