namespace MyTelegram.Messenger.CommandServer.DomainEventHandlers;

public class UserDomainEventHandler :
    ISubscribeSynchronousTo<UserAggregate, UserId, UserCreatedEvent>
{
    private readonly ICommandBus _commandBus;
    private readonly IMessageAppService _messageAppService;
    private readonly IOptionsMonitor<MyTelegramMessengerServerOptions> _options;
    private readonly IRandomHelper _randomHelper;

    public UserDomainEventHandler(IMessageAppService messageAppService,
        IRandomHelper randomHelper,
        IOptionsMonitor<MyTelegramMessengerServerOptions> options,
        ICommandBus commandBus)
    {
        _messageAppService = messageAppService;
        _randomHelper = randomHelper;
        _options = options;
        _commandBus = commandBus;
    }

    public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        if (_options.CurrentValue.SetPremiumToTrueAfterUserCreated)
        {
            var command = new UpdateUserPremiumStatusCommand(domainEvent.AggregateIdentity, true);
            await _commandBus.PublishAsync(command, default);
        }

        if (!domainEvent.AggregateEvent.Bot)
        {
            var welcomeMessage = "Welcome to use MyTelegram!";
            var sendMessageInput = new SendMessageInput(new RequestInfo(0,
                    MyTelegramServerDomainConsts.OfficialUserId,
                    domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
                    domainEvent.AggregateEvent.RequestInfo.PermAuthKeyId,
                    Guid.NewGuid(), 0, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()),
                MyTelegramServerDomainConsts.OfficialUserId,
                new Peer(PeerType.User, domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.AccessHash),
                welcomeMessage,
                _randomHelper.NextLong());

            await _messageAppService.SendMessageAsync(sendMessageInput);
        }
    }
}