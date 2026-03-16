using MyTelegram.Messenger.DomainEventHandlers;

namespace MyTelegram.Messenger.CommandServer.DomainEventHandlers;

public class UserDomainEventHandler(
    IObjectMessageSender objectMessageSender,
    ICommandBus commandBus,
    IIdGenerator idGenerator,
    IAckCacheService ackCacheService,
    IMessageAppService messageAppService,
    IOptionsMonitor<MyTelegramMessengerServerOptions> options,
    IRandomHelper randomHelper)
    : DomainEventHandlerBase(objectMessageSender, commandBus, idGenerator, ackCacheService),
        ISubscribeSynchronousTo<UserAggregate, UserId, UserCreatedEvent>
{
    private readonly ICommandBus _commandBus = commandBus;

    public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        if (options.CurrentValue.SetPremiumToTrueAfterUserCreated)
        {
            var command = new UpdateUserPremiumStatusCommand(domainEvent.AggregateIdentity, true);
            await _commandBus.PublishAsync(command, default);
        }

        if (!domainEvent.AggregateEvent.Bot)
        {
            var welcomeMessage = "Welcome to MyTelegram! 🎉\n\nYour account has been successfully created.\n\nThis service is powered by the MyTelegram open-source project.\r\nLearn more: https://github.com/loyldg/mytelegram";
            var sendMessageInput = new SendMessageInput(
                RequestInfo.Empty with
                {
                    UserId = MyTelegramConsts.NotificationServiceUserId,
                    AuthKeyId = domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
                    PermAuthKeyId = domainEvent.AggregateEvent.RequestInfo.PermAuthKeyId,
                    Date = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    RequestId = Guid.NewGuid(),
                    DeviceType = DeviceType.Desktop
                },
                MyTelegramConsts.NotificationServiceUserId,
                new Peer(PeerType.User, domainEvent.AggregateEvent.UserId/*, domainEvent.AggregateEvent.AccessHash*/),
                welcomeMessage,
                randomHelper.NextInt64());

            await messageAppService.SendMessageAsync([sendMessageInput]);
        }
    }
}