namespace MyTelegram.Domain.Tests.UnitTests.Aggregates.Messaging;

public class MessageAggregateTests : TestsFor<MessageAggregate>
{
    public MessageAggregateTests()
    {
        Fixture.Customize<MessageId>(c => c.FromFactory(() => MessageId.Create(1, 1)));
    }

    //[Fact]
    //public void Send_Reaction_To_Self_Message_Success()
    //{
    //    var item = CreatePrivateMessage();
    //    var reaction = new Reaction(item.SenderPeer.PeerId, null, 1);

    //    Sut.UpdateMessageReactions(A<RequestInfo>(), item.SenderPeer.PeerId, new List<Reaction> { reaction });

    //    var uncommittedEvent =
    //        Sut.UncommittedEvents.Single().AggregateEvent.ShouldBeOfType<MessageReactionsUpdatedEvent>();
    //    uncommittedEvent.AllReactions.ShouldNotBeNull();
    //    uncommittedEvent.AllReactions.Count.ShouldBe(1);
    //}

    //[Fact]
    //public void Send_Reaction_To_Channel_Message_Success()
    //{
    //    //var channelId = MyTelegramConsts.ChannelInitId + 1;
    //    //var item = CreateMessage(DateTime.UtcNow.ToTimestamp(), channelId, true);

    //    // Arrange
    //    var item = CreateChannelMessage(isOut: false);
    //    var userId1 = A<long>();
    //    var userId2 = A<long>();
    //    var userId3 = A<long>();
    //    var userId4 = A<long>();
    //    var userId5 = A<long>();

    //    var reaction = new Reaction(A<long>(), null, 1);
    //    var reaction2 = new Reaction(A<long>(), null, 2);
    //    var reaction3 = new Reaction(A<long>(), null, 3);

    //    // Act
    //    Sut.UpdateMessageReactions(A<RequestInfo>(), A<long>(), [reaction with { UserId = userId1 }]);
    //    Sut.UpdateMessageReactions(A<RequestInfo>(), A<long>(), [reaction with { UserId = userId2 }]);
    //    Sut.UpdateMessageReactions(A<RequestInfo>(), A<long>(), [reaction with { UserId = userId3 }]);
    //    Sut.UpdateMessageReactions(A<RequestInfo>(), A<long>(), [reaction with { UserId = userId4 }]);
    //    Sut.UpdateMessageReactions(A<RequestInfo>(), A<long>(), [reaction with { UserId = userId5 }]);

    //    Sut.UpdateMessageReactions(A<RequestInfo>(), A<long>(), [reaction2 with { UserId = userId1 }]);
    //    Sut.UpdateMessageReactions(A<RequestInfo>(), A<long>(), [reaction2 with { UserId = userId2 }]);
    //    Sut.UpdateMessageReactions(A<RequestInfo>(), A<long>(), [reaction2 with { UserId = userId3 }]);
    //    Sut.UpdateMessageReactions(A<RequestInfo>(), A<long>(), [reaction2 with { UserId = userId4 }]);
    //    Sut.UpdateMessageReactions(A<RequestInfo>(), A<long>(), [reaction2 with { UserId = userId5 }]);

    //    Sut.UpdateMessageReactions(A<RequestInfo>(), A<long>(), [reaction3 with { UserId = userId1 }]);
    //    Sut.UpdateMessageReactions(A<RequestInfo>(), A<long>(), [reaction3 with { UserId = userId2 }]);
    //    Sut.UpdateMessageReactions(A<RequestInfo>(), A<long>(), [reaction3 with { UserId = userId3 }]);
    //    Sut.UpdateMessageReactions(A<RequestInfo>(), A<long>(), [reaction3 with { UserId = userId4 }]);
    //    Sut.UpdateMessageReactions(A<RequestInfo>(), A<long>(), [reaction3 with { UserId = userId5 }]);

    //    // Assert
    //    var uncommittedEvents =
    //        Sut.UncommittedEvents.ToList();
    //    var lastEvent = uncommittedEvents.Last().AggregateEvent.ShouldBeOfType<MessageReactionsUpdatedEvent>();

    //    uncommittedEvents.Count.ShouldBe(15);
    //    lastEvent.AllReactions.ShouldNotBeNull();
    //    lastEvent.AllReactions.Count.ShouldBe(3);
    //    var reactionCount = lastEvent.AllReactions[0];
    //    reactionCount.Count.ShouldBe(5);

    //}

    //[Fact]
    //public void Reaction_Send_Inbox_Reaction_Success()
    //{
    //    var item = CreateMessage(DateTime.UtcNow.ToTimestamp());
    //    var reaction = new Reaction(item.SenderPeer.PeerId, null, 1);
    //    var reaction2 = new Reaction(item.ToPeer.PeerId, null, 1);
    //    Sut.UpdateMessageReactions(A<RequestInfo>(), item.SenderPeer.PeerId, new List<Reaction> { reaction }, A<Guid>());

    //    Sut.UpdateInboxMessageReactions(item.ToPeer.PeerId, new List<Reaction> { reaction2 }, A<Guid>());

    //    var uncommittedEvent =
    //        Sut.UncommittedEvents.Last().AggregateEvent.ShouldBeOfType<InboxMessageReactionsUpdatedEvent>();
    //    uncommittedEvent.AllReactions.ShouldNotBeNull();
    //    uncommittedEvent.AllReactions.Count.ShouldBe(2);
    //}

    //[Fact]
    //public void Clear_Reaction_Success()
    //{
    //    var item = CreatePrivateMessage();
    //    var reaction = new Reaction(item.SenderPeer.PeerId, null, 1);
    //    Sut.UpdateMessageReactions(A<RequestInfo>(), item.SenderPeer.PeerId, new List<Reaction> { reaction });

    //    Sut.UpdateMessageReactions(A<RequestInfo>(), item.SenderPeer.PeerId, null);

    //    var uncommittedEvent =
    //        Sut.UncommittedEvents.Last().AggregateEvent.ShouldBeOfType<MessageReactionsUpdatedEvent>();
    //    uncommittedEvent.AllReactions.ShouldBeNull();
    //}

    [Fact]
    public void Edit_Message_Success()
    {
        var newMessage = "new message";
        var item = CreatePrivateMessage();

        Sut.EditOutboxMessage(A<RequestInfo>(),
            item.MessageId,
            newMessage,
            DateTime.UtcNow.ToTimestamp(),
            null,
            null,
            null,
            false, null, null);

        var uncommittedEvent = Sut.UncommittedEvents.Single().AggregateEvent.ShouldBeOfType<OutboxMessageEditedEventV2>();
        uncommittedEvent.NewMessageItem.Message.ShouldBe(newMessage);
    }

    [Fact]
    public void Edit_Out_Of_Date_Message_Throws_Exception()
    {
        var newMessage = "new message";
        var creationTime = DateTime.UtcNow.ToTimestamp() - MyTelegramConsts.EditTimeLimit - 1000;
        var item = CreatePrivateMessage(creationTime);

        var exception = Assert.Throws<RpcException>(() =>
            Sut.EditOutboxMessage(A<RequestInfo>(),
                item.MessageId,
                newMessage,
                DateTime.UtcNow.ToTimestamp(),
                null,
                null,
                null,
                false, null, null));

        exception.Message.ShouldBe(RpcErrors.RpcErrors400.MessageEditTimeExpired.Message);
    }

    [Fact(DisplayName = "Only message author can edit message")]
    public void Edit_Inbox_Message_Throws_Exception()
    {
        var newMessage = A<string>();
        //CreateMessage(DateTime.UtcNow.ToTimestamp(), 1, false);
        var item = CreatePrivateMessage(isOut: false);

        var exception = Assert.Throws<RpcException>(() =>
            Sut.EditOutboxMessage(A<RequestInfo>(),
                item.MessageId,
                newMessage,
                DateTime.UtcNow.ToTimestamp(),
                null,
                null,
                null,
                false, null, null));

        exception.Message.ShouldBe(RpcErrors.RpcErrors403.MessageAuthorRequired.Message);
    }

    private MessageItem CreateChannelMessage(int creationTime = 0, bool isOut = true)
    {
        if (creationTime == 0)
        {
            creationTime = DateTime.UtcNow.ToTimestamp();
        }

        var ownerPeer = A<long>().ToChannelPeer();
        var toPeer = ownerPeer;
        var senderPeer = A<long>().ToUserPeer();

        return CreateMessage(creationTime, ownerPeer, toPeer, senderPeer, isOut);
    }

    private MessageItem CreatePrivateMessage(int creationTime = 0, bool isOut = true)
    {
        if (creationTime == 0)
        {
            creationTime = DateTime.UtcNow.ToTimestamp();
        }

        var ownerPeer = A<long>().ToUserPeer();
        var toPeer = A<long>().ToUserPeer();

        return CreateMessage(creationTime, ownerPeer, toPeer, ownerPeer, isOut);
    }

    private MessageItem CreateMessage(int creationDate, Peer ownerPeer, Peer toPeer, Peer senderPeer, bool isOut = true)
    {
        var messageItem = new MessageItem(
            ownerPeer,
            toPeer,
            senderPeer,
            senderPeer.PeerId,
            A<int>(),
            "test message",
            creationDate,
            1,
            isOut
        );
        var outboxMessageCreatedEvent = new OutboxMessageCreatedEvent(A<RequestInfo>(),
            messageItem,
            null, null,
            true,
            1,
            null, null);

        Sut.ApplyEvents([ADomainEvent<MessageAggregate, MessageId, OutboxMessageCreatedEvent>(outboxMessageCreatedEvent, 1)
        ]);
        return messageItem;
    }
}