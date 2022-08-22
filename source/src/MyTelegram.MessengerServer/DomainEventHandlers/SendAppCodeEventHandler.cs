namespace MyTelegram.MessengerServer.DomainEventHandlers;

public class SendAppCodeEventHandler : ISubscribeSynchronousTo<AppCodeAggregate, AppCodeId, AppCodeCreatedEvent>,
    ISubscribeSynchronousTo<UserAggregate, UserId, UserCreatedEvent>

{
    private readonly IMessageAppService _messageAppService;
    private readonly IRandomHelper _randomHelper;
    private readonly ILogger<SendAppCodeEventHandler> _logger;
    private readonly IEventBus _eventBus;
    public SendAppCodeEventHandler(IMessageAppService messageAppService,
        IRandomHelper randomHelper, ILogger<SendAppCodeEventHandler> logger,
        IEventBus eventBus)
    {
        _messageAppService = messageAppService;
        _randomHelper = randomHelper;
        _logger = logger;
        _eventBus = eventBus;
    }

    public async Task HandleAsync(IDomainEvent<AppCodeAggregate, AppCodeId, AppCodeCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("##### Send app code:phoneNumber={PhoneNumber},code={Code}",
            domainEvent.AggregateEvent.PhoneNumber,
            domainEvent.AggregateEvent.Code
        );

        await _eventBus.PublishAsync(new AppCodeCreatedIntegrationEvent(domainEvent.AggregateEvent.UserId,
                    domainEvent.AggregateEvent.PhoneNumber,
                    domainEvent.AggregateEvent.Code,
                    domainEvent.AggregateEvent.Expire)).ConfigureAwait(false);

        if (domainEvent.AggregateEvent.UserId != 0)
        {
            var message =
                $"Login code: {domainEvent.AggregateEvent.Code}. Do not give this code to anyone, even if they say they are from Telegram!\n\nThis code can be used to log in to your Telegram account. We never ask it for anything else.\n\nIf you didn't request this code by trying to log in on another device, simply ignore this message.";
            var entities = new TVector<IMessageEntity>
            {
                new TMessageEntityBold {Offset = 0, Length = 11,},
                new TMessageEntityBold {Offset = 22, Length = 3,}
            };

            var sendMessageInput = new SendMessageInput(
                new RequestInfo(0,
                    MyTelegramServerDomainConsts.OfficialUserId,
                    domainEvent.AggregateEvent.Request.AuthKeyId,
                    domainEvent.AggregateEvent.Request.PermAuthKeyId,
                    Guid.NewGuid()
                    ),
                MyTelegramServerDomainConsts.OfficialUserId,
                new Peer(PeerType.User, domainEvent.AggregateEvent.UserId),
                message,
                _randomHelper.NextLong(),
                entities: entities.ToBytes()
            );

            await _messageAppService.SendMessageAsync(sendMessageInput).ConfigureAwait(false);
        }
    }

    public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        if (!domainEvent.AggregateEvent.Bot)
        {
            var welcomeMessage = "Welcome to use MyTelegram!";
            var sendMessageInput = new SendMessageInput(new RequestInfo(0,
                    MyTelegramServerDomainConsts.OfficialUserId,
                    domainEvent.AggregateEvent.Request.AuthKeyId,
                    domainEvent.AggregateEvent.Request.PermAuthKeyId, Guid.NewGuid()),
                MyTelegramServerDomainConsts.OfficialUserId,
                new Peer(PeerType.User, domainEvent.AggregateEvent.UserId),
                welcomeMessage,
                _randomHelper.NextLong());

            await _messageAppService.SendMessageAsync(sendMessageInput).ConfigureAwait(false);
        }
    }
}