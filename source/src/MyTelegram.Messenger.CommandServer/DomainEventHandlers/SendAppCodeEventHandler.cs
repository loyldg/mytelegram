using MyTelegram.Domain.Aggregates.AppCode;
using MyTelegram.Domain.Events.AppCode;
using MyTelegram.Domain.Extensions;

namespace MyTelegram.Messenger.CommandServer.DomainEventHandlers;

public class SendAppCodeEventHandler :
    ISubscribeSynchronousTo<AppCodeAggregate, AppCodeId, AppCodeCreatedEvent>//,
                                                                             //ISubscribeSynchronousTo<UserAggregate, UserId, UserCreatedEvent>
{
    private readonly ILogger<SendAppCodeEventHandler> _logger;
    private readonly IMessageAppService _messageAppService;
    private readonly IRandomHelper _randomHelper;
    private readonly IEventBus _eventBus;
    public SendAppCodeEventHandler(
        ILogger<SendAppCodeEventHandler> logger,
        IEventBus eventBus, IMessageAppService messageAppService, IRandomHelper randomHelper)
    {
        //_messageAppService = messageAppService;
        //_randomHelper = randomHelper;
        _logger = logger;
        _eventBus = eventBus;
        _messageAppService = messageAppService;
        _randomHelper = randomHelper;
    }

    public async Task HandleAsync(IDomainEvent<AppCodeAggregate, AppCodeId, AppCodeCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("##### Send app code:phoneNumber={PhoneNumber},code={Code}",
            domainEvent.AggregateEvent.PhoneNumber,
            domainEvent.AggregateEvent.Code
        );
        await _eventBus.PublishAsync(new AppCodeCreatedIntegrationEvent(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.PhoneNumber, domainEvent.AggregateEvent.Code, domainEvent.AggregateEvent.Expire));

        if (domainEvent.AggregateEvent.UserId != 0)
        {
            var message =
                    $"Login code: {domainEvent.AggregateEvent.Code}. Do not give this code to anyone, even if they say they are from Telegram!\n\nThis code can be used to log in to your Telegram account. We never ask it for anything else.\n\nIf you didn't request this code by trying to log in on another device, simply ignore this message.";
            var entities = new TVector<IMessageEntity>
                {
                    new TMessageEntityBold { Offset = 0, Length = 11 },
                    new TMessageEntitySpoiler{Offset = 12,Length = domainEvent.AggregateEvent.Code.Length},
                    new TMessageEntityBold { Offset = 22, Length = 3 }
                };

            var sendMessageInput = new SendMessageInput(
                new RequestInfo(0,
                    MyTelegramServerDomainConsts.OfficialUserId,
                    domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
                    domainEvent.AggregateEvent.RequestInfo.PermAuthKeyId,
                    Guid.NewGuid(),
                    0,
                    DateTime.UtcNow.ToTimestamp()
                ),
                MyTelegramServerDomainConsts.OfficialUserId,
                new Peer(PeerType.User, domainEvent.AggregateEvent.UserId),
                message,
                _randomHelper.NextLong(),
                entities: entities
            );

            await _messageAppService.SendMessageAsync(sendMessageInput);
        }
    }
}
