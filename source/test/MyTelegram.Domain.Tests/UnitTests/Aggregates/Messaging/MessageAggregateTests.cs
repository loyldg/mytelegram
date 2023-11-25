using Shouldly;

namespace MyTelegram.Domain.Tests.UnitTests.Aggregates.Messaging;

public class MessageAggregateTests : TestsFor<MessageAggregate>
{
    private int _messageId = 1;
    public MessageAggregateTests()
    {
        Fixture.Customize<MessageId>(c => c.FromFactory(() => MessageId.Create(1, 1)));
    }

    [Fact]
    public void Edit_Message_Success()
    {
        var messageId = 1;
        var newMessage = "new message";
        CreateMessage(DateTime.UtcNow.ToTimestamp());

        Sut.EditOutboxMessage(A<RequestInfo>(),
            messageId,
            newMessage,
            DateTime.UtcNow.ToTimestamp(),
            null,
            null);

        var uncommittedEvent = Sut.UncommittedEvents.Single().AggregateEvent.ShouldBeOfType<OutboxMessageEditedEvent>();
        uncommittedEvent.NewMessage.ShouldBe(newMessage);
    }

    [Fact]
    public void Edit_Out_Of_Date_Message_Throws_Exception()
    {
        var newMessage = "new message";
        var creationTime = DateTime.UtcNow.ToTimestamp() - MyTelegramServerDomainConsts.EditTimeLimit - 1000;
        CreateMessage(creationTime);

        var exception = Assert.Throws<RpcException>(() =>
            Sut.EditOutboxMessage(A<RequestInfo>(),
                _messageId,
                newMessage,
                DateTime.UtcNow.ToTimestamp(),
                null,
                null));

        exception.Message.ShouldBe(RpcErrors.RpcErrors400.MessageEditTimeExpired.Message);
    }

    [Fact(DisplayName = "Only message author can edit message")]
    public void Edit_Inbox_Message_Throws_Exception()
    {
        var newMessage = "new message";
        CreateMessage(DateTime.UtcNow.ToTimestamp(), false);

        var exception = Assert.Throws<RpcException>(() =>
            Sut.EditOutboxMessage(A<RequestInfo>(),
                _messageId,
                newMessage,
                DateTime.UtcNow.ToTimestamp(),
                null,
                null));

        exception.Message.ShouldBe(RpcErrors.RpcErrors403.MessageAuthorRequired.Message);
    }

    private MessageItem CreateMessage(int creationDate, bool isOut = true)
    {
        var ownerPeerId = 1;
        var toPeerUserId = A<long>();
        //var aggregateId = MessageId.Create(ownerPeerId, _messageId);
        var messageItem = new MessageItem(
            new Peer(PeerType.User, ownerPeerId),
            new Peer(PeerType.User, toPeerUserId),
            new Peer(PeerType.User, ownerPeerId),
            _messageId,
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

        Sut.ApplyEvents(new IDomainEvent[] { ADomainEvent<MessageAggregate, MessageId, OutboxMessageCreatedEvent>(outboxMessageCreatedEvent, 1) });
        return messageItem;
    }
}