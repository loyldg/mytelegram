//namespace MyTelegram.Domain.Tests.IntegrationTests.Sagas;

//public class MessageSagaIntegrationTests : IntegrationTest
//{
//    [Fact]
//    public async Task SendMessage_To_UserPeer_Test()
//    {
//        var senderPeerId = 1;
//        var recipientPeerId = 2;
//        await CreateUserAsync(senderPeerId).ConfigureAwait(false);
//        await CreateUserAsync(recipientPeerId).ConfigureAwait(false);

//        var randomId = 1;
//        var aggregateId = MessageId.CreateWithRandomId(senderPeerId, randomId);
//        var messageItem = new MessageItem(aggregateId,
//            new Peer(PeerType.User, senderPeerId),
//            new Peer(PeerType.User, recipientPeerId),
//            new Peer(PeerType.User, senderPeerId),
//            0,
//            "test message",
//            DateTime.UtcNow.ToTimestamp(),
//            randomId,
//            true
//        );
//        var command = new StartSendMessageCommand(aggregateId, A<RequestInfo>(), messageItem, correlationId: Guid.NewGuid());

//        await CommandBus.PublishAsync(command, default).ConfigureAwait(false);

        
//        //var outboxMessageReadModel=await QueryProcessor.ProcessAsync(new )
//    }

//    private Task CreateUserAsync(int userId)
//    {
//        var command = new CreateUserCommand(UserId.Create(userId),
//            A<RequestInfo>(),
//            userId,
//            1,
//            userId.ToString(),
//            userId.ToString(),
//            null);
//        return CommandBus.PublishAsync(command, default);
//    }

//    protected override IServiceProvider Configure(IEventFlowOptions options)
//    {
//        options.AddDefaults(typeof(StartSendMessageCommand).Assembly);
//        options.ServiceCollection.AddMyEventFlow();
//        //options.ServiceCollection.AddSingleton<IIdGenerator, SimpleInMemoryIdGenerator>();
//        var serviceProvider = options.ServiceCollection.BuildServiceProvider();
//        //IdGeneratorFactory.SetDefaultIdGenerator(serviceProvider.GetRequiredService<IIdGenerator>());


//        return serviceProvider;
//    }
//}