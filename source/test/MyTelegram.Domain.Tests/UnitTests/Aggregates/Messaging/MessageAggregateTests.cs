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
            null,
            Guid.Empty);

        var uncommittedEvent = Sut.UncommittedEvents.Single().AggregateEvent.ShouldBeOfType<OutboxMessageEditedEvent>();
        uncommittedEvent.NewMessage.ShouldBe(newMessage);
    }

    [Fact]
    public void Edit_Out_Of_Date_Message_Throws_Exception()
    {
        var newMessage = "new message";
        var creationTime = DateTime.UtcNow.ToTimestamp() - MyTelegramServerDomainConsts.EditTimeLimit - 1000;
        CreateMessage(creationTime);

        var exception = Assert.Throws<UserFriendlyException>(() =>
            Sut.EditOutboxMessage(A<RequestInfo>(),
                _messageId,
                newMessage,
                DateTime.UtcNow.ToTimestamp(),
                null,
                null,
                Guid.Empty));

        exception.Message.ShouldBe(RpcErrorMessages.MessageEditTimeExpired);
    }

    [Fact(DisplayName = "Only message author can edit message")]
    public void Edit_Inbox_Message_Throws_Exception()
    {
        var newMessage = "new message";
        CreateMessage(DateTime.UtcNow.ToTimestamp(), false);

        var exception = Assert.Throws<UserFriendlyException>(() =>
            Sut.EditOutboxMessage(A<RequestInfo>(),
                _messageId,
                newMessage,
                DateTime.UtcNow.ToTimestamp(),
                null,
                null,
                Guid.Empty));

        exception.Message.ShouldBe(RpcErrorMessages.MessageAuthorRequired);
    }

    private void CreateMessage(int creationDate, bool isOut = true)
    {
        var ownerPeerId = 1;
        var messageItem = new MessageItem(
            new Peer(PeerType.User, ownerPeerId),
            new Peer(PeerType.User, 10),
            new Peer(PeerType.User, ownerPeerId),
            _messageId,
            "test message",
            creationDate,
            1,
            isOut
        );
        var outboxMessageCreatedEvent = new OutboxMessageCreatedEvent(0,
            messageItem,
            true,
            1,
            Guid.NewGuid());

        Sut.ApplyEvents(new IDomainEvent[] { ADomainEvent<MessageAggregate, MessageId, OutboxMessageCreatedEvent>(outboxMessageCreatedEvent, 1) });
    }
}