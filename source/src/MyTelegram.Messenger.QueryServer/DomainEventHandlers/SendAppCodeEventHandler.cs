namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class SendAppCodeEventHandler : 
    ISubscribeSynchronousTo<AppCodeAggregate, AppCodeId, AppCodeCreatedEvent>//,
    //ISubscribeSynchronousTo<UserAggregate, UserId, UserCreatedEvent>
{
    private readonly ILogger<SendAppCodeEventHandler> _logger;
    //private readonly IMessageAppService _messageAppService;
    //private readonly IRandomHelper _randomHelper;
    private readonly IEventBus _eventBus;
    public SendAppCodeEventHandler(
        ILogger<SendAppCodeEventHandler> logger,
        IEventBus eventBus)
    {
        //_messageAppService = messageAppService;
        //_randomHelper = randomHelper;
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
        await _eventBus.PublishAsync(new AppCodeCreatedIntegrationEvent(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.PhoneNumber, domainEvent.AggregateEvent.Code, domainEvent.AggregateEvent.Expire));
    }
}
