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
        Assert.Throws<DomainError>(() => Sut.EditAbout(0, 1, "test"));
    }

    [Fact]
    public void EditAbout_Success_Test()
    {
        var about = "test about";
        var aggregateEvent = A<ChannelCreatedEvent>();
        var channelCreatedEvent = ADomainEvent<ChannelAggregate, ChannelId, ChannelCreatedEvent>(aggregateEvent, 1);
        Sut.ApplyEvents(new IDomainEvent[] { channelCreatedEvent });

        Sut.EditAbout(0, aggregateEvent.CreatorId, about);

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

        var exception = Assert.Throws<UserFriendlyException>(() => Sut.EditAbout(0, aggregateEvent.CreatorId, longAbout));

        exception.Message.ShouldBe(RpcErrorMessages.ChatAboutTooLong);
    }

    [Fact]
    public void Non_Admin_EditAbout_Throws_Exception()
    {
        var about = "test about";
        var aggregateEvent = A<ChannelCreatedEvent>();
        var channelCreatedEvent = ADomainEvent<ChannelAggregate, ChannelId, ChannelCreatedEvent>(aggregateEvent, 1);
        Sut.ApplyEvents(new IDomainEvent[] { channelCreatedEvent });

        var exception = Assert.Throws<UserFriendlyException>(() => Sut.EditAbout(0, aggregateEvent.CreatorId + 1, about));

        exception.Message.ShouldBe(RpcErrorMessages.ChatAdminRequired);
    }

    [Fact]
    public void CheckChannelState_For_Not_Exists_Channel_Throws_Exception()
    {
        Assert.Throws<DomainError>(() => Sut.CheckChannelState(1,
            1,
            1,
            MessageSubType.Normal,
            Guid.Empty));
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
            Guid.Empty);

        var exception = Assert.Throws<UserFriendlyException>(() =>
            Sut.CheckChannelState(senderPeerId,
                1,
                1,
                MessageSubType.Normal,
                Guid.Empty));

        exception.Message.ShouldBe(RpcErrorMessages.ChatWriteForbidden);
    }

    [Fact]
    public void CheckChannelState_For_Banned_SendMessage_Rights_MegaGroup_Throws_Exception()
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
            Guid.Empty);
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
            int.MaxValue);
        Sut.EditChatDefaultBannedRights(1, bannedWriteMessageRights, creatorId);

        var exception = Assert.Throws<UserFriendlyException>(() =>
            Sut.CheckChannelState(senderPeerId,
                1,
                1,
                MessageSubType.Normal,
                Guid.Empty));

        exception.Message.ShouldBe(RpcErrorMessages.ChatWriteForbidden);
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
            Guid.Empty);
        Sut.ToggleSlowMode(1, 60, 1);
        var checkStateCompletedEvent = new CheckChannelStateCompletedEvent(senderPeerId,
            1,
            DateTime.UtcNow.ToTimestamp(),
            false,
            null,
            new List<long>(),
            null,
            Guid.Empty);
        var domainEvent = ADomainEvent<ChannelAggregate, ChannelId, CheckChannelStateCompletedEvent>(checkStateCompletedEvent, 3);
        Sut.ApplyEvents(new IDomainEvent[] { domainEvent });

        var exception = Assert.Throws<UserFriendlyException>(() => Sut.CheckChannelState(senderPeerId, 2, DateTime.UtcNow.ToTimestamp(), MessageSubType.Normal, Guid.Empty));

        exception.Message.ShouldStartWith("SLOWMODE_WAIT_");
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
            Guid.Empty);
        Sut.ApplyEvents(new IDomainEvent[] { ADomainEvent<ChannelAggregate, ChannelId, ChannelCreatedEvent>(aggregateEvent, 1) });

        Sut.CheckChannelState(senderPeerId, 1, DateTime.UtcNow.ToTimestamp(), MessageSubType.Normal, Guid.Empty);

        Sut.UncommittedEvents.Single().AggregateEvent.ShouldBeOfType<CheckChannelStateCompletedEvent>();
    }
}