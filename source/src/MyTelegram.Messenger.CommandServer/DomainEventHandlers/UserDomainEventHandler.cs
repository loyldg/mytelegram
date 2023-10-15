using MyTelegram.Messenger.Services;
using MyTelegram.Messenger.Services.Interfaces;

namespace MyTelegram.Messenger.CommandServer.DomainEventHandlers;
public class UserDomainEventHandler :
    ISubscribeSynchronousTo<UserAggregate, UserId, UserCreatedEvent>
{
    private readonly ILogger<UserDomainEventHandler> _logger;
    private readonly IMessageAppService _messageAppService;
    private readonly IRandomHelper _randomHelper;
    private readonly IEventBus _eventBus;
    public UserDomainEventHandler(IMessageAppService messageAppService,
        IRandomHelper randomHelper,
        ILogger<UserDomainEventHandler> logger,
        IEventBus eventBus)
    {
        _messageAppService = messageAppService;
        _randomHelper = randomHelper;
        _logger = logger;
        _eventBus = eventBus;
    }

    public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserCreatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        if (!domainEvent.AggregateEvent.Bot)
        {
            var welcomeMessage = "Welcome to use MyTelegram!";
            var sendMessageInput = new SendMessageInput(new RequestInfo(0,
                    MyTelegramServerDomainConsts.OfficialUserId,
                    domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
                    domainEvent.AggregateEvent.RequestInfo.PermAuthKeyId,
                    Guid.NewGuid(), 0, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()),
                MyTelegramServerDomainConsts.OfficialUserId,
                new Peer(PeerType.User, domainEvent.AggregateEvent.UserId,domainEvent.AggregateEvent.AccessHash),
                welcomeMessage,
                _randomHelper.NextLong());

            await _messageAppService.SendMessageAsync(sendMessageInput);
        }
    }
}