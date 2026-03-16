namespace MyTelegram.Messenger.CommandServer.DomainEventHandlers;

public class SendAppCodeEventHandler(
    ILogger<SendAppCodeEventHandler> logger,
    IEventBus eventBus,
    IMessageAppService messageAppService,
    IRandomHelper randomHelper)
    :
        ISubscribeSynchronousTo<AppCodeAggregate, AppCodeId, AppCodeCreatedEvent>
{
    public async Task HandleAsync(IDomainEvent<AppCodeAggregate, AppCodeId, AppCodeCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("### Send app code: phoneNumber: {PhoneNumber}, code: {Code}",
            domainEvent.AggregateEvent.PhoneNumber,
            domainEvent.AggregateEvent.Code
        );
        await eventBus.PublishAsync(new AppCodeCreatedIntegrationEvent(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.PhoneNumber, domainEvent.AggregateEvent.Code, domainEvent.AggregateEvent.Expire));

        if (domainEvent.AggregateEvent.UserId != 0)
        {
            var message =
                $"Login code: {domainEvent.AggregateEvent.Code}. Do not give this code to anyone, even if they say they are from Telegram!\n\nThis code can be used to log in to your Telegram account. We never ask it for anything else.\n\nIf you didn't request this code by trying to log in on another device, simply ignore this message.\n\nPowered by MyTelegram\nhttps://github.com/loyldg/mytelegram";
            var entities = new TVector<IMessageEntity>
            {
                new TMessageEntityBold { Offset = 0, Length = 11 },
                new TMessageEntitySpoiler{Offset = 12,Length = domainEvent.AggregateEvent.Code.Length},
                new TMessageEntityBold { Offset = 22, Length = 3 },
                new TMessageEntityBold { Offset = message.Length-48, Length = 10 },

            };

            var sendMessageInput = new SendMessageInput(
                RequestInfo.Empty with
                {
                    UserId = MyTelegramConsts.NotificationServiceUserId,
                    Layer = MyTelegramConsts.Layer,
                    Date = DateTime.UtcNow.ToTimestamp(),
                    RequestId = Guid.NewGuid()
                },
                MyTelegramConsts.NotificationServiceUserId,
                new Peer(PeerType.User, domainEvent.AggregateEvent.UserId),
                message,
                randomHelper.NextInt64(),
                entities: entities
            );

            await messageAppService.SendMessageAsync([sendMessageInput]);
        }
    }
}
