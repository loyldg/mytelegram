namespace MyTelegram.Domain.Tests.UnitTests.Sagas;

public class MessageSagaTests : TestsFor<MessageSaga>
{
    //private Mock<ICommandBus> _commandBus;
    private readonly Mock<ISagaContext> _sagaContext;
    private int _messageId = 1;

    public MessageSagaTests()
    {
        Fixture.Customize<MessageSagaId>(c => c.FromFactory(() => new MessageSagaId($"messagesaga-{Guid.Empty}")));
        //_commandBus = InjectMock<ICommandBus>();
        _sagaContext = InjectMock<ISagaContext>();
    }

    [Theory]
    [MemberData(nameof(GetSendMessageData))]
    public async Task SendMessage_Started_Test(Peer fromPeer, Peer toPeer)
    {
        var aggregateId = MessageId.Create(fromPeer.PeerId, _messageId);
        var messageItem = new MessageItem(aggregateId,
            fromPeer,
            toPeer,
            fromPeer,
            _messageId,
            "test message",
            A<int>(),
            A<long>(),
            true);
        var aggregateEvent = new SendMessageStartedEvent(A<RequestInfo>(),
            messageItem,
            true,
            1,
            Guid.NewGuid()
        );
        var domainEvent = ADomainEvent<MessageAggregate, MessageId, SendMessageStartedEvent>(aggregateEvent, aggregateId, 1);

        await Sut.HandleAsync(domainEvent, _sagaContext.Object, CancellationToken.None);

        var uncommittedEvent = Sut.UncommittedEvents.Single().AggregateEvent.ShouldBeOfType<MessageSagaStartedEvent>();
        uncommittedEvent.MessageItem.ToPeer.ShouldBe(toPeer);
    }

    //[Fact]
    //public async Task SendMessage_To_User_Peer_Started_Test()
    //{
    //    var fromPeer = new Peer(PeerType.User, 1);
    //    var toPeer = new Peer(PeerType.User, 2);
    //    var aggregateId = MessageId.Create(fromPeer.PeerId, _messageId);
    //    var messageItem = new MessageItem(aggregateId,
    //        fromPeer,
    //        toPeer,
    //        fromPeer,
    //        _messageId,
    //        "test message",
    //        A<int>(),
    //        A<long>(),
    //        true);
    //    var aggregateEvent = new SendMessageStartedEvent(A<RequestInfo>(),
    //        messageItem,
    //        true,
    //        1,
    //        Guid.NewGuid()
    //    );
    //    var domainEvent = ADomainEvent<MessageAggregate, MessageId, SendMessageStartedEvent>(aggregateEvent, aggregateId, 1);
    //    _commandBus.Setup(c => c.PublishAsync(It.IsAny<ICommand<UserAggregate, UserId, IExecutionResult>>(),
    //        It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(ExecutionResult.Success()));

    //    await Sut.HandleAsync(domainEvent, _sagaContext.Object, CancellationToken.None);
    //    await Sut.PublishAsync(_commandBus.Object, CancellationToken.None);

    //    Sut.UncommittedEvents.Single().AggregateEvent.ShouldBeOfType<MessageSagaStartedEvent>();
    //    _commandBus.Verify(c => c.PublishAsync(It.IsAny<ICommand<UserAggregate, UserId, IExecutionResult>>(),
    //        It.IsAny<CancellationToken>()), Times.Once);
    //}


    private static IEnumerable<object[]> GetSendMessageData()
    {
        yield return new object[] { new Peer(PeerType.User, 1), new Peer(PeerType.User, 2) };
        yield return new object[] { new Peer(PeerType.User, 1), new Peer(PeerType.Chat, 2) };
        yield return new object[] { new Peer(PeerType.User, 1), new Peer(PeerType.Channel, 2) };
    }
}